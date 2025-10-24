using Domain.Application.Abstractions;
using Microsoft.JSInterop;

namespace Web.Interop

{
    public class ToastService : IToastService
    {
        private readonly IJSRuntime _js;
        public ToastService(IJSRuntime js) => _js = js;

        public Task ShowInfoToast(string message, string type = "info", int ms = 3000)
            => _js.InvokeVoidAsync("toastHelper.show", message, type, ms).AsTask();
    }
}
