namespace Domain.ValueObjects;
public class HandlerRequestResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public HandlerRequestResult()
    {
        Success = true;
    }
    public HandlerRequestResult(string errorMessage)
    {
        Success = false;
        ErrorMessage = errorMessage;
    }
}
