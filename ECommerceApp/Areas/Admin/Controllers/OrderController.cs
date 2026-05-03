using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderViewModelProvider _orderViewModelProvider;

        public OrderController(
            IOrderService orderService,
            IOrderViewModelProvider orderViewModelProvider)
        {
            _orderService = orderService;
            _orderViewModelProvider = orderViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            var model = orders.Select(o => new OrderConfirmViewModel
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status ?? "Pending"
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _orderViewModelProvider
                .GetOrderDetailsAsync(
                    id,
                    "",
                    true);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDelivered(int id)
        {
            await _orderViewModelProvider
                .MarkDeliveredAsync(id);

            TempData["Message"] = "Order marked as delivered";

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
