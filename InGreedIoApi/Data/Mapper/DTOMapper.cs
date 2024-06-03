using System.Security.Claims;
using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Mapper
{
    public class DTOMapper : Profile
    {
        public DTOMapper()
        {
            // From models to DTO
            CreateMap<Review, ReviewDTO>().ConstructUsing(src => new ReviewDTO(
                src.Id,
                src.User.UserName ?? "???",
                src.Text,
                src.Rating,
                src.UserID
            ));
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<ApiUser, ApiUserDTO>().ConstructUsing(src => new(
                src.Id,
                src.Email,
                src.UserName,
                src.EmailConfirmed));

            CreateMap<Product, ProductDTO>()
                .ConstructUsing((product, context) => new ProductDTO(
                    product.Id,
                    product.Name,
                    product.IconUrl,
                    product.Rating,
                    product.Reviews.Count(),
                    product.Featuring != null
                ))
                .ForMember(dto => dto.Favourite, opt => opt.MapFrom<IsFavoritedResolver>());

            CreateMap<Product, ProductDetailsDTO>()
                .ConstructUsing((product, context) =>
                {
                    var ingredientNames = product.Ingredients.Select(ing => ing.Name).ToList();
                    return new ProductDetailsDTO(
                        product.Id,
                        product.Name,
                        product.IconUrl,
                        product.Rating,
                        product.Reviews.Count(),
                        product.Featuring != null,
                        product.Producer?.Company?.Name ?? "Unknown",
                        product.Description,
                        ingredientNames,
                        false
                    );
                })
                .ForMember(dto => dto.Ingredients, opt => opt.MapFrom(src => src.Ingredients.Select(ing => ing.Name)));
        }
    }


    public class IsFavoritedResolver : IValueResolver<Product, ProductDTO, bool?>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiDbContext _context;

        public IsFavoritedResolver(IHttpContextAccessor httpContextAccessor, ApiDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public bool? Resolve(Product source, ProductDTO destination, bool? destMember, ResolutionContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null || httpContext.User == null)
            {
                return false;
            }

            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return false;

            var user = _context.ApiUsers
                                .Include(u => u.FavouriteProducts)
                                .SingleOrDefault(u => u.Id == userId);

            return user?.FavouriteProducts.Any(p => p.Id == source.Id) ?? false;
        }
    }
}