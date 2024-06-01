using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
using InGreedIoApi.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> Details()
        {
            var userId = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var user = await _userRepository.GetUserById(userId);
            return Ok(_mapper.Map<ApiUserDTO>(user));
        }

        [Authorize]
        [HttpGet("preferences")]
        public async Task<IActionResult> GetPreferences()
        {
            var userId = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var preferences = await _userRepository.GetPreferences(userId);
            return Ok(_mapper.Map<IEnumerable<PreferenceDTO>>(preferences));
        }

        [Authorize]
        [HttpPost("preferences")]
        public async Task<IActionResult> CreatePreference([FromBody] CreatePreferenceDTO args)
        {
            var userId = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var preference = await _userRepository.CreatePreference(userId, args);
            return Ok(_mapper.Map<PreferenceDTO>(preference));
        }
    }
}
