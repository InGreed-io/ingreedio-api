using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InGreedIoApi.Model.Exceptions;

namespace InGreedIoApi.Controllers;

[TypeFilter<InGreedIoExceptionFilter>]
[Route("/api/[controller]/")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryDTO productQueryDto)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid ModelState");

        var products = await _productRepository.GetAll(productQueryDto);
        return Ok(products);
    }

    [HttpGet("{productId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetProductReviews(int productId, int page = 0, int limit = 10)
    {
        var reviews = await _productRepository.GetReviews(productId, page, limit);
        return Ok(_mapper.Map<IEnumerable<ReviewDTO>>(reviews));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("{productId}/reviews")]
    public async Task<IActionResult> AddProductReview(int productId, [FromBody] ReviewUpdateDTO reviewDto)
    {
        var userId = User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        if (reviewDto.Rating < 1 || reviewDto.Rating > 5)
            throw new InGreedIoException("Rating should be from interval [1;5].");

        var newReview = await _productRepository.AddReview(
            productId, userId, reviewDto.Content, reviewDto.Rating
        );
        return CreatedAtAction(
            "GetSingle",
            "Review",
            new { reviewId = newReview.Id },
            _mapper.Map<ReviewDTO>(newReview)
        );
    }
}
