namespace Domain.Application.Abstractions;
public interface IToastService
{
    Task ShowInfoToast(string message, string type = "info", int ms = 3000);
}
