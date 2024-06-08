using AutoMapper;
using InGreedIoApi.POCO;
using InGreedIoApi.DTO;

namespace InGreedIoApi.Data.Mapper
{
    public class ListMapper : Profile
    {
        public ListMapper()
        {
            CreateProjection<ProductPOCO, ProductDTO>()
                .ConstructUsing(src => new ProductDTO(
                    src.Id, src.Name, src.IconUrl,
                    src.Reviews == null ? // For some reason .Average(review => review.Rating) doesn't work
                        0 : src.Reviews.Sum(review => review.Rating) /
                            (src.Reviews.Count() == 0 ? 1 : src.Reviews.Count()),
                    src.Reviews == null ? 0 : src.Reviews.Count(),
                    src.Featuring != null && src.Featuring.ExpireDate > DateTime.UtcNow,
                    null
                ));
            CreateProjection<IngredientPOCO, IngredientDTO>()
                .ConstructUsing(src => new IngredientDTO(src.Id, src.Name));
            CreateProjection<ReviewPOCO, ReviewDTO>()
                .ConstructUsing(src => new ReviewDTO(
                    src.Id, src.User.UserName!, src.Text, src.Rating, src.UserID
                ));
            CreateProjection<ReviewPOCO, ReportedReviewDTO>()
                .ConstructUsing(src => new ReportedReviewDTO(
                    src.Id, src.User.UserName!, src.Text, src.Rating, src.UserID, src.ReportsCount
                ));
            CreateProjection<ApiUserPOCO, ApiUserListItemDTO>()
                .ForMember(dst => dst.IsBlocked, opt => opt.MapFrom(src => 
                    src.LockoutEnabled && src.LockoutEnd.HasValue && src.LockoutEnd.Value > DateTimeOffset.UtcNow
                ));
        }
    }
}
