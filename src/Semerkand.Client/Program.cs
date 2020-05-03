using Semerkand.CommonUI;
using Semerkand.CommonUI.Services.Contracts;
using Semerkand.CommonUI.Services.Implementations;
using Semerkand.CommonUI.States;
using Semerkand.Shared.AuthorizationDefinitions;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Semerkand.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ5NDcyQDMxMzgyZTMxMmUzMEN4L0RNQTdEU1NTVmpveTZNWkpHZ3JQelZjdC96dXFhcCs4NWE2T0ZidW89");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddAuthorizationCore(config =>
            {
                config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
                config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
                config.AddPolicy(Policies.IsReadOnly, Policies.IsUserPolicy());
                // config.AddPolicy(Policies.IsMyDomain, Policies.IsMyDomainPolicy());  Only works on the server end
            });

            builder.Services.AddScoped<AuthenticationStateProvider, IdentityAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthorizeApi, AuthorizeApi>();
            builder.Services.Add(new ServiceDescriptor(typeof(IUserProfileApi), typeof(UserProfileApi), ServiceLifetime.Scoped));
            builder.Services.AddScoped<AppState>();
            builder.Services.AddLoadingBar();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            await builder
            .Build()
            .UseLoadingBar()
            .RunAsync();
        }
    }
}
