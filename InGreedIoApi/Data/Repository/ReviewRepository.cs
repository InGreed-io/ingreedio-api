using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Model.Exceptions;
using InGreedIoApi.POCO;
using InGreedIoApi.Utils.Pagination;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Review>> GetAll(string userId)
        {
            var reviewsPOCO = await _context.Reviews.Where(x => x.UserID == userId).ToListAsync();
            return reviewsPOCO.Select(x => _mapper.Map<Review>(x));
        }

        public async Task<Review?> Report(int reviewId)
        {
            var reviewPOCO = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (reviewPOCO == null)
            {
                return null;
            }

            reviewPOCO.ReportsCount++;
            await _context.SaveChangesAsync();

            await _context.Entry(reviewPOCO).Reference(review => review.User).LoadAsync();

            return _mapper.Map<Review>(reviewPOCO);
        }

        public async Task<Review?> Rate(int reviewId, float reviewRating)
        {
            var reviewPOCO = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (reviewPOCO == null)
            {
                return null;
            }

            reviewPOCO.Rating = reviewRating;
            await _context.SaveChangesAsync();

            await _context.Entry(reviewPOCO).Reference(review => review.User).LoadAsync();

            return _mapper.Map<Review>(reviewPOCO);
        }

        public async Task<Review?> Update(int reviewId, ReviewUpdateDTO reviewUpdateDto)
        {
            var reviewPOCO = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (reviewPOCO == null)
            {
                return null;
            }

            reviewPOCO.Text = reviewUpdateDto.Text;
            reviewPOCO.Rating = reviewUpdateDto.Rating;
            await _context.SaveChangesAsync();

            await _context.Entry(reviewPOCO).Reference(review => review.User).LoadAsync();

            return _mapper.Map<Review>(reviewPOCO);
        }

        public async Task<IEnumerable<Review>> GetForProduct(int productId)
        {
            var reviewsPOCO = await _context.Reviews
                .Include(review => review.User)
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.Id).ToListAsync();

            if (reviewsPOCO == null)
            {
                return [];
            }

            return reviewsPOCO.Select(x => _mapper.Map<Review>(x));
        }

        public async Task<Review?> Create(CreateReviewDTO createReviewDto)
        {
            var newReview = new ReviewPOCO
            {
                Text = createReviewDto.Text,
                Rating = createReviewDto.Rating,
                ReportsCount = 0,
                UserID = createReviewDto.UserID,
                ProductId = createReviewDto.ProductId,
            };
            try
            {
                await _context.Reviews.AddAsync(newReview);
                await _context.SaveChangesAsync();

                await _context.Entry(newReview).Reference(review => review.User).LoadAsync();
            }
            catch
            {
                return null;
            }
            return _mapper.Map<Review>(newReview);
        }

        public async Task<IPage<ReportedReviewDTO>> GetReported(int pageIndex, int pageSize)
        {
            return await _context.Reviews
                .Where(review => review.ReportsCount > 0)
                .OrderByDescending(review => review.ReportsCount)
                .ProjectToPageAsync<ReviewPOCO, ReportedReviewDTO>(pageIndex, pageSize, _mapper.ConfigurationProvider);
        }

        public async Task Delete(int reviewId, string? userId = null)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
                throw new InGreedIoException("Could not find review.", StatusCodes.Status404NotFound);

            if (!string.IsNullOrEmpty(userId) && review.UserID != userId)
                throw new InGreedIoException("Could not access review.", StatusCodes.Status403Forbidden);

            _context.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
