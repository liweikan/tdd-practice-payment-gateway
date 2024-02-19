namespace PaymentGateway.Api.Model;

public class ApiResponse<T>
{
    public ApiResponse()
    {
        Status = StatusCode.Success;
    }

    public ApiResponse(StatusCode status, T data)
    {
        Status = status;
        Data = data;
    }

    public StatusCode Status { get; set; }
    public T Data { get; set; }
}