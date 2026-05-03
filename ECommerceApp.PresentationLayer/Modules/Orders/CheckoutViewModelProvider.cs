using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.DataAccessLayer.Identity;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.PresentationLayer.Modules.Orders
{
    public class CheckoutViewModelProvider : ICheckoutViewModelProvider
    {
        private readonly ICartViewModelProvider _cartViewModelProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        public CheckoutViewModelProvider(IOrderService orderService,  ICartViewModelProvider cartViewModelProvider, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _cartViewModelProvider = cartViewModelProvider;
            _userManager = userManager;
        }

        public async Task<CheckoutViewModel?> GetCheckoutViewModel(string userId)
        {
            var CartVm = _cartViewModelProvider.GetCartViewModel();
            if (CartVm.Items.Count == 0)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return new CheckoutViewModel
            {
                Cart = CartVm,
                Email = user.Email,
                FullName = user.FullName,
                ShipAddress = user.ShippingAddress
            };
        }

        public async Task<OrderConfirmViewModel> PlaceOrderAsync(CheckoutViewModel model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            user.ShippingAddress = model.ShipAddress;
            await _userManager.UpdateAsync(user);

            var order = await _orderService.PlaceOrderAsync(
                userId,
                model.ShipAddress);

            return new OrderConfirmViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status ?? "Pending"
            };
        }

    }
}
