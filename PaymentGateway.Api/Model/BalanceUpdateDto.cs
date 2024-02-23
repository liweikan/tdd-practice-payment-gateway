using PaymentGateway.Entities.Enum;
using System;

namespace PaymentGateway.Api.Model
{
    public class BalanceUpdateDto
    {
        public Guid ExternalTransactionId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}