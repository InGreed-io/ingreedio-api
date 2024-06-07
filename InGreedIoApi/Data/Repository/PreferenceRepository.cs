using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Repository
{
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public PreferenceRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> DeleteIngredient(int preferenceId, int ingredientId)
        {
            var preference = await _context.Preferences.FirstOrDefaultAsync(x => x.Id == preferenceId);

            if (preference != null)
            {
                var ingredient = FindById(preference, ingredientId);
                if (ingredient != null && (preference.Wanted.Remove(ingredient) || preference.Unwanted.Remove(ingredient)))
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> AddIngredient(int preferenceId, AddIngredientDTO addIngredientDto)
        {
            var preference = await _context.Preferences
            .Include(p => p.Wanted)
            .Include(p => p.Unwanted)
            .FirstOrDefaultAsync(x => x.Id == preferenceId);

            if (preference != null)
            {
                var ingredient = await _context.Ingredients.FirstOrDefaultAsync(x => x.Id == addIngredientDto.Id);
                if (ingredient != null)
                {
                    if (addIngredientDto.IsWanted)
                    {
                        preference.Wanted.Add(ingredient);
                    }
                    else
                    {
                        preference.Unwanted.Add(ingredient);
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Delete(int preferenceId)
        {
            var preference = await _context.Preferences.FirstOrDefaultAsync(x => x.Id == preferenceId);

            if (preference != null)
            {
                _context.Preferences.Remove(preference);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IngredientPOCO? FindById(PreferencePOCO preferencePOCO, int ingredientId)
        {
            var ingredientWanted = preferencePOCO.Wanted.FirstOrDefault(x => x.Id == ingredientId);
            if (ingredientWanted != null)
            {
                return ingredientWanted;
            }
            return preferencePOCO.Unwanted.FirstOrDefault(x => x.Id == ingredientId);
        }
    }
}