using System;

namespace PaymentGateway.Entities
{
    public class PlayerWallet
    {
        public string PlayerId { get; set; }
        public decimal Balance { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}