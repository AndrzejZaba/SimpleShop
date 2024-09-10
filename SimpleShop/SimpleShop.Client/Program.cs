using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleShop.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var uri = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "api/");

builder.Services.AddClient(uri);

await builder.Build().RunAsync();
