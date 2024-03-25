using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly ApiDbContext _context;

        public CategoryRepository(IMapper mapper, ApiDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var categoriesPOCO = await _context.Category.ToListAsync();
            return _mapper.Map<List<Category>>(categoriesPOCO);
        }
    }
}