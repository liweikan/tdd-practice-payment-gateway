using PaymentGateway.Entities.Enum;
using System;

namespace PaymentGateway.Entities
{
    public class PlayerCashLog
    {
        public Guid PlayerCashLogId { get; set; }
        public string PlayerId { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal PostBalance { get; set; }
        public TransactionType TransactionType { get; set; }
        public string ExternalTransactionId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}