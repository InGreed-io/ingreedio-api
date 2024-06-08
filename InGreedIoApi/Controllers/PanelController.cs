using InGreedIoApi.DTO;
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
    [Authorize(Roles = "Moderator,Admin")]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(string? emailQuery, int pageIndex = 0, int pageSize = 10) 
    {
        var users = await _userRepository.GetUsers(emailQuery, pageIndex, pageSize);
        return Ok(users);
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

    [Authorize]
    [Authorize(Roles = "Moderator,Admin")]
    [HttpPatch("users/{userId}/deactivate")]
    public async Task<IActionResult> Lock(string userId) 
    {
        await _userRepository.LockUser(userId);
        var user = await _userRepository.GetUserById(userId);
        var userDTO = _mapper.Map<ApiUserListItemDTO>(user);
        userDTO.Role = await _userRepository.GetRole(userId);
        return Ok(userDTO);
    }

    [Authorize]
    [Authorize(Roles = "Moderator,Admin")]
    [HttpPatch("users/{userId}/activate")]
    public async Task<IActionResult> Unlock(string userId) 
    {
        await _userRepository.UnlockUser(userId);
        var user = await _userRepository.GetUserById(userId);
        var userDTO = _mapper.Map<ApiUserListItemDTO>(user);
        userDTO.Role = await _userRepository.GetRole(userId);
        return Ok(userDTO);
    }
}
