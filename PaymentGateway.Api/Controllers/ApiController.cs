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
        public Task<ApiResponse<CreateTransactionResponse>> CreateTransactionAsync(CreateTransactionRequest createTransactionRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}