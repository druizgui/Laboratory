using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AspnetCore.Responsive.Razor
{
    public class ExampleJsInterop
    {
        public static ValueTask<string> Prompt(IJSRuntime jsRuntime, string message)
        {
            // Implemented in exampleJsInterop.js
            return jsRuntime.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
        }
    }
}


//https://docs.microsoft.com/es-es/aspnet/core/razor-pages/ui-class?view=aspnetcore-3.1&tabs=visual-studio