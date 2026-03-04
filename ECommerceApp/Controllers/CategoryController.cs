using ECommerceApp.BusinessLayer.Exceptions;
using ECommerceApp.PresentationLayer.Modules.Categories.Interface;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModel;
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

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryViewModelProvider.GetAllAsync();
            return View(categories);
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
                return View(category);
            }

            try
            {   
                await _categoryViewModelProvider.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch(InvalidUserInputException ex)
            {
                ModelState.AddModelError(nameof(CategoryCreateViewModel.Name), ex.Message);
                return View(category);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _categoryViewModelProvider.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await _categoryViewModelProvider.UpdateAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputException ex)
            {
                ModelState.AddModelError(nameof(CategoryEditViewModel.Name), ex.Message);
                return View(viewModel);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _categoryViewModelProvider.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _categoryViewModelProvider.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            bool isDeleted = await _categoryViewModelProvider.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
