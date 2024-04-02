using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
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
        public async Task<IActionResult> GetAll([FromQuery] string userId)
        {
            var reviews = await _reviewRepository.GetAll(userId);
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

        [HttpPatch("/{reviewId}/rate")]
        public async Task<IActionResult> Rate(int reviewId, [FromBody] float reviewRating)
        {
            if (reviewRating < 1 || reviewRating > 5)
            {
                return BadRequest("The rating should be from [1;5]");
            }
            var review = await _reviewRepository.Rate(reviewId, reviewRating);
            if (review == null)
            {
                return NotFound("There is no such reviewId");
            }
            return Ok("The review was rated");
        }

        [HttpPut("/{reviewId}")]
        public async Task<IActionResult> Update(int reviewId, [FromBody] ReviewUpdateDTO reviewUpdateDto)
        {
            if (reviewUpdateDto.Rating < 1 || reviewUpdateDto.Rating > 5)
            {
                return BadRequest("The rating should be from [1;5]");
            }
            var review = await _reviewRepository.Update(reviewId, reviewUpdateDto);
            if (review == null)
            {
                return NotFound("There is no such reviewId");
            }
            return Ok("The review was updated");
        }
    }
}