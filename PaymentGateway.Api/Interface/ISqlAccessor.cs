using PaymentGateway.Entities;

namespace PaymentGateway.Api.Interface;

public interface ISqlAccessor
{
    Task<Guid> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
}