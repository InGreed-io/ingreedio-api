using AutoMapper;
using InGreedIoApi.Model;
using InGreedIoApi.POCO;

namespace InGreedIoApi.Data.Mapper
{
    public class POCOMapper : Profile
    {
        public POCOMapper()
        {
            // From POCO to Models
            CreateMap<CategoryPOCO, Category>();
            CreateMap<CompanyInfoPOCO, CompanyInfo>();
            CreateMap<FeaturingPOCO, Featuring>();
            CreateMap<IngredientPOCO, Ingredient>();
            CreateMap<ReviewPOCO, Review>();
            CreateMap<ProductPOCO, Product>()
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
            .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
            .ForMember(dest => dest.Producer, opt => opt.MapFrom(src => src.Producer));

            CreateMap<ApiUserPOCO, ApiUser>();
            CreateMap<PreferencePOCO, Preference>();
            CreateMap<AppNotificationPOCO, AppNotification>();
            CreateMap<OperationLogPOCO, OperationLog>();
            CreateMap<OperationTypePOCO, OperationType>();

            // From Models to POCO
            CreateMap<Category, CategoryPOCO>();
            CreateMap<CompanyInfo, CompanyInfoPOCO>();
            CreateMap<Featuring, FeaturingPOCO>();
            CreateMap<Ingredient, IngredientPOCO>();
            CreateMap<Review, ReviewPOCO>();
            CreateMap<Product, ProductPOCO>();
            CreateMap<ApiUser, ApiUserPOCO>();
            CreateMap<Preference, PreferencePOCO>();
            CreateMap<AppNotification, AppNotificationPOCO>();
            CreateMap<OperationType, OperationTypePOCO>();
            CreateMap<OperationLog, OperationLogPOCO>();
        }
    }
}