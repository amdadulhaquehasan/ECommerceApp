using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Carts.Interfaces;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
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
