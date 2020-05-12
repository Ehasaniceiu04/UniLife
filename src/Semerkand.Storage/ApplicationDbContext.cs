using Semerkand.Server.Models;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Storage.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ApiLogItem = Semerkand.Shared.DataModels.ApiLogItem;
using Message = Semerkand.Shared.DataModels.Message;
using UserProfile = Semerkand.Shared.DataModels.UserProfile;
using System.Runtime.CompilerServices;

namespace Semerkand.Storage
{
    //https://trailheadtechnology.com/entity-framework-core-2-1-automate-all-that-boring-boiler-plate/
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IApplicationDbContext
    {
        public DbSet<ApiLogItem> ApiLogs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        private IUserSession _userSession { get; set; }
        public DbSet<DbLog> Logs { get; set; }
        public DbSet<Universite> Universites{ get; set; }
        public DbSet<Fakulte> Fakultes{ get; set; }
        public DbSet<Bolum> Bolums{ get; set; }
        public DbSet<Program> Programs{ get; set; }
        public DbSet<Harc> Harcs { get; set; }
        public DbSet<Mufredat> Mufredats{ get; set; }
        public DbSet<Ders> Derss{ get; set; }
        public DbSet<OgrenimTur> OgrenimTurs{ get; set; }
        public DbSet<FakulteTur> FakulteTurs { get; set; }
        public DbSet<Donem> Donems{ get; set; }
        public DbSet<DonemTip> DonemTips{ get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserSession userSession) : base(options)
        {
            _userSession = userSession;
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API Does not follow foreign key naming convention
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Profile)
                .WithOne(b => b.ApplicationUser)
                .HasForeignKey<UserProfile>(b => b.UserId);

            modelBuilder.Entity<Tenant>()
                .HasOne(t => t.Owner)
                .WithOne(a => a.Tenant)
                .HasForeignKey<Tenant>(t => t.OwnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Ogrenci)
                .WithOne(o => o.ApplicationUser)
                .IsRequired(false);

            modelBuilder.ShadowProperties();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbLog>()
                .HasKey(d => d.Id)
                .IsClustered(false);
            modelBuilder.Entity<DbLog>()
                .HasIndex(d => d.TimeStamp)
                .IsClustered(true);

            modelBuilder.Entity<Message>().ToTable("Messages");

            modelBuilder.ApplyConfiguration(new MessageConfiguration());

            SetGlobalQueryFilters(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        private void SetGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType tp in modelBuilder.Model.GetEntityTypes())
            {
                Type t = tp.ClrType;

                // set Tenant Properties
                if (typeof(ITenant).IsAssignableFrom(t))
                {
                    MethodInfo method = SetGlobalQueryForTenantMethodInfo.MakeGenericMethod(t);
                    method.Invoke(this, new object[] { modelBuilder });
                }

                // set Soft Delete Property
                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    MethodInfo method = SetGlobalQueryForSoftDeleteMethodInfo.MakeGenericMethod(t);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private static readonly MethodInfo SetGlobalQueryForSoftDeleteMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDelete");

        private static readonly MethodInfo SetGlobalQueryForTenantMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForTenant");

        private static readonly MethodInfo SetGlobalQueryForSoftDeleteAndTenantMethodInfo = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDeleteAndTenant");

        public void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete
        {
            builder.Entity<T>().HasQueryFilter(item => !EF.Property<bool>(item, "IsDeleted"));
        }

        public void SetGlobalQueryForTenant<T>(ModelBuilder builder) where T : class, ISoftDelete
        {
            builder.Entity<T>().HasQueryFilter(item => (_userSession.DisableTenantFilter || EF.Property<int>(item, "TenantId") == _userSession.TenantId));
        }

        public void SetGlobalQueryForSoftDeleteAndTenant<T>(ModelBuilder builder) where T : class, ISoftDelete, ITenant
        {
            builder.Entity<T>().HasQueryFilter(
                item => !EF.Property<bool>(item, "IsDeleted") &&
                        (_userSession.DisableTenantFilter || EF.Property<int>(item, "TenantId") == _userSession.TenantId));
        }

        public override int SaveChanges()
        {
            ChangeTracker.SetShadowProperties(_userSession);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.SetShadowProperties(_userSession);
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
