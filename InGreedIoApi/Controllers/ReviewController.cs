using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [Route("/api/[controller]/")]
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

        // Dummy action for CreatedAtAction().
        [HttpGet("{reviewId}")]
        public ActionResult<ReviewDTO> GetSingle(int reviewId)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string userId)
        {
            var reviews = await _reviewRepository.GetAll(userId);
            return Ok(_mapper.Map<ReviewDTO>(reviews));
        }

        [HttpPatch("{reviewId}/report")]
        public async Task<IActionResult> Report(int reviewId)
        {
            var review = await _reviewRepository.Report(reviewId);
            if (review == null)
            {
                return NotFound("There is no such reviewId");
            }
            return Ok("The review was reported");
        }

        [HttpPatch("{reviewId}/rate")]
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

        [HttpPut("{reviewId}")]
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

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetForProduct(int productId)
        {
            var reviewsPOCO = await _reviewRepository.GetForProduct(productId);
            var reviewsDTO = _mapper.Map<IEnumerable<ReviewDTO>>(reviewsPOCO);
            return Ok(reviewsDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDTO createReviewDto)
        {
            var userId = User.FindFirst("Id")?.Value;
            createReviewDto.UserID = userId;
            var isSuccess = await _reviewRepository.Create(createReviewDto);
            return isSuccess ? Ok("the review has been added") : BadRequest("The review has not been added");
        }
    }
}