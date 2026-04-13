using ECommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ECommerceApp.DataAccessLayer.Identity
{
    public class IdentityRoleSeeder
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityRoleSeeder(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

            // Seed a default super admin user
            const string saEmail = "superadmin@gmail.com";
            const string saPassword = "Super@1234";

            var saUser = await _userManager.FindByEmailAsync(saEmail);
            if (saUser == null)
            {
                saUser = new ApplicationUser
                {
                    UserName = saEmail,
                    Email = saEmail,
                    FullName = "Super Admin"
                };

                var result = await _userManager.CreateAsync(saUser, saPassword);
                if (!result.Succeeded)
                {
                    return;
                }

                var roleResult = await _userManager.AddToRoleAsync(saUser, AppRole.SuperAdmin.ToString());
                if (!roleResult.Succeeded)
                {
                    return;
                }

                var claimResult = await _userManager.AddClaimAsync(saUser, new Claim("Fullname", saUser.FullName));
                if (!claimResult.Succeeded)
                {
                    return;
                }
            }
        }
    }
}
