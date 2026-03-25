using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutViewModelProvider _checkoutViewModelProvider;

        public CheckoutController(ICheckoutViewModelProvider checkoutViewModelProvider)
        {
            _checkoutViewModelProvider = checkoutViewModelProvider;
        }

        public IActionResult Index()
        {
            var model = _checkoutViewModelProvider.GetCheckoutViewModel();
            if (model == null)
            {
                TempData["Message"] = "Your cart is empty";
                return RedirectToAction(nameof(CartController.Index), "Cart");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            ModelState.Remove("Cart");
            if (ModelState.IsValid)
            {
                var confirmation = await _checkoutViewModelProvider.PlaceOrderAsync(model);
                if (confirmation == null)
                {
                    TempData["Message"] = "Could not place order. Cart may be empty";
                    return RedirectToAction(nameof(CartController.Index), "Cart");
                }

                return RedirectToAction(nameof(Confirmation), new { id = confirmation.OrderId });
            }

            // Repopulate cart summary for redisplay
            var fresh = _checkoutViewModelProvider.GetCheckoutViewModel();
            if (fresh != null)
            {
                model.Cart = fresh.Cart;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Confirmation(int id)
        {
            return View(id);
        }
    }
}
