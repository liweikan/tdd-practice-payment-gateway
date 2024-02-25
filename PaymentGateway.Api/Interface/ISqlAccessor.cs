using PaymentGateway.Api.Model;
using PaymentGateway.Entities;
using PaymentGateway.Entities.Enum;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Interface;

public interface ISqlAccessor
{
    Task<Guid> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<PlayerCashLog> GetCashLogAsync(TransactionType type, string transactionId, string playerId, CancellationToken cancellationToken);
    Task<Transaction> GetTransactionAsync(string ticketId, CancellationToken cancellationToken);
    Task<BalanceUpdateResponseDto> WalletBalanceUpdateAsync(BalanceUpdateDto dto, CancellationToken cancelToken);
}