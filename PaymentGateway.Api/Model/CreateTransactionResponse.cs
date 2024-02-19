namespace PaymentGateway.Api.Model;

public class CreateTransactionResponse
{
    public string TicketId { get; set; }
    public string TransactionId { get; set; }
    public decimal Balance { get; set; }
}