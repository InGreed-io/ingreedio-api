using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.POCO;
using InGreedIoApi.Utils.Pagination;

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

        public async Task<IPage<IngredientDTO>> FindAll(GetIngredientsQuery getIngredientsQuery)
        {
            var ingredientsQuery = _context.Ingredients.AsQueryable();

            if (!string.IsNullOrEmpty(getIngredientsQuery.Query))
            {
                ingredientsQuery = ingredientsQuery.Where(
                    x => x.Name.ToLower().Contains(getIngredientsQuery.Query.ToLower())
                );
            }

            if (getIngredientsQuery.Include != null) {
                ingredientsQuery = ingredientsQuery.Where(
                    x => getIngredientsQuery.Include.Contains(x.Id)
                );
            }

            if (getIngredientsQuery.Exclude != null) {
                ingredientsQuery = ingredientsQuery.Where(
                    x => !getIngredientsQuery.Exclude.Contains(x.Id)
                );
            }

            return await ingredientsQuery.OrderBy(ingredient => ingredient.Id)
                .ProjectToPageAsync<IngredientPOCO, IngredientDTO>(
                    getIngredientsQuery.pageIndex, getIngredientsQuery.pageSize, _mapper.ConfigurationProvider
                );
        }
    }
}
