using InGreedIoApi.POCO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace InGreedIoApi.Data.Seed
{
    public class ApiUserSeeder : IUserSeeder
    {
        private readonly UserManager<ApiUserPOCO> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public ApiUserSeeder(UserManager<ApiUserPOCO> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            var users = new List<(string username, string email, string role)>
            {
                ("user@example.com", "user@example.com", "User"),
                ("admin@example.com", "admin@example.com", "Admin"),
                ("moderator@example.com", "moderator@example.com", "Moderator"),
                ("producer@example.com", "producer@example.com", "Producer")
            };

            foreach (var user in users)
            {
                if (await _userManager.FindByNameAsync(user.username) == null)
                {
                    var apiUser = new ApiUserPOCO
                    {
                        UserName = user.username,
                        Email = user.email
                    };
                    string userPassword = _configuration[$"UserSecrets:{user.username}:Password"];
                    var createResult = await _userManager.CreateAsync(apiUser, userPassword);
                    if (createResult.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync(user.role))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(user.role));
                        }

                        await _userManager.AddToRoleAsync(apiUser, user.role);
                    }
                }
            }
        }
    }
}