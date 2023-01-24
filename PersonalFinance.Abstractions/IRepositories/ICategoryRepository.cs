using PersonalFinance.Entities;
using PersonalFinance.Models;
using PersonalFinance.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Abstractions.IRepositories
{
    public interface ICategoryRepository
    {
        Task<ServerResponseSuccess<IEnumerable<Category>>> GetCategoriesAsync(int userId);
        Task<ServerResponseSuccess<int>> GetAmountOfCategoryByIdAsync(int id);
        Task<ServerResponseSuccess<int>> GetCategoryIdByNameOrIdAsync(string? name,int? id);
        Task<ServerResponseSuccess<Category>> GetCategoryByIdAsync(int id);
        Task<ServerResponseSuccess<bool>> EditCategoryByIdAsync(Category category);
        Task<ServerResponseSuccess<bool>> DeleteCategoryByIdAsync(int categoryId);
        Task<bool> CreateCategoryAsync(Category category);
    }
}
