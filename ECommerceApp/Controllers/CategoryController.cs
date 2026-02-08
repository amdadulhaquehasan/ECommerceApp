using ECommerceApp.PresentationLayer.Module.Categories.Interface;
using ECommerceApp.PresentationLayer.Module.Categories.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;
        public CategoryController(ICategoryViewModelProvider categoryViewModelProvider)
        {
            _categoryViewModelProvider = categoryViewModelProvider;
        }

        //Get: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel category)
        {
            //Server side Validation
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _categoryViewModelProvider.CreateCategoryAsync(category);

            if (!result.Success)
            {
                category.ErrorMessage = result.ErrorMessage;
            }
            return View(category);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
