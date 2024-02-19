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
}