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

    async Task<Guid> ITransactionService.CreateTransactionAsync(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        //TODO: check transaction before add it

        var transactionId = await _sqlAccessor.AddTransactionAsync(request.BuildTransaction(DateTimeOffset.UtcNow), cancellationToken);
        _logger.LogInformation($"create transaction done, transactionId[{transactionId}]");

        return transactionId;
    }
}