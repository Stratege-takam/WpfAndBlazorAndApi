using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Brewery.Web;
using Brewery.Web.Helpers;
using Elia.Core.Containers;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Environments = Elia.Core.Utils.Environments;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};

builder.Services.AddScoped(sp => http);

var env = Environments.Current.ToString();

string appsettings = Environments.Current == EnvironmentEnum.Debug ? "Appsettings/appsettings.json" :  $"Appsettings/appsettings.{env}.json";
  
using var response = await http.GetAsync(appsettings);
using var stream = await response.Content.ReadAsStreamAsync();

builder.Configuration.AddJsonStream(stream);


builder.Services.AutoInject(SolutionAssembly.GetAllAssemblies);

var app = builder.Build();

await app.RunAsync();