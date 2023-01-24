using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Abstractions.IServices;
using PersonalFinance.Models.Dtos;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> PostPurchase([FromBody] TransactionCreateDto transaction)
        {
            var response = await _transactionService.PostTransactionAsync(transaction);

            return Ok(response);
        }
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionsByCategoryId(int categoryId, [FromQuery]TransactionQuery query)
        {
            var response = await _transactionService.GetTransactionsByCategoryId(categoryId,query);

            return Ok(response);
        }
    }
}
