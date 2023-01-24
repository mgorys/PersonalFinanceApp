using PersonalFinance.Models;
using PersonalFinance.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Abstractions.IServices
{
    public interface ICategoryService
    {
        Task<ServerResponseSuccess<IEnumerable<CategoryDto>>> GetCategoriesAsync();
        Task<int> GetCategoryIdByNameAsync(string name);
        Task<bool> EditCategoryByIdAsync(CategoryEditDto data);
        Task<bool> DeleteCategoryByIdAsync(int categoryId);
        Task<bool> CreateCategoryAsync(CategoryCreateDto categoryCreate);
        Task<bool> RegisterCreateCategories(int userId);
    }
}
