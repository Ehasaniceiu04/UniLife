using System;
using System.Linq;
//using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using AutoMapper;
#if ServerSideBlazor

using UniLife.CommonUI;
using UniLife.CommonUI.Services.Contracts;
using UniLife.CommonUI.Services.Implementations;
using UniLife.CommonUI.States;

using MatBlazor;

using Microsoft.AspNetCore.Components.Authorization;

using System.Net.Http;
using UniLife.CommonUI.Components;

#endif

using UniLife.Server.Authorization;
using UniLife.Server.Helpers;
using UniLife.Server.Managers;
using UniLife.Server.Middleware;
using UniLife.Shared;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Storage;
using UniLife.Storage.Mapping;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using System.Reflection;
using Syncfusion.Blazor;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
//using System.Globalization;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Localization;
//using Microsoft.Extensions.Options;


namespace UniLife.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ5NDcyQDMxMzgyZTMxMmUzMEN4L0RNQTdEU1NTVmpveTZNWkpHZ3JQelZjdC96dXFhcCs4NWE2T0ZidW89");
            Configuration = configuration;
            _environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var authAuthority = Configuration["UniLife:IS4ApplicationUrl"].TrimEnd('/');

            services.RegisterStorage(Configuration);
            var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName();
            var migrationsAssemblyName = migrationsAssembly.Name;
            var useSqlServer = Convert.ToBoolean(Configuration["UniLife:UseSqlServer"] ?? "false");
            var dbConnString = useSqlServer
                ? Configuration.GetConnectionString("DefaultConnection")
                : $"Filename={Configuration.GetConnectionString("SqlLiteConnectionFileName")}";

            void DbContextOptionsBuilder(DbContextOptionsBuilder builder)
            {
                if (useSqlServer)
                {
                    builder.UseSqlServer(dbConnString, sql => sql.MigrationsAssembly(migrationsAssemblyName));
                }
                else if (Convert.ToBoolean(Configuration["UniLife:UsePostgresServer"] ?? "false"))
                {
                    builder.UseNpgsql(Configuration.GetConnectionString("PostgresConnection"), sql => sql.MigrationsAssembly(migrationsAssemblyName));
                }
                else
                {
                    builder.UseSqlite(dbConnString, sql => sql.MigrationsAssembly(migrationsAssemblyName));
                }
            }

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddDbContext<ApplicationDbContext>(DbContextOptionsBuilder);

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                AdditionalUserClaimsPrincipalFactory>();

            // cookie policy to deal with temporary browser incompatibilities
            services.AddSameSiteCookiePolicy();

            // Adds IdentityServer
            var identityServerBuilder = services.AddIdentityServer(options =>
            {
                options.IssuerUri = authAuthority;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
              .AddIdentityServerStores(Configuration)
              .AddConfigurationStore(options =>
              {
                  options.ConfigureDbContext = DbContextOptionsBuilder;
              })
              .AddOperationalStore(options =>
              {
                  options.ConfigureDbContext = DbContextOptionsBuilder;

                  // this enables automatic token cleanup. this is optional.
                  options.EnableTokenCleanup = true;
                  options.TokenCleanupInterval = 3600; //In Seconds 1 hour
              })
              .AddAspNetIdentity<ApplicationUser>();

            //X509Certificate2 cert = null;

            services.AddSyncfusionBlazor();


            //// 
            //// TODO ::
            //// BU Identity server olay? nedir daha sonra girecez.
            ///

            //if (_environment.IsDevelopment())
            //{
            //    // The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.
            //    // This might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
            //    // See http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto for more information.
            //    // https://stackoverflow.com/questions/42351274/identityserver4-hosting-in-iis
            //    //.AddDeveloperSigningCredential(true, @"C:\tempkey.rsa")
            identityServerBuilder.AddDeveloperSigningCredential();
            //}
            //else
            //{
            //    // running on azure
            //    // please make sure to replace your vault URI and your certificate name in appsettings.json!
            //    if (Convert.ToBoolean(Configuration["HostingOnAzure:RunsOnAzure"]) == true)
            //    {
            //        // if we use a key vault
            //        if (Convert.ToBoolean(Configuration["HostingOnAzure:AzurekeyVault:UsingKeyVault"]) == true)
            //        {

            //            // if managed app identity is used
            //            if (Convert.ToBoolean(Configuration["HostingOnAzure:AzurekeyVault:UseManagedAppIdentity"]) == true)
            //            {
            //                try
            //                {
            //                    AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();

            //                    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            //                    var certificateBundle = keyVaultClient.GetSecretAsync(Configuration["HostingOnAzure:AzureKeyVault:VaultURI"], Configuration["HostingOnAzure:AzurekeyVault:CertificateName"]).GetAwaiter().GetResult();
            //                    var certificate = System.Convert.FromBase64String(certificateBundle.Value);
            //                    cert = new X509Certificate2(certificate, (string)null, X509KeyStorageFlags.MachineKeySet);
            //                }
            //                catch (Exception ex)
            //                {
            //                    throw (ex);
            //                }
            //            }

            //            // if app id and app secret are used
            //            if (Convert.ToBoolean(Configuration["HostingOnAzure:AzurekeyVault:UsingKeyVault"]) == false)
            //            {
            //                throw new NotImplementedException();
            //            }

            //        }
            //    }

            //    // using local cert store
            //    if (Convert.ToBoolean(Configuration["UniLife:UseLocalCertStore"]) == true)
            //    {
            //        var certificateThumbprint = Configuration["UniLife:CertificateThumbprint"];
            //        using (X509Store store = new X509Store("WebHosting", StoreLocation.LocalMachine))
            //        {
            //            store.Open(OpenFlags.ReadOnly);
            //            var certs = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
            //            if (certs.Count > 0)
            //            {
            //                cert = certs[0];
            //            }
            //            else
            //            {
            //                // import PFX
            //                cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, "AuthSample.pfx"), "Admin123",
            //                                    X509KeyStorageFlags.MachineKeySet |
            //                                    X509KeyStorageFlags.PersistKeySet |
            //                                    X509KeyStorageFlags.Exportable);
            //                // save certificate and private key
            //                X509Store storeMy = new X509Store(StoreName.CertificateAuthority, StoreLocation.LocalMachine);
            //                storeMy.Open(OpenFlags.ReadWrite);
            //                storeMy.Add(cert);
            //            }
            //            store.Close();
            //        }
            //    }

            //    // pass the resulting certificate to Identity Server
            //    if (cert != null)
            //    {
            //        identityServerBuilder.AddSigningCredential(cert);
            //    }
            //    else
            //    {
            //        throw new FileNotFoundException("No certificate for Identity Server could be retrieved.");
            //    }
            //}

            var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = authAuthority;
                options.SupportedTokens = SupportedTokens.Jwt;
                options.RequireHttpsMetadata = _environment.IsProduction() ? true : false;
                options.ApiName = IdentityServerConfig.ApiName;
            });

            ////https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-3.1
            //if (Convert.ToBoolean(Configuration["ExternalAuthProviders:Google:Enabled"] ?? "false"))
            //{
            //    authBuilder.AddGoogle(options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        options.ClientId = Configuration["ExternalAuthProviders:Google:ClientId"];
            //        options.ClientSecret = Configuration["ExternalAuthProviders:Google:ClientSecret"];
            //    });
            //}

            //Add Policies / Claims / Authorization - https://stormpath.com/blog/tutorial-policy-based-authorization-asp-net-core
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
                options.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
                options.AddPolicy(Policies.IsReadOnly, Policies.IsReadOnlyPolicy());
                options.AddPolicy(Policies.IsMyDomain, Policies.IsMyDomainPolicy());  // valid only on serverside operations
            });

            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddTransient<IAuthorizationHandler, DomainRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, PermissionRequirementHandler>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                //options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // Require Confirmed Email User settings
                if (Convert.ToBoolean(Configuration["UniLife:RequireConfirmedEmail"] ?? "false"))
                {
                    options.User.RequireUniqueEmail = false;
                    options.SignIn.RequireConfirmedEmail = true;
                }
            });

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.ConfigureExternalCookie(options =>
            // {
            //     //macOS login fix
            //options.Cookie.SameSite = SameSiteMode.None;
            // });

            services.ConfigureApplicationCookie(options =>
            {
                // macOS login fix
                //options.Cookie.SameSite = SameSiteMode.None;
                //options.Cookie.HttpOnly = false;

                // Suppress redirect on API URLs in ASP.NET Core -> https://stackoverflow.com/a/56384729/54159
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToAccessDenied = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api"))
                        {
                            context.Response.StatusCode = Status403Forbidden;
                        }

                        return Task.CompletedTask;
                    },
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            });

            ////Eskisi buydu
            //services.AddControllers().AddNewtonsoftJson();

            //ODATALI
            services.AddControllers(action => action.EnableEndpointRouting = false).AddNewtonsoftJson();
            services.AddOData();

            ////Bunu syncfusiongrid dataadaptoru anlas?n diye yapmak gerekbiliyor.
            //services.AddControllers().AddNewtonsoftJson(options => {
            //    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //});


            //#region Localization
            //// Set the Resx file folder path to access
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            //services.AddSyncfusionBlazor();
            //// register a Syncfusion locale service to customize the Syncfusion Blazor component locale culture
            //services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncLocalizer));
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    // Define the list of cultures your app will support
            //    var supportedCultures = new List<CultureInfo>()
            //{
            //    new CultureInfo("en-US"),
            //    new CultureInfo("de")
            //};

            //    // Set the default culture
            //    options.DefaultRequestCulture = new RequestCulture("en-US");

            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //});
            //#endregion




            services.AddSignalR();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = migrationsAssembly.Version.ToString();
                    document.Info.Title = "Uni-Life";
