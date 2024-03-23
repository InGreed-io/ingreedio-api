using AutoMapper;
using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IMapper _mapper;
        private readonly ApiDbContext _context;

        public IngredientRepository(IMapper mapper, ApiDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> FindAll(string query, int page, int limit)
        {
            var ingredientsPOCO = await _context.Ingredients.Where(x => x.Name.StartsWith(query)).ToListAsync();
            ingredientsPOCO.GetRange(page * limit, limit);
            return _mapper.Map<List<Ingredient>>(ingredientsPOCO);
        }
    }
}
