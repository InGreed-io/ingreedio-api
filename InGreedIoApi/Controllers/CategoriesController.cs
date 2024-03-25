using AutoMapper;
using InGreedIoApi.Data.Mapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [Route("/api/[controller]/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }
    }
}
