using Microsoft.EntityFrameworkCore;
using PersonalFinance.Abstractions.IRepositories;
using PersonalFinance.Entities;
using PersonalFinance.Models;
using PersonalFinance.Models.Dtos;
using PersonalFinance.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PFDbContext _dbContext;

        public CategoryRepository(PFDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ServerResponseSuccess<IEnumerable<Category>>> GetCategoriesAsync(int userId)
        {
            var response = new ServerResponseSuccess<IEnumerable<Category>>();
            var result = await _dbContext.Categories.Where(x=>x.UserId == userId)
                .ToListAsync();

            if (result == null)
            {
                response.Success = false;
                return response;
            }
            else
            {
                response.Success = true;
            }
            response.DataFromServer = result;
            return response;
        }
        public async Task<ServerResponseSuccess<int>> GetAmountOfCategoryByIdAsync(int id)
        {
            var response = new ServerResponseSuccess<int>();
            var result = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                response.Success = false;
                return response;
            }
            else
            {
                var transactionsList = await _dbContext.Transactions.Include(x => x.Category).Where(y => y.Category.Id == result.Id).ToListAsync();
                foreach (var transaction in transactionsList)
                    response.DataFromServer += transaction.Amount;

                response.Success = true;
                return response;
            }
        }
        public async Task<ServerResponseSuccess<int>> GetCategoryIdByNameOrIdAsync(string? name, int? id)
        {
            var response = new ServerResponseSuccess<int>();
            var result = await _dbContext.Categories
                .Where(x => x.Name == name || x.Id == id).FirstOrDefaultAsync();

            if (result == null)
            {
                response.Success = false;
                return response;
            }
            else
            {
                response.Success = true;
            }
            response.DataFromServer = result.Id;
            return response;
        }
        public async Task<ServerResponseSuccess<Category>> GetCategoryByIdAsync(int id)
        {
            var response = new ServerResponseSuccess<Category>();
            var result = await _dbContext.Categories
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (result == null)
            {
                response.Success = false;
                return response;
            }
            else
            {
                response.Success = true;
            }
            response.DataFromServer = result;
            return response;
        }
        public async Task<ServerResponseSuccess<bool>> EditCategoryByIdAsync(Category category)
        {
            var response = new ServerResponseSuccess<bool>();
            var result = await _dbContext.Categories.Where(x => x.Id == category.Id).FirstOrDefaultAsync();

            if (result == null)
            {
                response.Success = false;
                return response;
            }

            result.Color = category.Color;
            if(result.Default == false)
            result.Name = category.Name;
            await _dbContext.SaveChangesAsync();
            response.Success = true;
            response.DataFromServer = true;
            return response;
        }

        public async Task<ServerResponseSuccess<bool>> DeleteCategoryByIdAsync(int categoryId)
        {
            var response = new ServerResponseSuccess<bool>();
            var result = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);
            if (result == null)
            {
                response.Success = false;
                return response;
            }
            _dbContext.Categories.Remove(result);
            await _dbContext.SaveChangesAsync();
            response.Success = true;
            return response;
        }
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
