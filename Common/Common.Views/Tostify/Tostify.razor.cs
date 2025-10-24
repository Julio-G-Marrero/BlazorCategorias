using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Common.Views.Tostify;
public partial class Tostify
{
    [Inject]
    public IJSRuntime JsRuntime { get; set; }

    private IJSObjectReference JsModule;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                JsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Common.Views/Tostify/Tostify.razor.js");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    public async Task ShowSuccessToast(string message)
    {
        if (JsModule != null)
        {
            await JsModule.InvokeVoidAsync("showSuccess", message);
        }
    }
}