#if ServerSideBlazor
                    document.Info.Description = "Uni-Life / Starter Template using the  Server Side Version";
#endif
#if ClientSideBlazor
                    document.Info.Description = "Uni-Life / Starter Template using the Client Side / Webassembly Version.";
#endif
                };
            });

            services.AddScoped<IUserSession, UserSession>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IAdminManager, AdminManager>();
            services.AddTransient<IApiLogManager, ApiLogManager>();
            services.AddTransient<IDbLogManager, DbLogManager>();
            services.AddTransient<IEmailManager, EmailManager>();
            services.AddTransient<IExternalAuthManager, ExternalAuthManager>(); // Currently not being used.
            services.AddTransient<IMessageManager, MessageManager>();
            services.AddTransient<ITodoManager, ToDoManager>();
            services.AddTransient<IUniversiteManager, UniversiteManager>();
            services.AddTransient<IFakulteManager, FakulteManager>();
            services.AddTransient<IBolumManager, BolumManager>();
            services.AddTransient<IHarcManager, HarcManager>();
            services.AddTransient<IProgramManager, ProgramManager>();
            services.AddTransient<IMufredatManager, MufredatManager>();
            services.AddTransient<IDersManager, DersManager>();
            services.AddTransient<IDersAcilanManager, DersAcilanManager>();
            services.AddTransient<IDersKayitManager, DersKayitManager>();
            services.AddTransient<IOgrenimTurManager, OgrenimTurManager>();
            services.AddTransient<IFakulteTurManager, FakulteTurManager>();
            services.AddTransient<IDonemManager, DonemManager>();
            services.AddTransient<IDonemTipManager, DonemTipManager>();
            services.AddTransient<IOgrenimDurumManager, OgrenimDurumManager>();
            services.AddTransient<IKayitNedenManager, KayitNedenManager>();
            services.AddTransient<IOgrenciManager, OgrenciManager>();
            services.AddTransient<IAkademisyenManager, AkademisyenManager>();
            services.AddTransient<IPersonelManager, PersonelManager>();
            services.AddTransient<ISinavManager, SinavManager>();
            services.AddTransient<ISinavTurManager, SinavTurManager>();
            services.AddTransient<ISinavTipManager, SinavTipManager>();
            services.AddTransient<ISinavKayitManager, SinavKayitManager>();
            services.AddTransient<IBinaManager, BinaManager>();
            services.AddTransient<IDerslikManager, DerslikManager>();
            services.AddTransient<IDerslikRezervManager, DerslikRezervManager>();
            services.AddTransient<IUserProfileManager, UserProfileManager>();



            //Automapper to map DTO to Models https://www.c-sharpcorner.com/UploadFile/1492b1/crud-operations-using-automapper-in-mvc-application/
            var automapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new MappingProfile());
            });

            var autoMapper = automapperConfig.CreateMapper();

            services.AddSingleton(autoMapper);

