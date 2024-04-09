using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Data.Repository.Interface;


namespace InGreedIoApi.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly ApiDbContext _context;

    public ProductRepository(IMapper mapper, ApiDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetAll(ProductQueryDTO productQueryDto)
    {
        var queryable = _context.Products.AsQueryable();

        queryable = queryable.Where(p => p.Name.Contains(productQueryDto.query));
        if (productQueryDto.categoryId.HasValue)
        {
            queryable = queryable.Where(p => p.CategoryId == productQueryDto.categoryId.Value);
        }
        
        queryable = queryable.Where(p => p.Ingredients
            .Any(i => productQueryDto.ingredients.Contains(i.Id)));
        
        if (productQueryDto.preferenceId.HasValue)
        {
            //Get preference 
            var preferencePoco = _context.Preferences.Single(pref => pref.Id == productQueryDto.preferenceId);
            //Remove all products which have any unwanted ingredient or and have all wanted ingredientsp
            _mapper.Map<Preference>(preferencePoco);
            
            
        }
        
        var productsPoco = queryable.AsEnumerable();

        return _mapper.Map<List<Product>>(productsPoco);
    }
    
}