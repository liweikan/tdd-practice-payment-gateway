using System;

namespace PaymentGateway.Api.Model;

public class CreateTransactionResponse
{
    public string TicketId { get; set; }
    public Guid TransactionId { get; set; }
    public decimal Balance { get; set; }
}