using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
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

            return _mapper.Map<Review>(reviewPOCO);
        }

        public async Task<Review?> Update(int reviewId, ReviewUpdateDTO reviewUpdateDto)
        {
            var reviewPOCO = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (reviewPOCO == null)
            {
                return null;
            }

            reviewPOCO.Text = reviewUpdateDto.Content;
            reviewPOCO.Rating = reviewUpdateDto.Rating;
            await _context.SaveChangesAsync();

            return _mapper.Map<Review>(reviewPOCO);
        }

        public async Task<IEnumerable<Review>> GetForProduct(int productId)
        {
            var reviewsPOCO = await _context.Reviews.Where(x => x.ProductId == productId).ToListAsync();

            if (reviewsPOCO == null)
            {
                return null;
            }

            return reviewsPOCO.Select(x => _mapper.Map<Review>(x));
        }
    }
}