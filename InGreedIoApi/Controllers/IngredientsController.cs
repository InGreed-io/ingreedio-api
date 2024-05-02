using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.Model.Exceptions;
using InGreedIoApi.Utils.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [TypeFilter<InGreedIoExceptionFilter>]
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

        [Paginated]
        [HttpGet]
        public async Task<ActionResult<IPage<IngredientDTO>>> GetIngredients([FromQuery] GetIngredientsQuery getIngredientsQuery)
        {
            var ingredients = await _ingredientRepository.FindAll(getIngredientsQuery);
            return Ok(ingredients);
        }
    }
}
