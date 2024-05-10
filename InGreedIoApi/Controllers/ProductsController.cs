using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InGreedIoApi.Model.Exceptions;
using InGreedIoApi.Utils.Pagination;

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

    [Paginated]
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryDTO productQueryDto)
    {
        var products = await _productRepository.GetAll(productQueryDto);
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var product = await _productRepository.GetProduct(productId);
        if (product == null)
        {
            return NotFound("the product id is incorrect");
        }

        var productDto = _mapper.Map<ProductDTO>(product);
        return Ok(productDto);
    }

    [Paginated]
    [HttpGet("{productId}/reviews")]
    public async Task<IActionResult> GetProductReviews(int productId, int pageIndex = 0, int pageSize = 10)
    {
        var reviews = await _productRepository.GetReviews(productId, pageIndex, pageSize);
        return Ok(reviews);
    }

    [Authorize]
    [HttpPost("{productId}/reviews")]
    public async Task<IActionResult> AddProductReview(int productId, [FromBody] ReviewUpdateDTO reviewDto)
    {
        if (reviewDto.Rating < 1 || reviewDto.Rating > 5)
            throw new InGreedIoException("Rating should be from interval [1;5].");

        var userId = User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

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