using ECommerceApp.PresentationLayer.Modules.Carts.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.PresentationLayer.Modules.Orders.ViewModel
{
    public class CheckoutViewModel
    {
        public CartViewModel Cart { get; set; } = null!;

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Shipping address is required")]
        [StringLength(500)]
        [Display(Name = "Shipping Address")]
        public string ShipAddress { get; set; } = string.Empty;
    }
}
