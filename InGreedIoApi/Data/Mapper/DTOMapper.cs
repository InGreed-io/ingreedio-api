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
                .ConstructUsing((product, context) => new ProductDetailsDTO(
                    product.Id,
                    product.Name,
                    product.IconUrl,
                    product.Rating,
                    product.Reviews.Count(),
                    product.Featuring != null,
                    product.Producer?.Company?.Name,
                    product.Description,
                    false
                ));
        }
    }
}