using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Orders
{
    public class CheckoutViewModelProvider : ICheckoutViewModelProvider
    {
        private readonly ICartViewModelProvider _cartViewModelProvider;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutViewModelProvider(IOrderService orderService, ICartService cartService, ICartViewModelProvider cartViewModelProvider)
        {
            _orderService = orderService;
            _cartService = cartService;
            _cartViewModelProvider = cartViewModelProvider;
        }

        public CheckoutViewModel? GetCheckoutViewModel()
        {
            var CartVm = _cartViewModelProvider.GetCartViewModel();
            if (CartVm.Items.Count == 0)
            {
                return null;
            }
            return new CheckoutViewModel
            {
                Cart = CartVm
            };
        }

        public async Task<OrderConfirmViewModel> PlaceOrderAsync(CheckoutViewModel model)
        {
            var order = await _orderService.PlaceOrderAsync(
                model.FirstName,
                model.LastName,
                model.Email,
                model.Phone ?? "",
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
