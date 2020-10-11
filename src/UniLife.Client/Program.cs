using UniLife.CommonUI;
using UniLife.CommonUI.Services.Contracts;
using UniLife.CommonUI.Services.Implementations;
using UniLife.CommonUI.States;
using UniLife.Shared.AuthorizationDefinitions;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using UniLife.CommonUI.Components;
using System.Globalization;
using Microsoft.JSInterop;

namespace UniLife.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgzMTIyQDMxMzgyZTMyMmUzMElrS3Z6YXN4bUo3RTJESVhIeWRXMEZXa0pSc2NIaVN2d0kvM0F5YXRQNlU9");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzMyNDQzQDMxMzgyZTMzMmUzMG5TOG5VWXBtOXdzTjhjV0lNZ253eXArNXlBWlhyZFBqL2ZSWTR1Yk92UVE9");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddAuthorizationCore(config =>
            {
                config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
                //config.AddPolicy(Policies.IsOgrenci, Policies.IsOgrenciPolicy());
                //config.AddPolicy(Policies.IsAkademisyen, Policies.IsAkademisyenPolicy());
                //config.AddPolicy(Policies.IsPersonel, Policies.IsPersonelPolicy());
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

            #region Localization
            // Register the Syncfusion locale service to customize the  SyncfusionBlazor component locale culture
            builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

            // Set the default culture of the application
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("tr");

            // Get the modified culture from culture switcher
            var host = builder.Build();
            var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
            var result = await jsInterop.InvokeAsync<string>("cultureInfo.get");
            if (result != null)
            {
                // Set the culture from culture switcher
                var culture = new CultureInfo(result);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
            #endregion



            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            builder.Services.AddScoped<SampleService>();


            await builder
            .Build()
            .UseLoadingBar()
            .RunAsync();
        }
    }
}
