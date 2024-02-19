using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Model;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost("transaction/create")]
        public async Task<ApiResponse<CreateTransactionResponse>> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken cancellationToken = default)
        {
            var transactionId = await CreateTicketAsync(request, cancellationToken);

            return new ApiResponse<CreateTransactionResponse>(Model.StatusCode.Success, new CreateTransactionResponse()
            {
                Balance = 0,
                TicketId = request.TicketId,
                TransactionId = transactionId
            });
        }

        private async Task<string> CreateTicketAsync(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            return "TempTransactionId";
        }
    }
}