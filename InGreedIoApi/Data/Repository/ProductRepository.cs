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
            productQueryDto.pageIndex, productQueryDto.pageSize, _mapper.ConfigurationProvider
        );
    }

    public async Task<Product> GetProduct(int productId)
    {
        var product = await _context.Products.Include(x => x.Featuring).Include(x => x.Ingredients).Include(x => x.Producer).ThenInclude(x => x.Company).FirstOrDefaultAsync(x => x.Id == productId);

        return _mapper.Map<Product>(product);
    }

    public async Task<Product?> GetProductPermission(int productId, string userId)
    {
        var product = await GetProduct(productId);
        if ((userId != "Admin" && userId != "Moderator") || product == null)
        {
            if (product == null || product.Producer == null || product.Producer.Id != userId)
                return null;
            else
                return product;
        }

        return product;
    }

    public async Task<IPage<ReviewDTO>> GetReviews(int productId, int pageIndex, int pageSize)
    {
        return await _context.Reviews
            .Include(review => review.User)
            .Where(review => review.ProductId == productId)
            .OrderBy(review => review.ReportsCount)
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

    public async Task<bool> Create(CreateProductDTO createProductDto, string Id)
    {
        var productPOCO = new ProductPOCO()
        {
            CategoryId = createProductDto.CategoryId,
            ProducerId = Id,
            Description = createProductDto.Description,
            Name = createProductDto.Name,
            Category = _context.Category.Single(x => x.Id == createProductDto.CategoryId),
            Producer = _context.ApiUsers.Single(x => x.Id == Id),
            IconUrl = "brak",
            Ingredients = _context.Ingredients.Where(x => createProductDto.Ingredients.Contains(x.Id)).ToList()
        };
        await _context.Products.AddAsync(productPOCO);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> Update(UpdateProductDTO updateProductDTO, int productId)
    {
        var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);
        if (product == null)
            return false;
        product.Description = updateProductDTO.Description;
        product.Name = updateProductDTO.Name;
        var ingredients = await _context.Ingredients.Where(x => updateProductDTO.Ingredients.Contains(x.Id)).ToListAsync();
        product.Ingredients = ingredients;
        _context.Update(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int productId)
    {
        var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);
        if (product == null)
            return false;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
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
