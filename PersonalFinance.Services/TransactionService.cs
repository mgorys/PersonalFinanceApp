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
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryService _categoryService;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository, ICategoryService categoryService)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _categoryService = categoryService;
        }
        public async Task<bool> PostTransactionAsync(TransactionCreateDto transactionDto)
        {
            var categoryId = await _categoryService.GetCategoryIdByNameAsync(transactionDto.CategoryName);
            transactionDto.CategoryId = categoryId;
            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.PutTime = DateTime.Now;
            var response = await _transactionRepository.PostTransactionAsync(transaction);
            if (response.Success == false)
                throw new NotFoundException("Sorry something went wrong");
            return response.DataFromServer;
        }
        public async Task<ServerResponseSuccess<IEnumerable<TransactionDto>>> GetTransactionsByCategoryId(int categoryId, TransactionQuery query)
        {
            int resultCount = await _transactionRepository.GetTransactionsCount(categoryId, query);
            double count = (double)resultCount / (double)query.PageSize;
            int pagesCount = (int)Math.Ceiling(count);
            query.Page ??= 1;
            if (query.Page < 1 || pagesCount < query.Page)
                throw new BadRequestException("Sorry, page has been not found");
            var resultDto = new ServerResponseSuccess<IEnumerable<TransactionDto>>();
            var result = await _transactionRepository.GetListOfTransactionByCategoryId(categoryId,query);
            if (result.Success == false)
                throw new NotFoundException("Sorry, entities have been not found");
            resultDto.DataFromServer = _mapper.Map<IEnumerable<TransactionDto>>(result.DataFromServer);
            resultDto.PagesCount = pagesCount;
            resultDto.Success = result.Success;
            return resultDto;
        }
        public async Task<ServerResponseSuccess<IEnumerable<int>>> GetArrayOfMonthlyTransactionsByCategoryId(int categoryId)
        {
            var resultDto = new ServerResponseSuccess<IEnumerable<int>>();
            int[] monthlyArray = new int[12];
            for (int month = 1; month < 13; month++)
            {
            var result = await _transactionRepository.GetAmountOfMonthOfTransactionByCategoryId(categoryId, month);
                monthlyArray[month-1] += (int)result;
            }
            resultDto.DataFromServer = monthlyArray;
            resultDto.Success = true;
            return resultDto;
        }
    }
}
