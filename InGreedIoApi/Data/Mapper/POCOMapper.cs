﻿using AutoMapper;
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
            CreateMap<AppNotification, AppNotificationPOCO>();
            CreateMap<OperationType, OperationTypePOCO>();
            CreateMap<OperationLog, OperationLogPOCO>();
        }
    }
}