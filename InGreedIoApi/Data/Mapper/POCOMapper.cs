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
            CreateMap<ProductPOCO, Product>();
            CreateMap<ApiUserPOCO, ApiUser>();
            CreateMap<PreferencePOCO, Preference>();

            // From Models to POCO
            CreateMap<Category, CategoryPOCO>();
            CreateMap<CompanyInfo, CompanyInfoPOCO>();
            CreateMap<Featuring, FeaturingPOCO>();
            CreateMap<Ingredient, IngredientPOCO>();
            CreateMap<Review, ReviewPOCO>();
            CreateMap<Product, ProductPOCO>();
            CreateMap<ApiUser, ApiUserPOCO>();
            CreateMap<Preference, PreferencePOCO>();
        }
    }
}