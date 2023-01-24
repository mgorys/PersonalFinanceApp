using PersonalFinance.Models;
using PersonalFinance.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Abstractions.IServices
{
    public interface ITransactionService
    {
        Task<bool> PostTransactionAsync(TransactionCreateDto transactionDto);
        Task<ServerResponseSuccess<IEnumerable<TransactionDto>>> GetTransactionsByCategoryId(int categoryId, TransactionQuery query);
    }
}
