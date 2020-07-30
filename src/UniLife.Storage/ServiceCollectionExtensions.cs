using System;
using System.Reflection;
using UniLife.Server.Models;
using UniLife.Shared;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Storage.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UniLife.Storage
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterStorage(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(builder => GetDbContextOptions(builder, Configuration)); // Look into the way we initialise the PB ways. Look at the old way they did this, with side effects on the builder. 
            services.AddScoped<IApplicationDbContext>(s => s.GetRequiredService<ApplicationDbContext>() as IApplicationDbContext);

            services.AddTransient<IMessageStore, MessageStore>();
            services.AddTransient<IUserProfileStore, UserProfileStore>();
            services.AddTransient<IToDoStore, ToDoStore>();
            services.AddTransient<IUniversiteStore, UniversiteStore>();
            services.AddTransient<IFakulteStore, FakulteStore>();
            services.AddTransient<IBolumStore, BolumStore>();
            services.AddTransient<IProgramStore, ProgramStore>();
            services.AddTransient<IHarcStore, HarcStore>();
            services.AddTransient<IMufredatStore, MufredatStore>();
            services.AddTransient<IDersStore, DersStore>();
            services.AddTransient<IDersAcilanStore, DersAcilanStore>();
            services.AddTransient<IDersKayitStore, DersKayitStore>();
            services.AddTransient<IOgrenimTurStore, OgrenimTurStore>();
            services.AddTransient<IFakulteTurStore, FakulteTurStore>();
            services.AddTransient<IDonemStore, DonemStore>();
            services.AddTransient<IDonemTipStore, DonemTipStore>();
            services.AddTransient<IOgrenimDurumStore, OgrenimDurumStore>();
            services.AddTransient<IKayitNedenStore, KayitNedenStore>();
            services.AddTransient<IOgrenciStore, OgrenciStore>();
            services.AddTransient<IAkademisyenStore, AkademisyenStore>();
            services.AddTransient<IPersonelStore, PersonelStore>();
            services.AddTransient<ISinavStore, SinavStore>();
            services.AddTransient<ISinavTipStore, SinavTipStore>();
            services.AddTransient<ISinavTurStore, SinavTurStore>();
            services.AddTransient<ISinavKayitStore, SinavKayitStore>();
            services.AddTransient<IYabanciBasvuruStore, YabanciBasvuruStore>();

            services.AddTransient<IBinaStore, BinaStore>();
            services.AddTransient<IDerslikStore, DerslikStore>();
            services.AddTransient<IDerslikRezervStore, DerslikRezervStore>();
            services.AddTransient<INufusStore, NufusStore>();

            //services.AddTransient<ITenantStore, TenantStore>();
            services.AddTransient<IApiLogStore, ApiLogStore>();
                       
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            
            return services;
        }

        public static void GetDbContextOptions(DbContextOptionsBuilder builder, IConfiguration configuration)
        {
            var migrationsAssembly = "UniLife.Storage";
            var useSqlServer = Convert.ToBoolean(configuration["UniLife:UseSqlServer"] ?? "false");
            var dbConnString = useSqlServer
                ? configuration.GetConnectionString("DefaultConnection")
                : $"Filename={configuration.GetConnectionString("SqlLiteConnectionFileName")}";

            if (useSqlServer)
            {
                builder.UseSqlServer(dbConnString, sql => sql.MigrationsAssembly(migrationsAssembly));
            }
            else if (Convert.ToBoolean(configuration["UniLife:UsePostgresServer"] ?? "false"))
            {
                builder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"), sql => sql.MigrationsAssembly(migrationsAssembly));
            }
            else
            {
                builder.UseSqlite(dbConnString, sql => sql.MigrationsAssembly(migrationsAssembly));
            }
        }
        
        public static IIdentityServerBuilder AddIdentityServerStores(this IIdentityServerBuilder builder, IConfiguration configuration)
        => builder.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = x =>
                    ServiceCollectionExtensions.GetDbContextOptions(x, configuration);
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = x => ServiceCollectionExtensions.GetDbContextOptions(x, configuration);

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                
                options.TokenCleanupInterval = 3600; //In Seconds 1 hour
            });
        
    }
}