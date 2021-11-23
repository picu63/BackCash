using BC.Client;
using BC.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BC.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddMudServices();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BC.ServerAPI"));
builder.Services.AddScoped<ShopsService>();
builder.Services.AddScoped<CashbackService>();
builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();

//static class ServiceExtensions
//{
//    public static IServiceCollection AddMudServices(this IServiceCollection services, MudServicesConfiguration configuration = null)
//    {
//        configuration ??= new MudServicesConfiguration();
//        return services
//            .AddMudBlazorDialog()
//            .AddMudBlazorSnackbar(configuration.SnackbarConfiguration)
//            .AddMudBlazorResizeListener(configuration.ResizeOptions)
//            .AddMudBlazorScrollManager()
//            .AddMudBlazorScrollListener();
//    }
//}