#if ServerSideBlazor

            services.AddScoped<IAuthorizeApi, AuthorizeApi>();
            services.AddScoped<IUserProfileApi, UserProfileApi>();
            services.AddScoped<AppState>();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            // Setup HttpClient for server side
            services.AddScoped<HttpClient>();


            //#region Localization
            //// Set the Resx file folder path to access
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            //services.AddSyncfusionBlazor();
            //// register a Syncfusion locale service to customize the Syncfusion Blazor component locale culture
            //services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncLocalizer));
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    // Define the list of cultures your app will support
            //    var supportedCultures = new List<CultureInfo>()
            //{
            //    new CultureInfo("en-US"),
            //    new CultureInfo("tr")
            //};

            //    // Set the default culture
            //    options.DefaultRequestCulture = new RequestCulture("tr");

            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //});
            //#endregion




            services.AddRazorPages();
            //services.AddServerSideBlazor(); //For dev show error.

            services.AddServerSideBlazor().AddCircuitOptions(o =>
            {
                if (_environment.IsDevelopment()) //only add details when debugging
                {
                    o.DetailedErrors = true;
                }
            });


            // Authentication providers

            Log.Logger.Debug("Removing AuthenticationStateProvider...");
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(AuthenticationStateProvider));
            if (serviceDescriptor != null)
            {
                services.Remove(serviceDescriptor);
            }

            Log.Logger.Debug("Adding AuthenticationStateProvider...");
            services.AddScoped<AuthenticationStateProvider, IdentityAuthenticationStateProvider>();

            services.AddScoped<SampleService>();
