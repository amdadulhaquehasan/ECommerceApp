using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/StatusCode")]
        [HttpGet]
        public IActionResult StatusCode(int statusCode)
        {
            Response.StatusCode = statusCode;

            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                case 403:
                    return View("Forbidden");
                case 401:
                    return View("Unauthorized");
                default:
                    return View("GenericError");
            }
        }

        [Route("/Error/ServerError")]
        public IActionResult ServerError()
        {
            Response.StatusCode = 500;
            return View("ServerError");
        }
    }
}
