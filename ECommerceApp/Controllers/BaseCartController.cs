using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceApp.Controllers
{
    public class BaseCartController : Controller
    {
        protected readonly ICartViewModelProvider _cartViewModelProvider;

        public BaseCartController(ICartViewModelProvider cartViewModelProvider)
        {
            _cartViewModelProvider = cartViewModelProvider;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = _cartViewModelProvider.GetCartViewModel();
            ViewBag.CartItemCount = cart.TotalItems;

            base.OnActionExecuting(context);
        }
    }
}