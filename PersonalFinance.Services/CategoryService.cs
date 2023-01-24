using AutoMapper;
using PersonalFinance.Abstractions.IRepositories;
using PersonalFinance.Abstractions.IServices;
using PersonalFinance.Entities;
using PersonalFinance.Infrastructure.Exceptions;
using PersonalFinance.Models;
using PersonalFinance.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserContextService _userContextService;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper,
            ITransactionRepository transactionRepository, IUserContextService userContextService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _userContextService = userContextService;
        }
        public async Task<ServerResponseSuccess<IEnumerable<CategoryDto>>> GetCategoriesAsync()
        {
            var userId = _userContextService.GetUserId;
            var resultDto = new ServerResponseSuccess<IEnumerable<CategoryDto>>();
            var result = await _categoryRepository.GetCategoriesAsync(userId);
            if (result.Success == false)
                throw new NotFoundException("Sorry, entities have been not found");
            resultDto.DataFromServer = _mapper.Map<IEnumerable<CategoryDto>>(result.DataFromServer);
            foreach (var item in resultDto.DataFromServer)
            {
                var amount = await _categoryRepository.GetAmountOfCategoryByIdAsync(item.Id);
                if (amount.Success == false)
                    throw new NotFoundException("Sorry, entities have been not found");
                item.Amount = amount.DataFromServer;
            }
            resultDto.Success = result.Success;
            return resultDto;
        }
        public async Task<int> GetCategoryIdByNameAsync(string name)
        {
            var result = await _categoryRepository.GetCategoryIdByNameOrIdAsync(name, null);
            if (result.Success == false)
                throw new NotFoundException("Sorry, entities have been not found");
            return result.DataFromServer;
        }
        public async Task<bool> EditCategoryByIdAsync(CategoryEditDto categoryCreate)
        {
            var result = await _categoryRepository.GetCategoryIdByNameOrIdAsync(null, categoryCreate.Id);
            if (result.Success == false)
                throw new NotFoundException("Sorry, entities have been not found");
            var category = _mapper.Map<Category>(categoryCreate);
            var resultDto = await _categoryRepository.EditCategoryByIdAsync(category);
            return resultDto.DataFromServer;
        }
        public async Task<bool> CreateCategoryAsync(CategoryCreateDto categoryCreate)
        {
            var category = _mapper.Map<Category>(categoryCreate);
            if (category.UserId == 0)
                category.UserId = _userContextService.GetUserId;
            var result = await _categoryRepository.CreateCategoryAsync(category);
            return result;
        }
        public async Task<bool> RegisterCreateCategories(int userId)
        {

            var newExp = new Category() { Name = "Uncategorized Expenditure", 
                Type = TypeOfCategory.EXPENDITURE, UserId = userId, Color = "#37FF00", Default=true };

            var expR = await _categoryRepository.CreateCategoryAsync(newExp);
            var newInc = new Category() { Name = "Uncategorized Income", 
                Type = TypeOfCategory.INCOME, UserId = userId, Color = "#EE00FF", Default = true };

            var incR = await _categoryRepository.CreateCategoryAsync(newInc);
            if (expR && incR)
                return true;
            else return false;
        }
        public async Task<bool> DeleteCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category.Success == false)
                throw new NotFoundException("Sorry, entity has been not found");
            if (category.DataFromServer.Default == true)
                throw new BadRequestException("You cannot delete default categories");
            var resultList = await _transactionRepository.GetListOfTransactionByCategoryId(category.DataFromServer.Id, null);
            var categoryUncategorizedId = await _categoryRepository.GetCategoryIdByNameOrIdAsync(category.DataFromServer.Type ==
                        TypeOfCategory.EXPENDITURE ? "Uncategorized Expenditure" : "Uncategorized Income", null);
            if (resultList.DataFromServer != null)
            {
                foreach (var item in resultList.DataFromServer)
                {
                    await _transactionRepository.ChangeTransactionsCategory(item.Id, categoryUncategorizedId.DataFromServer);
                }
            }
            var result = await _categoryRepository.DeleteCategoryByIdAsync(category.DataFromServer.Id);
            if (result.Success == false)
                return false;
            return true;
        }
    }
}
