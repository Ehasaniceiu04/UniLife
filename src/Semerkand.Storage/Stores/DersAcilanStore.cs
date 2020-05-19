using AutoMapper;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Semerkand.Storage.Stores
{
    public class DersAcilanStore : BaseStore<DersAcilan, DersAcilanDto>, IDersAcilanStore
    {
        private readonly IApplicationDbContext _db;
        public DersAcilanStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
        }

        public async Task<bool> CreateDersAcilanByDers(DersAcDto dersAcDto)
        {
            IQueryable<DersAcilan> acilacakDersler;

            if (dersAcDto.RefProgramSecilen.Contains(55555) || dersAcDto.AcProgramSecilen.Contains(55555))
            {
                acilacakDersler = from d in _db.Derss.Where(x => dersAcDto.DersAcIds.Contains(x.Id))
                                  join m in _db.Mufredats on d.MufredatId equals m.Id
                                  join p in _db.Programs.Where(y=> dersAcDto.AcProgramSecenektekiler.Contains(y.Id)) on m.ProgramId equals p.Id
                                  select new DersAcilan
                                  {
                                      Ad = d.Ad,
                                      AdEn = d.AdEn,
                                      Akts = d.Akts,
                                      DersId = d.Id,
                                      DonemId = dersAcDto.AcDonemSecilen, // TODO : eğer farklı döneme atması mümkün değilse d.DonemId olsun
                                      Durum = d.Durum,
                                      GecmeNotu = d.GecmeNotu,
                                      KisaAd = d.KisaAd,
                                      Kod = d.Kod,
                                      Kredi = d.Kredi,
                                      LabSaat = d.LabSaat,
                                      OptikKod = d.OptikKod,
                                      ProgramId = p.Id,
                                      Sinif = d.Sinif,
                                      TeoSaat = d.TeoSaat,
                                      UygSaat = d.UygSaat,
                                      Zorunlu = d.Zorunlu
                                  };

            }
            else
            {
                acilacakDersler = from d in _db.Derss.Where(x => dersAcDto.DersAcIds.Contains(x.Id))
                                  select new DersAcilan
                                  {
                                      Ad = d.Ad,
                                      AdEn = d.AdEn,
                                      Akts = d.Akts,
                                      DersId = d.Id,
                                      DonemId = dersAcDto.AcDonemSecilen, // TODO : eğer farklı döneme atması mümkün değilse d.DonemId olsun
                                      Durum = d.Durum,
                                      GecmeNotu = d.GecmeNotu,
                                      KisaAd = d.KisaAd,
                                      Kod = d.Kod,
                                      Kredi =d.Kredi,
                                      LabSaat =d.LabSaat,
                                      OptikKod =d.OptikKod,
                                      ProgramId = dersAcDto.AcProgramSecilen.First(),
                                      Sinif=d.Sinif,
                                      TeoSaat=d.TeoSaat,
                                      UygSaat =d.UygSaat,
                                      Zorunlu=d.Zorunlu
                                  };

            }

            _db.DersAcilans.AddRange(await acilacakDersler.ToListAsync());

            if (await _db.SaveChangesAsync(CancellationToken.None) > 0)
            {
                return true;
            }
            else
            {
                throw new System.Exception("CreateDersAcilanByDers hatası");
            }

        }
    }
}
