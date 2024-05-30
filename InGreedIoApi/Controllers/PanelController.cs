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
    [Authorize(Roles = "Producer,Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDto)
    {
        if (await _productRepository.Create(createProductDto, User.FindFirst("Id")?.Value))
            return Ok("the product has been created");
        return BadRequest("the product has not been added");
    }

    [Authorize]
    [Authorize(Roles = "Producer,Admin")]
    [HttpPatch("{productId}")]
    public async Task<IActionResult> Update(int productId, [FromBody] UpdateProductDTO updateProductDTO)
    {
        if (await _productRepository.Update(updateProductDTO, productId))
            return Ok("the product has been updated");
        return BadRequest("the product has not been updated");
    }

    [Authorize]
    [Authorize(Roles = "Producer,Admin")]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        if (await _productRepository.Delete(productId))
            return Ok("the product has been deleted");
        return BadRequest("the product has not been deleted");
    }

    [Authorize(Roles = "Producer,Admin,Moderator")]
    [HttpGet("products/{productId}")]
    public async Task<IActionResult> Details(int productId)
    {
        var userId = User.FindFirst("Id")?.Value;
        var product = await _productRepository.GetProductPermission(productId, userId);
        if (product == null)
            return BadRequest("the product has not been found");
        return Ok(product);
    }
}