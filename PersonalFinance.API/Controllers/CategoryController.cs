using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Abstractions.IServices;
using PersonalFinance.Models.Dtos;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return Ok(categories);
        }
        [HttpPost("edit")]
        public async Task<ActionResult<bool>> EditCategoryAsync([FromBody] CategoryEditDto categoryEdit)
        {
            var result = await _categoryService.EditCategoryByIdAsync(categoryEdit);

            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<ActionResult<bool>> CreateCategoryAsync([FromBody] CategoryCreateDto categoryCreate)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryCreate);

            return Ok(result);
        }
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<bool>> DeleteCategoryByIdAsync(int categoryId)
        {
            var result = await _categoryService.DeleteCategoryByIdAsync(categoryId);

            return Ok(result);
        }
    }
}
