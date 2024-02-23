using PaymentGateway.Entities.Enum;
using System;

namespace PaymentGateway.Entities
{
    public sealed class Transaction
    {
        public Guid TransactionId { get; set; }
        public string MerchantTransactionId { get; set; }
        public string ProviderTransactionId { get; set; }
        public Provider Provider { get; set; }
        public Guid TokenId { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal Amount { get; set; }
        public string BankCode { get; set; }
        public string PlayerId { get; set; }
        public string PlayerRealName { get; set; }
        public string PlayerCardNumber { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
