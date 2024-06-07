using InGreedIoApi.POCO;
using InGreedIoApi.DTO;
using InGreedIoApi.Model.Enum;

namespace InGreedIoApi.Services;

public class ProductService : IProductService
{
    public void SortProductQueryDto(ProductQueryDTO productQueryDto, ref IQueryable<ProductPOCO> queryable, IEnumerable<int> wantedProducts)
    {
        if (productQueryDto.SortBy.HasValue)
        {
            queryable = productQueryDto.SortBy switch
            {
                QuerySortType.Featured => queryable.OrderBy(p => p.Featuring != null).ThenBy(p => p.Id),
                QuerySortType.Rating => queryable.OrderBy(p => p.Reviews.Average(r => r.Rating) == null)
                  .ThenByDescending(p => p.Reviews.Average(r => r.Rating)).ThenBy(p => p.Id),
                QuerySortType.RatingCount => queryable.OrderByDescending(p => p.Reviews.Count()).ThenBy(p => p.Id),
                QuerySortType.BestMatch => queryable.OrderBy(p => p.Ingredients.Select(p => p.Id).Union(wantedProducts).Count())
                .ThenByDescending(p => p.Ingredients.Select(p => p.Id).Except(wantedProducts).Count())
                .ThenBy(p => p.Id),
                QuerySortType.Names => queryable.OrderBy(p => p.Name).ThenBy(p => p.Id),
                _ => throw new ArgumentOutOfRangeException("sorty is not defined properly")
            };
        }
        else
        {
            queryable = queryable.OrderBy(p => p.Id);
        }
    }
};
