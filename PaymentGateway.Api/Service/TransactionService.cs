using PaymentGateway.Api.Interface;
using PaymentGateway.Api.Model;

namespace PaymentGateway.Api.Service;

internal class TransactionService : ITransactionService
{
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(ILogger<TransactionService> logger)
    {
        _logger = logger;
    }

    async Task<string> ITransactionService.CreateTransactionAsync(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var txnId = "TempTransactionId";
        _logger.LogInformation($"Create transaction done, transactionId[{txnId}]");

        return txnId;
    }
}