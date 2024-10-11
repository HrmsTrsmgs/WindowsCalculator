using Marimo.WindowsCalculator.BlazorWebAssembly;
using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<CalculatorViewModel>();

builder.Services.AddLog4Net(Path.Combine(AppContext.BaseDirectory, "log.txt"));


await builder.Build().RunAsync();



#if DEBUG
builder.Logging.SetMinimumLevel(LogLevel.Debug); 
#else
builder.Logging.SetMinimumLevel(LogLevel.Information);
#endif
