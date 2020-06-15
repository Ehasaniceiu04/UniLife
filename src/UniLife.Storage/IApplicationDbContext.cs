using UniLife.Server.Models;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace UniLife.Storage
{
    public interface IApplicationDbContext
    {
        public DbSet<ApiLogItem> ApiLogs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        //public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Universite> Universites { get; set; }
        public DbSet<Fakulte> Fakultes { get; set; }
        public DbSet<Bolum> Bolums { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Harc> Harcs{ get; set; }
        public DbSet<Mufredat> Mufredats { get; set; }
        public DbSet<Ders> Derss { get; set; }
        public DbSet<DersAcilan> DersAcilans { get; set; }
        public DbSet<DersKayit> DersKayits { get; set; }
        public DbSet<OgrenimTur> OgrenimTurs{ get; set; }
        public DbSet<FakulteTur> FakulteTurs{ get; set; }
        public DbSet<Donem> Donems{ get; set; }
        public DbSet<KayitNeden> KayitNedens{ get; set; }
        public DbSet<DonemTip> DonemTips{ get; set; }
        public DbSet<OgrenimDurum> OgrenimDurums { get; set; }
        public DbSet<Ogrenci> Ogrencis { get; set; }
        public DbSet<Akademisyen> Akademisyens { get; set; }


        //TODO Bunu böyle bırakacakmıyız.
        public Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; set; }
        //TODO Bunu böyle bırakacakmıyız.
        public ApplicationDbContext Context { get; set; }

        public void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete;

        public void SetGlobalQueryForSoftDeleteAndTenant<T>(ModelBuilder builder) where T : class, ISoftDelete, ITenant;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();

    }
}
