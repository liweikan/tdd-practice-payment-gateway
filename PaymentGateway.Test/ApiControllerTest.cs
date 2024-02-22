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
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var controller = new ApiController(transactionService, _logger);
            var cancelToken = CancellationToken.None;
            var request = CreateTransactionRequest();
            var transactionId = Guid.NewGuid();

            MockGetTransactionReturnNull();
            MockAddTransactionSuccess(transactionId);

            var response = await controller.CreateTransactionAsync(request, cancelToken);

            await AssertReceivedParametersAsync(request, cancelToken);
            AssertResponse(response, request, transactionId);
        }

        [Fact]
        public async Task CreateDuplicatedTransaction_Test()
        {
            var transactionService = new TransactionService(_sqlAccessor, _loggerTransactionService);
            var controller = new ApiController(transactionService, _logger);
            var cancelToken = CancellationToken.None;
            var request = CreateTransactionRequest();
            var transactionId = Guid.NewGuid();

            MockGetTransactionByRequest(transactionId, request);

            var response = await controller.CreateTransactionAsync(request, cancelToken);
            AssertResponse(response, request, transactionId);
        }

        private static CreateTransactionRequest CreateTransactionRequest()
        {
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
            return request;
        }

        private void MockAddTransactionSuccess(Guid transactionId)
        {
            _sqlAccessor
                .AddTransactionAsync(Arg.Any<Transaction>(), Arg.Any<CancellationToken>())
                .Returns(transactionId);
        }

        private void MockGetTransactionReturnNull()
        {
            _sqlAccessor
                .GetTransactionAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(default(Transaction));
        }

        private void MockGetTransactionByRequest(Guid transactionId, CreateTransactionRequest request)
        {
            var createDate = DateTimeOffset.UtcNow.AddHours(-1);

            _sqlAccessor
                .GetTransactionAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new Transaction
                {
                    TransactionId = transactionId,
                    MerchantTransactionId = request.TicketId,
                    ProviderTransactionId = string.Empty,
                    Provider = Provider.EeziePay,
                    TokenId = request.TokenId,
                    Status = TransactionStatus.Pending,
                    Amount = request.Amount,
                    BankCode = request.BankCode,
                    PlayerId = request.PlayerCardNumber,
                    PlayerRealName = request.PlayerRealName,
                    PlayerCardNumber = request.PlayerCardNumber,
                    CreatedUser = request.PlayerId,
                    UpdatedUser = request.PlayerId,
                    CreatedDate = createDate,
                    UpdatedDate = createDate
                });
        }

        private async Task AssertReceivedParametersAsync(CreateTransactionRequest request, CancellationToken cancelToken)
        {
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
        }

        private static void AssertResponse(ApiResponse<CreateTransactionResponse> response, CreateTransactionRequest request, Guid transactionId)
        {
            response.Status.Should().Be(StatusCode.Success);
            response.Data.Should().NotBeNull();
            response.Data.TicketId.Should().Be(request.TicketId);
            response.Data.TransactionId.Should().Be(transactionId.ToString());
            //response.Data.Balance.Should().Be(1000 - request.Amount);
        }
    }
}