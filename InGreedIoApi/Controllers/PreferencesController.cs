using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PreferencesController : ControllerBase
    {
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IMapper _mapper;

        public PreferencesController(IPreferenceRepository preferenceRepository, IMapper mapper)
        {
            _preferenceRepository = preferenceRepository;
            _mapper = mapper;
        }

        [HttpPatch("/{preferenceId}")]
        public async Task<IActionResult> AddIngredient(int preferenceId, [FromBody] AddIngredientDTO addIngredientDto)
        {
            var isDone = await _preferenceRepository.AddIngredient(preferenceId, addIngredientDto);
            return isDone ? Ok("the ingredient was added") : NotFound("the ingredient/preference wasnt found");
        }

        [HttpDelete("/{preferenceId}")]
        public async Task<IActionResult> Delete(int preferenceId)
        {
            var isDone = await _preferenceRepository.Delete(preferenceId);
            return isDone ? Ok("the preference was deleted") : NotFound("the preference wasnt found");
        }

        [HttpPatch("/{preferenceId}/ingredients/{ingredientId}/delete")]
        public async Task<IActionResult> DeleteIngredient(int preferenceId, int ingredientId)
        {
            var isDone = await _preferenceRepository.DeleteIngredient(preferenceId, ingredientId);
            return isDone ? Ok("the ingredient was deleted") : NotFound("the ingredient/preference wasnt found");
        }
    }
}