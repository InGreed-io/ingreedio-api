using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Utils.Pagination;
using System.Threading.Tasks;

namespace InGreedIoApi.Data.Repository.Interface;

public interface IProductRepository
{
    public Task<IPage<ReviewDTO>> GetReviews(int productId, int pageIndex, int pageSize);

    public Task<Review> AddReview(int productId, string userId, string content, float rating);

    public Task<IPage<ProductDTO>> GetAll(ProductQueryDTO productQueryDto);

    public Task<Product> GetProduct(int productId);

    public Task<bool> Create(CreateProductDTO createProductDto, string Id);

    public Task<bool> Update(UpdateProductDTO updateProductDTO, int productId);

    public Task<bool> Delete(int productId);

    public Task<bool> AddToFavourites(int productId, string userId);

    public Task<bool> RemoveFromFavourites(int productId, string userId);
}