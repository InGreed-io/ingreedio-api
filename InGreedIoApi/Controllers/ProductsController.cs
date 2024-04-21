using AutoMapper;
using InGreedIoApi.Services;
using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers;

[Route("/api/[controller]/")]
[ApiController]
public class ProductsController : ControllerBase
{
  private readonly IProductRepository _productRepository;
  private readonly IPaginationService _paginationService;
  private readonly IMapper _mapper;

  public ProductsController(IProductRepository productRepository, IMapper mapper, IPaginationService paginationService)
  {
    _productRepository = productRepository;
    _paginationService = paginationService;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<IActionResult> GetProducts([FromQuery] ProductQueryDTO productQueryDto)
  {
    if (!ModelState.IsValid) return BadRequest("Invalid ModelState");

    var products = await _productRepository.GetAll(productQueryDto);

    var paginatedResult = await _paginationService.Paginate<Product>(
        products, productQueryDto.limit, productQueryDto.page);
    return Ok(paginatedResult);
  }
}
