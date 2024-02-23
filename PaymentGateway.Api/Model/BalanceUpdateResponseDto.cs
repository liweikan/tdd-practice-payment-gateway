using System;

namespace PaymentGateway.Api.Model
{
    public class BalanceUpdateResponseDto
    {
        public string PlayerId { get; set; }
        public Guid CashLogId { get; set; }
        public string ExternalTransactionId { get; set; }
        public decimal Balance { get; set; }
    }
}