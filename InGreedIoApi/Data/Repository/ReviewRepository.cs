using AutoMapper;
using InGreedIoApi.Data;
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

        public async Task<IEnumerable<Review>> GetAll()
        {
            var reviewsPOCO = await _context.Reviews.ToListAsync();

            return _mapper.Map<IEnumerable<Review>>(reviewsPOCO);
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
    }
}