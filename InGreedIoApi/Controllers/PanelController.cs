﻿using InGreedIoApi.DTO;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using InGreedIoApi.Utils.Pagination;
using InGreedIoApi.Model.Exceptions;

namespace InGreedIoApi.Controllers;

[TypeFilter<InGreedIoExceptionFilter>]
[Route("/api/[controller]/")]
[ApiController]
public class PanelController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public PanelController(IProductRepository productRepository, IReviewRepository reviewRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    [Authorize]
    [Authorize(Roles = "Producer,Admin")]
    [HttpPost("products")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDto)
    {
        var userId = User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var product = await _productRepository.Create(createProductDto, userId);
        if (product == null) return BadRequest("Could not add product");

        return CreatedAtAction(
            nameof(Details),
            new { productId = product.Id },
            _mapper.Map<ProductDTO>(product)
        );
    }

    [Authorize]
    [Authorize(Roles = "Producer,Admin")]
    [HttpPatch("products/{productId}")]
    public async Task<IActionResult> Update(int productId, [FromBody] UpdateProductDTO updateProductDTO)
    {
        var userId = User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var product = User.IsInRole("Producer")
            ? await _productRepository.Update(updateProductDTO, productId, userId)
            : await _productRepository.Update(updateProductDTO, productId);

        return Ok(_mapper.Map<ProductDTO>(product));
    }

    [Authorize]
    [Authorize(Roles = "Producer,Admin")]
    [HttpDelete("products/{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        var userId = User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        if (User.IsInRole("Producer"))
        {
            await _productRepository.Delete(productId, userId);
        }
        else
        {
            await _productRepository.Delete(productId);
        }

        return NoContent();
    }

    [Authorize]
    [Authorize(Roles = "Producer,Admin,Moderator")]
    [HttpGet("products/{productId}")]
    public async Task<IActionResult> Details(int productId)
    {
        var userId = User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var product = await _productRepository.GetProduct(productId);

        if (product == null) return NotFound("Could not find product.");
        if (User.IsInRole("Producer") && product.ProducerId != userId)
            return StatusCode(StatusCodes.Status403Forbidden);

        return Ok(_mapper.Map<ProductDetailsDTO>(product));
    }

    [Paginated]
    [Authorize]
    [Authorize(Roles = "Producer,Admin,Moderator")]
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryDTO productQueryDto)
    {
        var userId = User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var products = User.IsInRole("Producer")
            ? await _productRepository.GetAll(productQueryDto, userId)
            : await _productRepository.GetAll(productQueryDto);

        return Ok(products);
    }

    [Paginated]
    [Authorize]
    [Authorize(Roles = "Producer,Admin")]
    [HttpGet("reviews/reported")]
    public async Task<IActionResult> GetReportedReviews(int pageIndex = 0, int pageSize = 10)
    {
        var reviews = await _reviewRepository.GetReported(pageIndex, pageSize);
        return Ok(reviews);
    }

    [Authorize]
    [Authorize(Roles = "Producer,Admin")]
    [HttpDelete("reviews/{reviewId}")]
    public async Task<IActionResult> DeleteReview(int reviewId)
    {
        await _reviewRepository.Delete(reviewId);
        return NoContent();
    }
}
