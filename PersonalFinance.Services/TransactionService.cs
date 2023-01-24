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
            var resultDto = new ServerResponseSuccess<IEnumerable<TransactionDto>>();
            var result = await _transactionRepository.GetListOfTransactionByCategoryId(categoryId,query);
            if (result.Success == false)
                throw new NotFoundException("Sorry, entities have been not found");
            resultDto.DataFromServer = _mapper.Map<IEnumerable<TransactionDto>>(result.DataFromServer);

            resultDto.Success = result.Success;
            return resultDto;
        }
    }
}
