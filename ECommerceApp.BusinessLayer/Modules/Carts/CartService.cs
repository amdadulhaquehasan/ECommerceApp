using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Carts.Interfaces;
using ECommerceApp.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.BusinessLayer.Modules.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepository, ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public void AddItem(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            var cart = _cartRepository.GetCart();
            var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    UnitPrice = unitPrice,
                    Quantity = quantity
                });
            }
            _cartRepository.SaveCart(cart);
            _logger.LogInformation("Cart with product name {ProductName} saved.", productName);
        }

        public Cart GetCart() => _cartRepository.GetCart();
        

        public void RemoveItem(int productId)
        {
            var cart = _cartRepository.GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                _cartRepository.SaveCart(cart);
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = _cartRepository.GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) return;
            if (quantity <= 0)
            {
                cart.Items.Remove(item);

            }
            else
            {
                item.Quantity = quantity;
            }
            _cartRepository.SaveCart(cart);
        }

        public void ClearCart()
        {
            _cartRepository.ClearCart();
        }

    }
}
