using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;

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
                ));

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
                .ForMember(dto => dto.Ingredients, opt => opt.MapFrom(src => src.Ingredients.Select(ing => ing.Name)))
                .ForMember(dto => dto.Reviews, opt => opt.MapFrom(src => src.Reviews.Select(x => new ProductRevewiDTO()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Rating = x.Rating,
                    UserName = x.User != null ? x.User.UserName : "Unknown"
                })));
        }
    }
}