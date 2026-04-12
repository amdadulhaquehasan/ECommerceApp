using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.DataAccessLayer.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
