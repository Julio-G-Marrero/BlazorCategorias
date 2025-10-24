using System;
namespace Common.Application.Abstractions.Notifications;
public interface IToastService
{
    Task ShowInfoToast(string message, string type = "info", int ms = 3000);
}