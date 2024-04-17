using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace InGreedIoApi.Controllers
{
    [Route("/api/[controller]/")]
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
        public async Task<IActionResult> GetIngredients([FromQuery] GetIngredientsQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredients = await _ingredientRepository.FindAll(query.Query, query.Page, query.Limit);
            return Ok(_mapper.Map<List<IngredientDTO>>(ingredients));
        }
    }
}