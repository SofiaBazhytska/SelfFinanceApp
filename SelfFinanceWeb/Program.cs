using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SelfFinanceWeb;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://selffinance-api-hmbcdmczfse7ahbh.polandcentral-01.azurewebsites.net/") });

await builder.Build().RunAsync();
