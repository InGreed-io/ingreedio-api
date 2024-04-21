using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.Model.Enum;
using InGreedIoApi.Model.Exceptions;

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

    public async Task<IEnumerable<ProductDTO>> GetAll(ProductQueryDTO productQueryDto)
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

        var productsPoco = queryable.AsEnumerable();
        var products = _mapper.Map<List<Product>>(productsPoco);
        var productsDTO = _mapper.Map<List<ProductDTO>>(products);

        return productsDTO;
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

    public async Task<IEnumerable<Review>> GetReviews(int productId, int page, int limit)
    {
        var reviewsPoco = await _context.Reviews
            .Include(review => review.User)
            .Where(review => review.ProductId == productId)
            .OrderBy(review => review.ReportsCount)
            .Skip(page * limit)
            .Take(limit)
            .ToListAsync();

        return _mapper.Map<List<Review>>(reviewsPoco);
    }

    public async Task<Review> AddReview(int productId, string userId, string content, float rating)
    {
        var newReviewPoco = new ReviewPOCO
        {
            Text = content,
            Rating = rating,
            ReportsCount = 0,
            ProductId = productId,
            UserID = userId
        };

        await _context.Reviews.AddAsync(newReviewPoco);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException)
        {
            if (!await _context.Products.AnyAsync(p => p.Id == productId))
                throw new InGreedIoException(
                    $"Could not find product with id: {productId}.",
                    StatusCodes.Status404NotFound
                );
            if (!await _context.Users.AnyAsync(u => u.Id == userId))
                throw new InGreedIoException(
                    $"Could not find user with id: {userId}.",
                    StatusCodes.Status404NotFound
                );
            throw new InGreedIoException(
                $"Unknown error.",
                StatusCodes.Status400BadRequest
            );
        }

        await _context.Entry(newReviewPoco).Reference(review => review.User).LoadAsync();

        return _mapper.Map<Review>(newReviewPoco);
    }
}