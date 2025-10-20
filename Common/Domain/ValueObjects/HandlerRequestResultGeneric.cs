namespace Domain.ValueObjects;
public class HandlerRequestResult<T>
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public T SuccessValue { get; set; }

    public HandlerRequestResult()
    {
        Success = true;
    }

    public HandlerRequestResult(string errorMessage)
    {
        Success = false;
        ErrorMessage = errorMessage;
    }

    public HandlerRequestResult(T data)
    {
        Success = true;
        SuccessValue = data;
    }

}
