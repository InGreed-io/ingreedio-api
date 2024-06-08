using InGreedIoApi.Data.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace InGreedIoApi.Utils.ActiveUserAuthorization 
{
    public class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
    {
        private readonly IUserRepository _userRepository;

        public ActiveUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
        {
            var userId = context.User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId)) return;

            var isLocked = await _userRepository.IsUserLocked(userId);
            if (!isLocked) context.Succeed(requirement);
        }
    }
}
