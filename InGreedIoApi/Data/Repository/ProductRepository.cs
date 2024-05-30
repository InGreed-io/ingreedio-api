using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.Model.Enum;
using InGreedIoApi.Model.Exceptions;
using InGreedIoApi.Utils.Pagination;

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

    public async Task<IPage<ProductDTO>> GetAll(ProductQueryDTO productQueryDto)
    {
        var queryable = _context.Products.AsQueryable();
        queryable = queryable.Where(p => p.Name.ToLower().Contains(productQueryDto.query.ToLower()));

        if (productQueryDto.categoryId.HasValue)
            queryable = queryable.Where(p => p.CategoryId == productQueryDto.categoryId.Value);

        UpdateWantedAndUnwantedFromPreference(productQueryDto, ref queryable);
        //sort elements by enum
        SortProductQueryDto(productQueryDto, ref queryable);

        return await queryable.ProjectToPageAsync<ProductPOCO, ProductDTO>(
            productQueryDto.page, productQueryDto.limit, _mapper.ConfigurationProvider
        );
    }

    public async Task<Product> GetProduct(int productId)
    {
        var product = await _context.Products
            .Include(x => x.Featuring)
            .Include(x => x.Ingredients)
            .Include(x => x.Producer)
            .ThenInclude(x => x.Company)
            .FirstOrDefaultAsync(x => x.Id == productId);

        return _mapper.Map<Product>(product);
    }

    public async Task<IPage<ReviewDTO>> GetReviews(int productId, int pageIndex, int pageSize)
    {
        return await _context.Reviews
            .Include(review => review.User)
            .Where(review => review.ProductId == productId)
            .OrderBy(review => review.ReportsCount)
            .ThenByDescending(review => review.Id)
            .ProjectToPageAsync<ReviewPOCO, ReviewDTO>(pageIndex, pageSize, _mapper.ConfigurationProvider);
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

    public async Task<Product> Create(CreateProductDTO createProductDto, string producerId)
    {
        var productPOCO = new ProductPOCO()
        {
            CategoryId = createProductDto.CategoryId,
            ProducerId = producerId,
            Description = createProductDto.Description,
            Name = createProductDto.Name,
            IconUrl = "brak",
            Ingredients = _context.Ingredients.Where(x => createProductDto.Ingredients.Contains(x.Id)).ToList()
        };
        await _context.Products.AddAsync(productPOCO);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException)
        {
            if (!await _context.Category.AnyAsync(c => c.Id == createProductDto.CategoryId))
                throw new InGreedIoException(
                    $"Could not find category with id: {createProductDto.CategoryId}.",
                    StatusCodes.Status404NotFound
                );
            if (!await _context.Users.AnyAsync(u => u.Id == producerId))
                throw new InGreedIoException(
                    $"Could not find user with id: {producerId}.",
                    StatusCodes.Status404NotFound
                );
            throw new InGreedIoException(
                $"Unknown error.",
                StatusCodes.Status400BadRequest
            );
        }
        return _mapper.Map<Product>(productPOCO);
    }

    public async Task<Product> Update(UpdateProductDTO updateProductDTO, int productId, string? producerId = null)
    {
        var product = await _context.Products
            .Include(product => product.Ingredients)
            .SingleOrDefaultAsync(x => x.Id == productId);

        if (product == null) 
            throw new InGreedIoException("Could not find product.", StatusCodes.Status404NotFound);
        if (!string.IsNullOrEmpty(producerId) && producerId != product.ProducerId) 
            throw new InGreedIoException("Could not access product.", StatusCodes.Status403Forbidden);

        product.Description = updateProductDTO.Description;
        product.Name = updateProductDTO.Name;
        var ingredients = await _context.Ingredients.Where(x => updateProductDTO.Ingredients.Contains(x.Id)).ToListAsync();
        product.Ingredients = ingredients;

        await _context.SaveChangesAsync();

        return _mapper.Map<Product>(product);
    }

    public async Task Delete(int productId, string? producerId = null)
    {
        var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product == null) 
            throw new InGreedIoException("Could not find product.", StatusCodes.Status404NotFound);
        if (!string.IsNullOrEmpty(producerId) && producerId != product.ProducerId) 
            throw new InGreedIoException("Could not access product.", StatusCodes.Status403Forbidden);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    private void UpdateWantedAndUnwantedFromPreference(ProductQueryDTO productQueryDto, ref IQueryable<ProductPOCO> queryable)
    {
        var wanted = productQueryDto.ingredients;
        var unwanted = new List<int>();

        if (productQueryDto.preferenceId.HasValue)
        {
            //Get preference
            var preferencePoco = _context.Preferences.Single(pref => pref.Id == productQueryDto.preferenceId);
            var preference = _mapper.Map<Preference>(preferencePoco);

            //Get wanted and unwanted
            ICollection<int> wantedFromPreference = preference.Wanted.Select(i => i.Id).ToList();
            wanted = wanted.Concat(wantedFromPreference).ToList();
            unwanted = preference.Unwanted.Select(i => i.Id).ToList();
        }

        // filter products that doesnt have any unwanted ingredient and has all wanted igredients
        if (wanted is not null && wanted.Count > 0)
        {
            queryable = queryable.Where(p => p.Ingredients.All(i => wanted.Contains(i.Id)));
        }
        if (unwanted.Count > 0)
        {
            queryable = queryable.Where(p => !p.Ingredients.Any(i => unwanted.Contains(i.Id)));
        }
    }

    private void SortProductQueryDto(ProductQueryDTO productQueryDto, ref IQueryable<ProductPOCO> queryable)
    {
        if (productQueryDto.SortBy.HasValue)
        {
            queryable = productQueryDto.SortBy switch
            {
                QuerySortType.Featured => queryable.OrderBy(p => p.Featuring != null),
                QuerySortType.Rating => queryable.OrderBy(p => p.Reviews.Average(r => r.Rating)),
                QuerySortType.RatingCount => queryable.OrderBy(p => p.Reviews.Count),
                QuerySortType.BestMatch => queryable,
                QuerySortType.Names => queryable.OrderBy(p => p.Name),
                _ => throw new ArgumentOutOfRangeException("sorty is not defined properly")
            };
        }
    }
}
