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
        public DbSet<Personel> Personels { get; set; }
        public DbSet<Sinav> Sinavs { get; set; }
        public DbSet<SinavTip> SinavTips { get; set; }
        public DbSet<SinavTur> SinavTurs { get; set; }
        public DbSet<SinavKayit> SinavKayits { get; set; }
        public DbSet<Bina> Binas { get; set; }
        public DbSet<Derslik> Dersliks { get; set; }
        public DbSet<DerslikRezerv> DerslikRezervs { get; set; }
        public DbSet<YabanciBasvuru> YabancıBasvurus { get; set; }
        public DbSet<Il> Ils { get; set; }
        public DbSet<Nufus> Nufuss { get; set; }
        public DbSet<Askerlik> Askerliks { get; set; }
        public DbSet<Osym> Osyms { get; set; }
        public DbSet<OgrenciDiger> OgrenciDigers { get; set; }
        public DbSet<OgrCeza> OgrCezas { get; set; }
        public DbSet<OgrDondur> OgrDondurs { get; set; }
        public DbSet<OgrGecis> OgrGeciss { get; set; }
        public DbSet<OgrStaj> OgrStajs { get; set; }
        public DbSet<OgrTez> OgrTezs { get; set; }
        public DbSet<AkademikTakvim> AkademikTakvims { get; set; }
        public DbSet<OgrHarc> OgrHarcs { get; set; }
        public DbSet<BursTip> BursTips { get; set; }
        public DbSet<CezaTip> CezaTips { get; set; }
        public DbSet<ProgramTur> ProgramTurs { get; set; }
        public DbSet<DersDil> DersDils { get; set; }
        public DbSet<DersNeden> DersNedens { get; set; }
        public DbSet<SinavKriter> SinavKriters { get; set; }
        public DbSet<DersKanca> DersKancas { get; set; }
        public DbSet<OgrBursBasari> OgrBursBasaris { get; set; }
        public DbSet<Kampus> Kampuss { get; set; }
        public DbSet<PersonelTask> PersonelTasks { get; set; }
        public DbSet<UserProgramYetki> UserProgramYetkis { get; set; }


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
