using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.Model.Exceptions;
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

            try 
            {
                var isLocked = await _userRepository.IsUserLocked(userId);
                if (!isLocked) context.Succeed(requirement);
            }
            catch (InGreedIoException) { return; }
        }
    }
}
