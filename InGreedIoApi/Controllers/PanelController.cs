using AutoMapper;
using InGreedIoApi.DTO;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InGreedIoApi.Model;

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

    [Authorize(Roles = "Producer,Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDto)
    {
        if (await _productRepository.Create(createProductDto, User.FindFirst("Id")?.Value))
            return Ok("the product has been created");
        return BadRequest("the product has not been added");
    }

    [Authorize(Roles = "Producer,Admin")]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        if (await _productRepository.Delete(productId))
            return Ok("the product has been deleted");
        return BadRequest("the product has not been deleted");
    }
}