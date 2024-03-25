using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;

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
        queryable = queryable.Where(p => p.Ingredients.Where(i => productQueryDto.ingredients.Contains(i)));
        queryable = queryable.Where(p => productQueryDto.ingredients.All(i => p.Ingredients.Contains(i)));
        
        var products = queryable.AsEnumerable();
        
        return products;
    
    }
    
}