using Microsoft.Extensions.Logging;
using PaymentGateway.Api.Interface;
using PaymentGateway.Api.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Service;

internal class TransactionService : ITransactionService
{
    private readonly ISqlAccessor _sqlAccessor;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(
        ISqlAccessor sqlAccessor,
        ILogger<TransactionService> logger)
    {
        _sqlAccessor = sqlAccessor;
        _logger = logger;
    }

    async Task<CreateTransactionResponse> ITransactionService.CreateTransactionAsync(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        Guid transactionId;
        var transaction = await _sqlAccessor.GetTransactionAsync(request.TicketId, cancellationToken);
        if (transaction == default)
        {
            transactionId = await _sqlAccessor.AddTransactionAsync(request.BuildTransaction(now: DateTimeOffset.UtcNow), cancellationToken);
            _logger.LogInformation($"create transaction done, transactionId[{transactionId}]");
        }
        else
        {
            transactionId = transaction.TransactionId;
        }

        var response = await UpdateWalletAsync(request, transactionId, cancellationToken);
        return new CreateTransactionResponse
        {
            TransactionId = transactionId,
            TicketId = request.TicketId,
            Balance = response.Balance
        };
    }

    private async Task<BalanceUpdateResponseDto> UpdateWalletAsync(CreateTransactionRequest request, Guid transactionId, CancellationToken cancellationToken)
    {
        var response = await _sqlAccessor.WalletBalanceUpdateAsync(
            new BalanceUpdateDto
            {
                Amount = request.Amount,
                ExternalTransactionId = transactionId.ToString(),
                PlayerId = request.PlayerId,
                Type = request.Type
            },
            cancellationToken);

        _logger.LogInformation($"update wallet done, cashLogId[{response.CashLogId}], balance[{response.Balance}]");

        return response;
    }
}