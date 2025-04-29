using Backend.Model;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly IAuthService _authService;

        public TransactionController(ITransactionService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet]
        [Authorize]
        [Route("api/balance")]
        public async Task<ActionResult<int>> Balance()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            var userId = await _authService.GetUserIdFromToken(token);

            if (userId == null)
            {
                return Unauthorized("Invalid token.");
            }

            var result = await _service.GetBalance(userId.Value);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("api/transactions")]
        public async Task<ActionResult<Transaction[]>> Transactions()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            var userId = await _authService.GetUserIdFromToken(token);

            if (userId == null)
            {
                return Unauthorized("Invalid token.");
            }

            var result = await _service.GetTransactions(userId.Value);

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("api/transaction")]
        public async Task<IActionResult> TransactionsPost([FromBody] CreateTransactionRequest request)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            var userId = await _authService.GetUserIdFromToken(token);

            if (userId == null)
            {
                return Unauthorized("Invalid token.");
            }

            var result = await _service.CreateTransaction(request, userId.Value);
            if (!result)
            {
                return BadRequest("Transaction failed.");
            }

            return Ok("Transaction created successfully.");
        }
    }

    public class CreateTransactionRequest(int amount, bool isWithdrawal)
    {
        public int Amount { get; set; } = amount;
        public bool IsWithdrawal { get; set; } = isWithdrawal;
    }
}
