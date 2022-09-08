using Edge.Bills.Blazor;
using Edge.Bills.Blazor.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.ConfigureGeneral();
builder.Services.ConfigureAutentication();
builder.Services.ConfigureServices();

await builder.Build().RunAsync();
