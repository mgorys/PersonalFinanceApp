using Microsoft.EntityFrameworkCore;
using PersonalFinance.Abstractions.IRepositories;
using PersonalFinance.Entities;
using PersonalFinance.Models;
using PersonalFinance.Models.Dtos;
using PersonalFinance.Models.Dtos.Enums;
using PersonalFinance.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PersonalFinance.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PFDbContext _dbContext;

        public TransactionRepository(PFDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ServerResponseSuccess<bool>> PostTransactionAsync(Transaction transaction)
        {
            var response = new ServerResponseSuccess<bool>();
            
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            response.Success = true;
            response.DataFromServer = true;
            return response;
        }
       
        public async Task<ServerResponseSuccess<bool>> ChangeTransactionsCategory(int transactionId, int uncategrorizedId)
        {
            var response = new ServerResponseSuccess<bool>();

            var transaction = await _dbContext.Transactions.Include(x=>x.Category).FirstOrDefaultAsync(x => x.Id == transactionId);

            if (transaction == null)
            {
                response.Success = false;
                return response;
            }
            transaction.CategoryId = uncategrorizedId;
            await _dbContext.SaveChangesAsync();
            response.Success = true;
            return response;
        }
        public async Task<ServerResponseSuccess<IEnumerable<Transaction>>> GetListOfTransactionByCategoryId(int categoryId, TransactionQuery query)
        {
            var response = new ServerResponseSuccess<IEnumerable<Transaction>>();
            var baseQuery = _dbContext.Transactions.Include(x => x.Category)
                .Where(x => x.CategoryId == categoryId).OrderByDescending(x => x.PutTime);

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columsSelectors = new Dictionary<string, Expression<Func<Transaction, object>>>
                {
                    {nameof(Transaction.Name), r=>r.Name },
                    {nameof(Transaction.Amount), r=>r.Amount },
                    {nameof(Transaction.PutTime), r=>r.PutTime },
                };

                var selectedColumn = columsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }
            var result = await baseQuery
                .Skip((int)query.PageSize * ((int)(query.Page == null ? 1 : query.Page) - 1))
                .Take((int)query.PageSize).ToListAsync();
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
        public async Task<int> GetTransactionsCount(int categoryId, TransactionQuery query)
        {
            var resultCountSearch = await _dbContext.Transactions.Where(x=>x.CategoryId==categoryId)
                .Where(x=>query.Search==null || x.Name.ToLower().Contains(query.Search.ToLower()) || x.Description.ToLower().Contains(query.Search.ToLower()))
                .CountAsync();
            return resultCountSearch;
        }
        public async Task<int> GetAmountOfMonthOfTransactionByCategoryId(int categoryId,int month)
        {
            int amountOfMonth = 0;
            var result = await _dbContext.Transactions.Where(x => x.CategoryId == categoryId).Where(x=>x.PutTime.Month == month).ToListAsync();
            foreach (var item in result)
            {
                amountOfMonth += item.Amount;
            } 
            return amountOfMonth;
        }
    }
}
