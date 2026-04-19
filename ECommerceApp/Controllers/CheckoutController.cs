using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApp.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutViewModelProvider _checkoutViewModelProvider;

        public CheckoutController(ICheckoutViewModelProvider checkoutViewModelProvider)
        {
            _checkoutViewModelProvider = checkoutViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            var model = await _checkoutViewModelProvider.GetCheckoutViewModel(userId);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            ModelState.Remove("Cart");
            if (ModelState.IsValid)
            {
                var confirmation = await _checkoutViewModelProvider.PlaceOrderAsync(model, userId);
                if (confirmation == null)
                {
                    TempData["Message"] = "Could not place order. Cart may be empty";
                    return RedirectToAction(nameof(CartController.Index), "Cart");
                }

                return RedirectToAction(nameof(Confirmation), new { id = confirmation.OrderId });
            }

            // Repopulate cart summary for redisplay
            var fresh = await _checkoutViewModelProvider.GetCheckoutViewModel(userId);
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
