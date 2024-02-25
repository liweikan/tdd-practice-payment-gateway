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
using TransactionType = PaymentGateway.Entities.Enum.TransactionType;

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
            var balanceUpdateResponse = CreateBalanceUpdateResponseDto(request, transactionId);

            MockGetTransactionReturnNull();
            MockAddTransactionSuccess(transactionId);
            MockGetCashLogNull(request, transactionId);
            MockUpdateWalletBalance(transactionId, request, balanceUpdateResponse, cancelToken);

            var response = await controller.CreateTransactionAsync(request, cancelToken);

            await AssertReceivedCreateRequestAsync(request, cancelToken);
            await AssertReceivedWalletBalanceUpdateAsync(transactionId, request, cancelToken);
            AssertResponse(response, request, transactionId, balanceUpdateResponse.Balance);
        }

        [Fact]
        public async Task CreateDuplicatedTransaction_Test()
        {
            var transactionService = new TransactionService(_sqlAccessor, _loggerTransactionService);
            var controller = new ApiController(transactionService, _logger);
            var cancelToken = CancellationToken.None;
            var request = CreateTransactionRequest();
            var transactionId = Guid.NewGuid();
            var balanceUpdateResponse = CreateBalanceUpdateResponseDto(request, transactionId);

            MockGetTransactionByRequest(transactionId, request);
            MockGetCashLogExist(request, transactionId, balanceUpdateResponse);

            var response = await controller.CreateTransactionAsync(request, cancelToken);

            AssertResponse(response, request, transactionId, balanceUpdateResponse.Balance);
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
                Type = TransactionType.Withdraw,
                TokenId = Guid.NewGuid(),
                BankCode = "MB"
            };
            return request;
        }

        private static BalanceUpdateResponseDto CreateBalanceUpdateResponseDto(CreateTransactionRequest request,
            Guid transactionId)
        {
            var balanceUpdateResponse = new BalanceUpdateResponseDto
            {
                PlayerId = request.PlayerId,
                CashLogId = Guid.NewGuid(),
                ExternalTransactionId = transactionId.ToString(),
                Balance = 892
            };
            return balanceUpdateResponse;
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
                    Type = request.Type,
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

        private void MockGetCashLogNull(CreateTransactionRequest request, Guid transactionId)
        {
            _sqlAccessor
                .GetCashLogAsync(request.Type, transactionId.ToString(), request.PlayerId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(default(PlayerCashLog)));
        }

        private void MockGetCashLogExist(CreateTransactionRequest request, Guid transactionId, BalanceUpdateResponseDto balanceUpdateResponse)
        {
            _sqlAccessor
                .GetCashLogAsync(request.Type, transactionId.ToString(), request.PlayerId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new PlayerCashLog
                {
                    ExternalTransactionId = transactionId.ToString(),
                    TransactionType = request.Type,
                    CreatedDate = DateTimeOffset.UtcNow.AddHours(-1),
                    Amount = request.Amount,
                    PlayerCashLogId = balanceUpdateResponse.CashLogId,
                    PlayerId = request.PlayerId,
                    PostBalance = balanceUpdateResponse.Balance,
                    CurrentBalance = balanceUpdateResponse.Balance + request.Amount
                }));
        }

        private void MockUpdateWalletBalance(Guid transactionId, CreateTransactionRequest request, BalanceUpdateResponseDto balanceUpdateResponse, CancellationToken cancelToken)
        {
            _sqlAccessor
                .WalletBalanceUpdateAsync(
                    Arg.Is<BalanceUpdateDto>(dto =>
                        dto.ExternalTransactionId == transactionId.ToString() &&
                        dto.Type == request.Type &&
                        dto.Amount == request.Amount),
                    Arg.Is(cancelToken))
                .Returns(Task.FromResult(balanceUpdateResponse));
        }

        private async Task AssertReceivedCreateRequestAsync(CreateTransactionRequest request, CancellationToken cancelToken)
        {
            await _sqlAccessor.Received().AddTransactionAsync(
                Arg.Is<Transaction>(a =>
                    a.MerchantTransactionId == request.TicketId &&
                    a.ProviderTransactionId == string.Empty &&
                    a.Provider == Provider.EeziePay &&
                    a.TokenId == request.TokenId &&
                    a.Type == request.Type &&
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

        private async Task AssertReceivedWalletBalanceUpdateAsync(Guid transactionId, CreateTransactionRequest request, CancellationToken cancelToken)
        {
            await _sqlAccessor.Received().WalletBalanceUpdateAsync(
                Arg.Is<BalanceUpdateDto>(dto =>
                    dto.ExternalTransactionId == transactionId.ToString() &&
                    dto.Type == request.Type &&
                    dto.Amount == request.Amount),
                Arg.Is(cancelToken));
        }

        private static void AssertResponse(ApiResponse<CreateTransactionResponse> response, CreateTransactionRequest request, Guid transactionId, decimal balance)
        {
            response.Status.Should().Be(StatusCode.Success);
            response.Data.Should().NotBeNull();
            response.Data.TicketId.Should().Be(request.TicketId);
            response.Data.TransactionId.Should().Be(transactionId.ToString());
            response.Data.Balance.Should().Be(balance);
        }
    }
}