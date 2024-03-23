using InGreedIoApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [Route("/api/[controller]/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
        }
    }
}
