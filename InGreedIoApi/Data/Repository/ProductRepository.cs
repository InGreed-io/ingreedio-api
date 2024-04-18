using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.Model.Enum;
using Serilog;

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
        queryable = queryable.Where(p => p.Name.ToLower().Contains(productQueryDto.query.ToLower()));

        if (productQueryDto.categoryId.HasValue)
            queryable = queryable.Where(p => p.CategoryId == productQueryDto.categoryId.Value);

        var wanted = productQueryDto.ingredients;
        var unwanted = new List<int>();

        if (productQueryDto.preferenceId.HasValue)
            UpdateWantedAndUnwantedFromPreference(productQueryDto.preferenceId, wanted, unwanted);

        if (productQueryDto.ingredients != null)
        {
            // filter products that doesnt have any unwanted ingredient and has all wanted igredients
            queryable = queryable.Where(p => p.Ingredients.Any(i => unwanted.Contains(i.Id)) == false);
            queryable = queryable.Where(p => p.Ingredients.All(i => wanted.Contains(i.Id)) == true);
        }

        //sort elements by enum
        queryable = productQueryDto.SortBy switch
        {
            QuerySortType.Featured => queryable.OrderBy(p => p.Featuring != null),
            QuerySortType.Rating => queryable.OrderBy(p => p.Reviews.Sum(r => r.Rating)),
            QuerySortType.RatingCount => queryable.OrderBy(p => p.Reviews.Count),
            QuerySortType.BestMatch => queryable,
            _ => throw new ArgumentOutOfRangeException()
        };

        //Pagination
        queryable = queryable.Skip(productQueryDto.page * productQueryDto.limit).Take(productQueryDto.limit);

        var productsPoco = queryable.AsEnumerable();
        return _mapper.Map<List<Product>>(productsPoco);
    }

    private void UpdateWantedAndUnwantedFromPreference(int? preferenceId, ICollection<int> wanted, ICollection<int> unwanted)
    {
        //Get preference
        var preferencePoco = _context.Preferences.Single(pref => pref.Id == preferenceId);
        var preference = _mapper.Map<Preference>(preferencePoco);

        //Get wanted and unwanted
        ICollection<int> wantedFromPreference = preference.Wanted.Select(i => i.Id).ToList();
        wanted = wanted.Concat(wantedFromPreference).ToList();
        unwanted = preference.Unwanted.Select(i => i.Id).ToList();
    }
}