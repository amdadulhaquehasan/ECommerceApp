using AutoMapper;
using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Carts
{
    public class CartViewModelProvider : ICartViewModelProvider
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartViewModelProvider(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public void AddItem(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            _cartService.AddItem(productId, productName, unitPrice, quantity);
        }

        public CartViewModel GetCartViewModel()
        {
            var cart = _cartService.GetCart();
            return _mapper.Map<CartViewModel>(cart);
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
