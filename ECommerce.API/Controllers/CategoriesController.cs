using ECommerceApp.PresentationLayer.Modules.Categories.Interface;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryViewModelProvider categoryViewModelProvider, ILogger<CategoriesController> logger)
        {
            _categoryViewModelProvider = categoryViewModelProvider;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryViewModelProvider.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var category = await _categoryViewModelProvider.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CategoryCreateViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _categoryViewModelProvider.AddAsync(category);
                _logger.LogInformation("Creating category: {CategoryName}", category.Name);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding category: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while adding the category.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryEditViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest("Category ID mismatch.");
            }

            var existingCategory = await _categoryViewModelProvider.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryViewModelProvider.UpdateAsync(category);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category with ID {CategoryId}", id);

                return StatusCode(500, "An error occurred while updating the category.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var existingCategory = await _categoryViewModelProvider.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            try
            {
                await _categoryViewModelProvider.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {CategoryId}", id);
                return StatusCode(500, "An error occurred while deleting the category.");
            }
        }
    }
}
