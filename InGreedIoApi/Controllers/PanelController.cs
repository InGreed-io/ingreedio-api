using InGreedIoApi.DTO;
using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InGreedIoApi.Model;
using System.Threading.Tasks;
using Serilog;
using AutoMapper;
using InGreedIoApi.Utils.Pagination;

namespace InGreedIoApi.Controllers;

[Route("/api/[controller]/")]
[ApiController]
public class PanelController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public PanelController(IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
        _mapper = mapper;
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

    [Authorize]
    [Authorize(Roles = "Producer,Admin,Moderator")]
    [HttpGet("products/{productId}")]
    public async Task<IActionResult> Details(int productId)
    {
        var userId = User.FindFirst("Id")?.Value;

        if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
        {
            userId = "Admin";
        }
        var product = await _productRepository.GetProductPermission(productId, userId);
        if (product == null)
            return BadRequest("the product has not been found");
        return Ok(_mapper.Map<ProductDetailsDTO>(product));
    }

    [Paginated]
    [Authorize]
    [Authorize(Roles = "Moderator,Admin")]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(string? emailQuery, int pageIndex = 0, int pageSize = 10) 
    {
        var users = await _userRepository.GetUsers(emailQuery, pageIndex, pageSize);
        return Ok(users);
    }
}
