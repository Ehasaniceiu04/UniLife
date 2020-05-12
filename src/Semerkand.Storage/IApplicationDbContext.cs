﻿using Semerkand.Server.Models;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Semerkand.Storage
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
        public DbSet<OgrenimTur> OgrenimTurs{ get; set; }
        public DbSet<FakulteTur> FakulteTurs{ get; set; }
        public DbSet<Donem> Donems{ get; set; }
        public DbSet<KayitNeden> KayitNedens{ get; set; }
        public DbSet<DonemTip> DonemTips{ get; set; }
        public DbSet<OgrenimDurum> OgrenimDurums { get; set; }


        public void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete;

        public void SetGlobalQueryForSoftDeleteAndTenant<T>(ModelBuilder builder) where T : class, ISoftDelete, ITenant;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();

    }
}
