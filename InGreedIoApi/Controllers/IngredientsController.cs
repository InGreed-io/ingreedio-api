using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [Route("/api/[controller]/")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IPaginationService _paginationService;
        private readonly IMapper _mapper;

        public IngredientsController(IIngredientRepository ingredientRepository, IMapper mapper, IPaginationService paginationService)
        {
            _ingredientRepository = ingredientRepository;
            _paginationService = paginationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredients([FromQuery] GetIngredientsQuery getIngredientsQuery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredients = await _ingredientRepository.FindAll(getIngredientsQuery.Query);
            var paginatedResults = await _paginationService.Paginate<IngredientDTO>(
                  _mapper.Map<List<IngredientDTO>>(ingredients),
                  getIngredientsQuery.Limit,
                  getIngredientsQuery.Page);
            return Ok(paginatedResults);
        }
    }
}
