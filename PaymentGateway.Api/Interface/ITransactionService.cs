using PaymentGateway.Api.Model;

namespace PaymentGateway.Api.Interface;

public interface ITransactionService
{
    Task<string> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken cancellationToken);
}