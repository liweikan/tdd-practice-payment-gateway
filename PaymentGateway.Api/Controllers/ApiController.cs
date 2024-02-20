using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Api.Interface;
using PaymentGateway.Api.Model;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<ApiController> _logger;

        public ApiController(
            ITransactionService transactionService,
            ILogger<ApiController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost("transaction/create")]
        public async Task<ApiResponse<CreateTransactionResponse>> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken cancellationToken = default)
        {
            var transactionId = await _transactionService.CreateTransactionAsync(request, cancellationToken);

            return new ApiResponse<CreateTransactionResponse>(Model.StatusCode.Success, new CreateTransactionResponse()
            {
                Balance = 0,
                TicketId = request.TicketId,
                TransactionId = transactionId.ToString()
            });
        }
    }
}