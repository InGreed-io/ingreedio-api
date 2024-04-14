using AutoMapper;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.DTO;
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

        [HttpGet]
        public async Task<IActionResult> Details([FromQuery] string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return Ok(_mapper.Map<ApiUserDTO>(user));
        }

        [HttpGet("preferences")]
        public async Task<IActionResult> GetPreferences([FromQuery] string userId)
        {
            var preferences = await _userRepository.GetPreferences(userId);
            return Ok(_mapper.Map<IEnumerable<PreferenceDTO>>(preferences));
        }
    }
}