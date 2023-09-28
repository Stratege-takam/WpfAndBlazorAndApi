using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace Brewery.Web.Extensions;

public static class WebAssemblyHostExtension
{
    public async static Task SetDefaultCulture(this WebAssemblyHost host)
    {
        var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
        var result = await jsInterop.InvokeAsync<string>("blazorCulture.get");

        CultureInfo culture;

        if (result != null)
            culture = new CultureInfo(result);
        else
            culture = new CultureInfo("fr");

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}