#endif

            Log.Logger.Debug($"Total Services Registered: {services.Count}");
            foreach (var service in services)
            {
                Log.Logger.Debug($"\n      Service: {service.ServiceType.FullName}\n      Lifetime: {service.Lifetime}\n      Instance: {service.ImplementationType?.FullName}");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // cookie policy to deal with temporary browser incompatibilities
            app.UseCookiePolicy();

            EmailTemplates.Initialize(env);

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var databaseInitializer = serviceScope.ServiceProvider.GetService<IDatabaseInitializer>();
                databaseInitializer.SeedAsync().Wait();
            }

            //odatalari loglamiyoruz (APIResponseRequestLoggingMiddleware den ayirdik).
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), applicationBuilder =>
            {
                // A REST API global exception handler and response wrapper for a consistent API
                // Configure API Loggin in appsettings.json - Logs most API calls. Great for debugging and user activity audits
                applicationBuilder.UseMiddleware<APIResponseRequestLoggingMiddleware>(Convert.ToBoolean(Configuration["UniLife:EnableAPILogging:Enabled"] ?? "true"));
            });

            // A REST API global exception handler and response wrapper for a consistent API
            // Configure API Loggin in appsettings.json - Logs most API calls. Great for debugging and user activity audits
            //app.UseMiddleware<APIResponseRequestLoggingMiddleware>(Convert.ToBoolean(Configuration["UniLife:EnableAPILogging:Enabled"] ?? "true"));


            //#region Localization

            //app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);

            //#endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
#if ClientSideBlazor
                app.UseWebAssemblyDebugging();
#endif
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //    app.UseHsts(); //HSTS Middleware (UseHsts) to send HTTP Strict Transport Security Protocol (HSTS) headers to clients.
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

#if ClientSideBlazor
            app.UseBlazorFrameworkFiles();
#endif

            app.UseRouting();
            //app.UseAuthentication(); //Removed for IS4
            app.UseIdentityServer();
            app.UseAuthorization();

            //Must be AFTER the Auth middleware to get the User/Identity info
            app.UseMiddleware<UserSessionMiddleware>();

            // NSwag
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.Expand().Select().Count().OrderBy().Filter().MaxTop(2000);
                endpoints.MapODataRoute("odata", "odata", GetEdmModel());
                // new SignalR endpoint routing setup
                endpoints.MapHub<Hubs.ChatHub>("/chathub");

#if ClientSideBlazor
                endpoints.MapFallbackToFile("index_csb.html");
#else
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/index_ssb");
#endif
            });

        }

        private Microsoft.OData.Edm.IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Ogrenci>("Ogrencis");
            builder.EntitySet<Bolum>("Bolums");
            builder.EntitySet<DersAcilan>("DersAcilans");
            builder.EntitySet<Fakulte>("Fakultes");
            builder.EntitySet<Fakulte>("Akademisyens");
            builder.EntitySet<UniLife.Shared.DataModels.Program>("Programs");
            builder.EntitySet<DerslikRezerv>("DerslikRezervs");
            builder.EntitySet<Derslik>("Dersliks");
            return builder.GetEdmModel();
        }
    }
}
