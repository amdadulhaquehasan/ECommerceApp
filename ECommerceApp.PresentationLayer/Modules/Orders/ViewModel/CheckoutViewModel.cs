using ECommerceApp.PresentationLayer.Modules.Carts.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.PresentationLayer.Modules.Orders.ViewModel
{
    public class CheckoutViewModel
    {
        public CartViewModel Cart { get; set; } = null!;

        
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shipping address is required")]
        [StringLength(500)]
        [Display(Name = "Shipping Address")]
        public string ShipAddress { get; set; } = string.Empty;
    }
}
