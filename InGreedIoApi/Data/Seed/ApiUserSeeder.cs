using InGreedIoApi.POCO;
using Microsoft.AspNetCore.Identity;

namespace InGreedIoApi.Data.Seed
{
    public class ApiUserSeeder : IUserSeeder
    {
        private readonly UserManager<ApiUserPOCO> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApiUserSeeder(UserManager<ApiUserPOCO> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var users = new List<(string username, string email, string password, string role)>
            {
                ("user@example.com", "user@example.com", "Password123!", "User"),
                ("admin@example.com", "admin@example.com", "Password123!", "Admin"),
                ("moderator@example.com", "moderator@example.com", "Password123!", "Moderator"),
                ("producer@example.com", "producer@example.com", "Password123!", "Producer")
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

                    var createResult = await _userManager.CreateAsync(apiUser, user.password);
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