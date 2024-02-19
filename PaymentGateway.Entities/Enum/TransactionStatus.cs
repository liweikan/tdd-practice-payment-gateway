namespace PaymentGateway.Entities.Enum;

public enum TransactionStatus
{
    Pending = 0,
    ProviderNotified = 1,
    Success = 2,
    Failed = 3,
}