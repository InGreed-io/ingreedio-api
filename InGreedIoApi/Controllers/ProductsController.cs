using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers;

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
}