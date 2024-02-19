using PaymentGateway.Api.Model;

namespace PaymentGateway.Api.Interface;

public interface ITransactionService
{
    Task<Guid> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken cancellationToken);
}