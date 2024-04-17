using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.POCO;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace InGreedIoApi.Controllers;

[Route("/api/[controller]/")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUserPOCO> _userManager;

    public ProductsController(IProductRepository productRepository, IMapper mapper, UserManager<ApiUserPOCO> userManager)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryDTO productQueryDto)
    {
        if (ModelState.IsValid) return BadRequest("Invalid ModelState");

        var products = await _productRepository.GetAll(productQueryDto);
        return Ok(products);
    }

    [HttpGet("{productId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetProductReviews(int productId, int page = 0, int limit = 10) {
        var reviews = await _productRepository.GetReviews(productId, page, limit);
        return Ok(_mapper.Map<IEnumerable<ReviewDTO>>(reviews));
    }

    [HttpPost("{productId}/reviews")]
    public async Task<IActionResult> AddProductReview(int productId, [FromBody] ReviewUpdateDTO reviewDto) {
        var authenticatedUser = await _userManager.GetUserAsync(User);
        if (authenticatedUser == null) 
            return Unauthorized();

        var newReview = await _productRepository.AddReview(
            productId, authenticatedUser.Id, reviewDto.Content, reviewDto.Rating
        );
        return CreatedAtAction(
            nameof(ReviewController), 
            nameof(ReviewController.GetSingle), 
            new { reviewId = newReview.Id }, 
            _mapper.Map<ReviewDTO>(newReview)
        );
    }
}
