using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Repository
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
    }
}
