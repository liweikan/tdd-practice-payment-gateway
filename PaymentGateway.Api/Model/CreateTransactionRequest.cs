using PaymentGateway.Entities;
using PaymentGateway.Entities.Enum;
using System;

namespace PaymentGateway.Api.Model;

public class CreateTransactionRequest
{
    public string TicketId { get; set; }
    public string PlayerId { get; set; }
    public string PlayerRealName { get; set; }
    public string PlayerCardNumber { get; set; }
    public decimal Amount { get; set; }
    public Guid TokenId { get; set; }
    public string BankCode { get; set; }

    public Transaction BuildTransaction(DateTimeOffset now)
    {
        var transaction = new Transaction
        {
            MerchantTransactionId = TicketId,
            ProviderTransactionId = string.Empty,
            Provider = Provider.EeziePay,
            TokenId = TokenId,
            Status = TransactionStatus.Pending,
            Amount = Amount,
            BankCode = BankCode,
            PlayerId = PlayerCardNumber,
            PlayerRealName = PlayerRealName,
            PlayerCardNumber = PlayerCardNumber,
            CreatedUser = PlayerId,
            CreatedDate = now,
            UpdatedUser = PlayerId,
            UpdatedDate = now,
        };
        return transaction;
    }
}