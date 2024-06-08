using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.Model.Enum;
using InGreedIoApi.Services;
using InGreedIoApi.Model.Exceptions;
using InGreedIoApi.Utils.Pagination;
using InGreedIoApi.Services;

namespace InGreedIoApi.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly ApiDbContext _context;
    private readonly IProductService _productService;
    private readonly IUploadService _uploadService;

    public ProductRepository(IMapper mapper, ApiDbContext context, IProductService productService, IUploadService uploadService)
    {
        _mapper = mapper;
        _context = context;
        _productService = productService;
        _uploadService = uploadService;
    }

    public async Task<IPage<ProductDTO>> GetAll(ProductQueryDTO productQueryDto, string? producerId = null)
    {
        var queryable = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(producerId))
            queryable = queryable.Where(p => p.ProducerId == producerId);

        if (!string.IsNullOrEmpty(productQueryDto.query))
            queryable = queryable.Where(p => p.Name.ToLower().Contains(productQueryDto.query.ToLower()));

        if (productQueryDto.categoryId.HasValue)
            queryable = queryable.Where(p => p.CategoryId == productQueryDto.categoryId.Value);

        UpdateWantedAndUnwantedFromPreference(productQueryDto, ref queryable);
        //sort elements by enum
        _productService.SortProductQueryDto(productQueryDto, ref queryable);

        return await queryable.ProjectToPageAsync<ProductPOCO, ProductDTO>(
            productQueryDto.pageIndex, productQueryDto.pageSize, _mapper.ConfigurationProvider
        );
    }

    public async Task<Product> GetProduct(int productId)
    {
        var product = await _context.Products
            .Include(x => x.Reviews)
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
        // TODO: catch error here
        string iconUrl = await _uploadService.UploadFileAsync(createProductDto.Photo, $"{createProductDto.Name}-{createProductDto.Photo.FileName}");

        var productPOCO = new ProductPOCO()
        {
            CategoryId = createProductDto.CategoryId,
            ProducerId = producerId,
            Description = createProductDto.Description,
            Name = createProductDto.Name,
            IconUrl = iconUrl,
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

    public async Task<bool> AddToFavourites(int productId, string userId)
    {
        var product = await _context.Products
            .Include(p => p.FavouriteBy)
            .SingleOrDefaultAsync(x => x.Id == productId);
        if (product == null) return false;

        var user = await _context.ApiUsers
            .Include(u => u.FavouriteProducts)
            .SingleOrDefaultAsync(x => x.Id == userId);

        if (user == null) return false;

        if (product.FavouriteBy.Contains(user)) return false;
        if (user.FavouriteProducts.Contains(product)) return false;

        product.FavouriteBy.Add(user);
        user.FavouriteProducts.Add(product);

        _context.Update(product);
        _context.Update(user);

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

    public async Task<bool> RemoveFromFavourites(int productId, string userId)
    {
        var product = await _context.Products
            .Include(p => p.FavouriteBy)
            .SingleOrDefaultAsync(x => x.Id == productId);
        if (product == null) return false;

        var user = await _context.ApiUsers
            .Include(u => u.FavouriteProducts)
            .SingleOrDefaultAsync(x => x.Id == userId);
        if (user == null) return false;

        if (!product.FavouriteBy.Contains(user) && !user.FavouriteProducts.Contains(product)) return false;

        product.FavouriteBy.Remove(user);
        user.FavouriteProducts.Remove(product);

        _context.Update(product);
        _context.Update(user);

        await _context.SaveChangesAsync();

        return true;
    }


    private void UpdateWantedAndUnwantedFromPreference(ProductQueryDTO productQueryDto, ref IQueryable<ProductPOCO> queryable)
    {
        var wanted = productQueryDto.ingredients ?? new List<int>();
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

    public async Task<IEnumerable<bool>> CheckFavourites(IEnumerable<int> productIds, string userId)
    {
        var user = await _context.Users
            .Include(u => u.FavouriteProducts)
            .SingleOrDefaultAsync(x => x.Id == userId) ??
            throw new InGreedIoException("User not found", StatusCodes.Status404NotFound);

        return productIds.Select(p => user.FavouriteProducts.Any(product => product.Id == p));
    }
}
