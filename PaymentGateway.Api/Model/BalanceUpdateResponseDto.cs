using System;

namespace PaymentGateway.Api.Model
{
    public class BalanceUpdateResponseDto
    {
        public string PlayerId { get; set; }
        public Guid CashLogId { get; set; }
        public Guid ExternalTransactionId { get; set; }
        public int Balance { get; set; }
    }
}