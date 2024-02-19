using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Api.Interface;
using PaymentGateway.Api.Model;
using PaymentGateway.Api.Service;
using PaymentGateway.Entities;
using PaymentGateway.Entities.Enum;

namespace PaymentGateway.Test
{
    public class ApiControllerTest
    {
        private readonly ILogger<TransactionService> _loggerTransactionService;
        private readonly ILogger<ApiController> _logger;
        private readonly ISqlAccessor _sqlAccessor;

        public ApiControllerTest()
        {
            _logger = Substitute.For<ILogger<ApiController>>();
            _loggerTransactionService = Substitute.For<ILogger<TransactionService>>();
            _sqlAccessor = Substitute.For<ISqlAccessor>();
        }

        [Fact]
        public async Task CreateTransaction_Test()
        {
            var transactionService = new TransactionService(_sqlAccessor, _loggerTransactionService);
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
            var cancelToken = CancellationToken.None;
            var controller = new ApiController(transactionService, _logger);
            var response = await controller.CreateTransactionAsync(request, cancelToken);

            await _sqlAccessor.Received().AddTransactionAsync(
                Arg.Is<Transaction>(a =>
                    a.MerchantTransactionId == request.TicketId &&
                    a.ProviderTransactionId == string.Empty &&
                    a.Provider == Provider.EeziePay &&
                    a.TokenId == request.TokenId &&
                    a.Status == TransactionStatus.Pending &&
                    a.Amount == request.Amount &&
                    a.BankCode == request.BankCode &&
                    a.PlayerId == request.PlayerCardNumber &&
                    a.PlayerRealName == request.PlayerRealName &&
                    a.PlayerCardNumber == request.PlayerCardNumber &&
                    a.CreatedUser == request.PlayerId &&
                    a.UpdatedUser == request.PlayerId),
                Arg.Is(cancelToken));

            response.Status.Should().Be(StatusCode.Success);
            response.Data.Should().NotBeNull();
            response.Data.TicketId.Should().Be(request.TicketId);
            response.Data.TransactionId.Should().NotBeNullOrWhiteSpace();

            //response.Data.Balance.Should().Be(1000 - request.Amount);
        }

        //TODO: add test case - transaction already exist before add it
    }
}