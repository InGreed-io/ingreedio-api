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
            CreateMap<Review, ReviewDTO>();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<ApiUser, ApiUserDTO>()
                .ForMember(dest => dest.Mail,
                opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.EmailConfirmed));
            CreateMap<Preference, PreferenceDTO>()
                .ForMember(dest => dest.Wanted,
                    opt => opt.MapFrom(src => src.Wanted))
                .ForMember(dest => dest.Unwanted,
                    opt => opt.MapFrom(src => src.Unwanted));
            CreateMap<Product, ProductDTO>();
        }
    }
}
