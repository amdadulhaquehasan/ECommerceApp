using ECommerceApp.PresentationLayer.Modules.Orders;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private readonly IOrderViewModelProvider _orderViewModelProvider;

        public OrderController(
            IOrderViewModelProvider orderViewModelProvider)
        {
            _orderViewModelProvider = orderViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await _orderViewModelProvider
                .GetCustomerOrdersAsync(userId!);

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await _orderViewModelProvider
                .GetOrderDetailsAsync(
                    id,
                    userId!,
                    false);

            if (model == null)
            {
                return Forbid();
            }

            return View(model);
        }
    }
}
