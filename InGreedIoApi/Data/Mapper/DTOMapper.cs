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
            CreateMap<Category, CategoryDTO>();

            // From DTO to models
            CreateMap<CategoryDTO, Category>();
        }
    }
}
