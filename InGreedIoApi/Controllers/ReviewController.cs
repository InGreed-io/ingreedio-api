using AutoMapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewRepository.GetAll();
            return Ok(_mapper.Map<ReviewDTO>(reviews));
        }

        [HttpPatch("/{reviewId}/report")]
        public async Task<IActionResult> Report(int reviewId)
        {
            var review = await _reviewRepository.Report(reviewId);
            if (review == null)
            {
                return NotFound("There is no such reviewId");
            }
            return Ok("The review was reported");
        }
    }
}