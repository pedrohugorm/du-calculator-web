using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DUCalculator.Web;
using DUCalculator.Web.Domain.HexGrid;
using DUCalculator.Web.Domain.LogAnalyser;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<IHexGridGenerator, HexGridGenerator>();
builder.Services.AddTransient<ILuaLogAnalyserService, LuaLogAnalyserService>();

await builder.Build().RunAsync();