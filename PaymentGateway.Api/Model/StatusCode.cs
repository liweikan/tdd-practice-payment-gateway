namespace PaymentGateway.Api.Model;

public enum StatusCode : short
{
    Success = 0,
    InsufficientBalance = 1,
    ParameterError = 2,
    GenerateError = 99
}