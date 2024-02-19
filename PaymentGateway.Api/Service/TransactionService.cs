using PaymentGateway.Api.Interface;
using PaymentGateway.Api.Model;
using PaymentGateway.Context.Interface;
using PaymentGateway.Entities;

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
        var now = DateTimeOffset.UtcNow;
        var transactionId = await _sqlAccessor.AddTransactionAsync(
            default,
            cancellationToken);

        _logger.LogInformation($"Create transaction done, transactionId[{transactionId}]");

        return transactionId;
    }
}

public interface ISqlAccessor
{
    Task<Guid> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
}

internal class SqlAccessor : ISqlAccessor
{
    private readonly IServiceProvider _serviceProvider;

    public SqlAccessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    async Task<Guid> ISqlAccessor.AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetService<IPaymentGatewayContext>();
            db.Transactions.Add(transaction);
            await db.SaveChangesAsync(cancellationToken);

            return transaction.TransactionId;
        }
    }
}