using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Api.Interface;
using PaymentGateway.Context.Interface;
using PaymentGateway.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Service;

internal class SqlAccessor : ISqlAccessor
{
    private readonly IServiceProvider _serviceProvider;

    public SqlAccessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    async Task<Guid> ISqlAccessor.AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetService<IPaymentGatewayContext>();
        db.Transactions.Add(transaction);
        await db.SaveChangesAsync(cancellationToken);

        return transaction.TransactionId;
    }
}