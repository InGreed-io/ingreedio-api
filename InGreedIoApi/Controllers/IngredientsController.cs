using AutoMapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientsController(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredients([FromBody] string query, [FromBody] int page, [FromBody] int limit)
        {
            var ingredients = await _ingredientRepository.FindAll(query, page, limit);
            return Ok(_mapper.Map<List<IngredientDTO>>(ingredients));
        }
    }
}
