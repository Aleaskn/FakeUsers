using Blazored.LocalStorage;
using FakeUsers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Aggiungi HttpClient per fare richieste alla tua API (aggiorna l'URL in base alla configurazione del server API)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5093/") });

// Registra ProductService nel contenitore DI
builder.Services.AddScoped<ProductService>();


// Registrazione del CustomAuthStateProvider come provider di autenticazione
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddBlazoredLocalStorage();

// Configura l'autenticazione
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
