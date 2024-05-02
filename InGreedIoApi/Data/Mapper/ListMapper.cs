using AutoMapper;
using InGreedIoApi.POCO;
using InGreedIoApi.DTO;

namespace InGreedIoApi.Data.Mapper
{
    public class ListMapper : Profile
    {
        public ListMapper()
        {
            CreateProjection<ReviewPOCO, ReviewDTO>()
                .ConstructUsing(src => new ReviewDTO(
                    src.Id, src.User.UserName!, src.Text, src.Rating, src.UserID
                ));
        }
    }
}
