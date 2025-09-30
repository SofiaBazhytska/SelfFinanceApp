using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SelfFinanceWeb;
using SelfFinanceWeb.Authentication;
using SelfFinanceWeb.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

// ? Реєструємо AuthMessageHandler
builder.Services.AddTransient<AuthMessageHandler>();

// ? Правильний спосіб для Blazor WASM
/*builder.Services.AddScoped(sp =>
    new HttpClient(new AuthMessageHandler(sp.GetRequiredService<ILocalStorageService>(), sp.GetRequiredService <ILogger<AuthMessageHandler>())
    {
        InnerHandler = new HttpClientHandler() // обов’язково вказати!
    })
    {
        BaseAddress = new Uri("https://localhost:7114/")
    });
*/

builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
})
.AddHttpMessageHandler<AuthMessageHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

await builder.Build().RunAsync();
