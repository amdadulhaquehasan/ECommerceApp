using ECommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.DataAccessLayer.Identity
{
    public class IdentityRoleSeeder
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public IdentityRoleSeeder(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var roles = Enum.GetNames<AppRole>();
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }
        }
    }
}
