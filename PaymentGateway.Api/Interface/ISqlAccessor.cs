using PaymentGateway.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Interface;

public interface ISqlAccessor
{
    Task<Guid> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<Transaction> GetTransactionAsync(string ticketId, CancellationToken cancellationToken);
}