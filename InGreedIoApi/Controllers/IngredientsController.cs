using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
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
<<<<<<< HEAD
        public async Task<IActionResult> GetIngredients([FromBody] GetIngredientsQuery query)
        {
            var ingredients = await _ingredientRepository.FindAll(query.Query, query.Page, query.Limit);
=======
        public async Task<IActionResult> GetIngredients([FromQuery]IngredientQueryDTO ingredientQuery)
        {
            var ingredients = await _ingredientRepository.FindAll(ingredientQuery.query,
                ingredientQuery.page, ingredientQuery.limit);
>>>>>>> 085c96f (fixed ingredients controller)
            return Ok(_mapper.Map<List<IngredientDTO>>(ingredients));
        }
    }
}