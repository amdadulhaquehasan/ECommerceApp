using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Carts
{
    public class CartViewModelProvider : ICartViewModelProvider
    {
        private readonly ICartService _cartService;

        public CartViewModelProvider(ICartService cartService)
        {
            _cartService = cartService;
        }

        public void AddItem(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            _cartService.AddItem(productId, productName, unitPrice, quantity);
        }

        public CartViewModel GetCartViewModel()
        {
            var cart = _cartService.GetCart();
            return new CartViewModel
            {
                Items = cart.Items.Select(i => new CartItemViewModel
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    LineTotal = i.UnitPrice * i.Quantity
                }).ToList(),
                GrandTotal = cart.GrandTotal,
                TotalItems = cart.TotalItems
            };
        }

        public void RemoveItem(int productId)
        {
            _cartService.RemoveItem(productId);
        }

        public void UpdateQuantity(int productId, int newQuantity)
        {
            _cartService.UpdateQuantity(productId, newQuantity);
        }
    }
}
