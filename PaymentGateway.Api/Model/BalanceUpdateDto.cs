using TransactionType = PaymentGateway.Entities.Enum.TransactionType;

namespace PaymentGateway.Api.Model
{
    public class BalanceUpdateDto
    {
        public string PlayerId { get; set; }
        public string ExternalTransactionId { get; set; }
        public TransactionType Type { get; set; }

        /// <summary>
        /// should provide plus or minus sign. ex: debit -120m / credit 20m
        /// </summary>
        public decimal Amount { get; set; }
    }
}