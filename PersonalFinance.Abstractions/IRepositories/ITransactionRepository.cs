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
    public interface ITransactionRepository
    {
        Task<ServerResponseSuccess<bool>> PostTransactionAsync(Transaction transaction);
        Task<ServerResponseSuccess<IEnumerable<Transaction>>> GetListOfTransactionByCategoryId(int categoryId, TransactionQuery query);
        Task<ServerResponseSuccess<bool>> ChangeTransactionsCategory(int transactionId, int uncategrorizedCategoryId);
    }
}
