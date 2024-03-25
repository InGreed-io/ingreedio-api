using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Repository;
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
        Console.WriteLine(productQueryDto.query);
        return Ok();
    }
}