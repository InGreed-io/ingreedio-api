using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InGreedIoApi.Controllers;

[Route("/api/[controller]/")]
[ApiController]
public class PanelController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public PanelController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDto)
    {
        if (await _productRepository.Create(createProductDto, User.FindFirst("Id")?.Value))
            return Ok("the product has been created");
        return BadRequest("the product has not been added");
    }
}