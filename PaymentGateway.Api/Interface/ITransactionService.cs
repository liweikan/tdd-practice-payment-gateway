using PaymentGateway.Api.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Interface;

public interface ITransactionService
{
    Task<Guid> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken cancellationToken);
}