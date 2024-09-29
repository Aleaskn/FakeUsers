using FakeUsers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Aggiungi HttpClient per fare richieste alla tua API (aggiorna l'URL in base alla configurazione del server API)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5093/") });

// Registra ProductService nel contenitore DI
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
