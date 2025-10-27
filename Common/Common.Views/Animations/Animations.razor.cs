using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views.Animations
{
    public partial class Animations
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
                    JsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Common.Views/Animations/Animations.razor.js");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task AnimateModalOpenAsync(string selector)
        {
            if(JsModule != null)
            {
                await  JsModule.InvokeVoidAsync("animateModalOpen", selector);
            }
        }

        public async Task PulseRowAsync(string selector)
        {
            if (JsModule != null)
            {
                await JsModule.InvokeVoidAsync("pulseRow", selector);
            }
        }

        public async Task FadeOutRowAsync(string selector)
        {
            if (JsModule != null)
            {
                await JsModule.InvokeVoidAsync("fadeOutRow", selector);
            }
        }

        public async Task FadeInAsync(string selector, int ms = 200)
        {
            if (JsModule != null)
            {
                await JsModule.InvokeVoidAsync("fadeIn", selector, ms);
            }
        }
        public async Task AnimateModalCloseAsync(string selector, object? options = null)
        {
            if (JsModule != null)
            {
                await JsModule.InvokeVoidAsync("animateModalClose", selector, options ?? new { });
            }
        }
    }
}
