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
        private readonly UserManager<ApiUserPOCO> _userManager;
        public UserController(IUserRepository userRepository, IMapper mapper, UserManager<ApiUserPOCO> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userHttp = await _userManager.GetUserAsync(User);
            if(userHttp == null)
            {
                return Unauthorized("The user is unauthorized");
            }
            var user = await _userRepository.GetUserById(userHttp.Id);
            return Ok(_mapper.Map<ApiUserDTO>(user));
        }

        [Authorize]
        [HttpGet("preferences")]
        public async Task<IActionResult> GetPreferences()
        {
            var userHttp = await _userManager.GetUserAsync(User);
            if (userHttp == null)
            {
                return Unauthorized("The user is unauthorized");
            }
            var preferences = await _userRepository.GetPreferences(userHttp.Id);
            return Ok(_mapper.Map<IEnumerable<PreferenceDTO>>(preferences));
        }
    }
}