using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Api.Model;
using PaymentGateway.Api.Service;

namespace PaymentGateway.Test
{
    public class ApiControllerTest
    {
        private readonly ILogger<TransactionService> _loggerTransactionService;
        private readonly ILogger<ApiController> _logger;

        public ApiControllerTest()
        {
            _logger = Substitute.For<ILogger<ApiController>>();
            _loggerTransactionService = Substitute.For<ILogger<TransactionService>>();
        }

        [Fact]
        public async Task CreateTransaction_Test()
        {
            var transactionService = new TransactionService(_loggerTransactionService);
            var request = new CreateTransactionRequest
            {
                TicketId = DateTimeOffset.UtcNow.Millisecond.ToString(),
                PlayerId = "AS12345678",
                PlayerRealName = "Willie",
                PlayerCardNumber = "12387891237389094789",
                Amount = 108,
                TokenId = Guid.NewGuid(),
                BankCode = "MB"
            };
            var controller = new ApiController(transactionService, _logger);
            var response = await controller.CreateTransactionAsync(
                request,
                CancellationToken.None);

            response.Status.Should().Be(StatusCode.Success);
            response.Data.Should().NotBeNull();
            response.Data.TicketId.Should().Be(request.TicketId);
            response.Data.TransactionId.Should().NotBeNullOrWhiteSpace();

            //response.Data.Balance.Should().Be(1000 - request.Amount);
        }
    }
}