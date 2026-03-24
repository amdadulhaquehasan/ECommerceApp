using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CartController : BaseCartController
    {
        public CartController(ICartViewModelProvider cartViewModelProvider)
            : base(cartViewModelProvider)
        {
        }

        public IActionResult Index()
        {
            var viewModel = _cartViewModelProvider.GetCartViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            if (quantity < 1)
            {
                quantity = 1;
            }

            _cartViewModelProvider.AddItem(productId, productName, unitPrice, quantity);

            TempData["CartMessage"] = $"{productName} has been added to your cart.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int productId, int quantity)
        {
            _cartViewModelProvider.UpdateQuantity(productId, quantity);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            _cartViewModelProvider.RemoveItem(productId);

            return RedirectToAction(nameof(Index));
        }
    }
}