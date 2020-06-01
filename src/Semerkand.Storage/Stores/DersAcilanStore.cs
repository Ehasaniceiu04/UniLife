using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Storage.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Semerkand.Storage.Stores
{
    public class DersAcilanStore : BaseStore<DersAcilan, DersAcilanDto>, IDersAcilanStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public DersAcilanStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<bool> CreateDersAcilanByDers(DersAcDto dersAcDto)
        {
            using (var context = _db.Context.Database.BeginTransaction())
            {
                try
                {
                    IQueryable<DersAcilan> acilacakDersler;
                    IQueryable<DersAcilan> silinecekDersler;

                    if (dersAcDto.RefProgramSecilen == 55555 && dersAcDto.AcProgramSecilen != 55555)
                    {
                        acilacakDersler = from d in _db.Derss.Where(x => dersAcDto.DersAcIds.Contains(x.Id) && dersAcDto.AcSinifSecilenler.Contains(x.Sinif))
                                          join m in _db.Mufredats on d.MufredatId equals m.Id
                                          join p in _db.Programs.Where(y => dersAcDto.AcProgramSecilen == y.Id) on m.ProgramId equals p.Id
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
                                              MufredatId = d.MufredatId,
                                              Sinif = d.Sinif,
                                              TeoSaat = d.TeoSaat,
                                              UygSaat = d.UygSaat,
                                              Zorunlu = d.Zorunlu
                                          };
                    }
                    else if (dersAcDto.RefProgramSecilen != 55555 && dersAcDto.AcProgramSecilen == 55555)
                    {
                        acilacakDersler = from d in _db.Derss.Where(x => dersAcDto.DersAcIds.Contains(x.Id) && dersAcDto.AcSinifSecilenler.Contains(x.Sinif))
                                          join m in _db.Mufredats on d.MufredatId equals m.Id
                                          join p in _db.Programs.Where(y => dersAcDto.RefProgramSecilen == y.Id) on m.ProgramId equals p.Id
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
                                              MufredatId = d.MufredatId,
                                              Sinif = d.Sinif,
                                              TeoSaat = d.TeoSaat,
                                              UygSaat = d.UygSaat,
                                              Zorunlu = d.Zorunlu
                                          };
                    }
                    else if (dersAcDto.RefProgramSecilen == 55555 && dersAcDto.AcProgramSecilen == 55555)
                    {
                        acilacakDersler = from d in _db.Derss.Where(x => dersAcDto.DersAcIds.Contains(x.Id) && dersAcDto.AcSinifSecilenler.Contains(x.Sinif))
                                          join m in _db.Mufredats on d.MufredatId equals m.Id
                                          join p in _db.Programs.Where(y => dersAcDto.AcProgramSecenektekiler.Contains(y.Id)) on m.ProgramId equals p.Id
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
                                              MufredatId = d.MufredatId,
                                              Sinif = d.Sinif,
                                              TeoSaat = d.TeoSaat,
                                              UygSaat = d.UygSaat,
                                              Zorunlu = d.Zorunlu
                                          };
                    }
                    else if (dersAcDto.RefProgramSecilen != 55555 && dersAcDto.AcProgramSecilen != 55555)
                    {
                        acilacakDersler = from d in _db.Derss.Where(x => dersAcDto.DersAcIds.Contains(x.Id) && dersAcDto.AcSinifSecilenler.Contains(x.Sinif))
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
                                              ProgramId = dersAcDto.AcProgramSecilen,
                                              MufredatId = d.MufredatId,
                                              Sinif = d.Sinif,
                                              TeoSaat = d.TeoSaat,
                                              UygSaat = d.UygSaat,
                                              Zorunlu = d.Zorunlu
                                          };

                    }
                    else
                    {
                        throw new System.Exception("Açılacak dersler atama hatası");
                    }

                    IEnumerable<DersAcilan> sonuc = await acilacakDersler.ToListAsync();

                    var sonucProgramIds = sonuc.DistinctBy(d => d.ProgramId).Select(x => x.ProgramId);

                    var sonucSinifIds = sonuc.DistinctBy(d => d.Sinif).Select(x => x.Sinif);

                    var silinecek = _db.DersAcilans.Where(y => sonucProgramIds.Contains(y.ProgramId) && sonucSinifIds.Contains(y.Sinif));

                    _db.DersAcilans.RemoveRange(silinecek);

                    await _db.SaveChangesAsync(CancellationToken.None);


                    _db.DersAcilans.AddRange(sonuc);
                    await _db.SaveChangesAsync(CancellationToken.None);

                    

                    context.Commit();

                    return true;
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }

        public async Task<List<DersAcilanDto>> GetAcilanDersByFilterDto(DersAcilanFilterDto dersAcilanFilterDto)
        {
            var dersAcilans = from d in _db.DersAcilans
                              //join p in _db.Programs on d.ProgramId equals p.Id
                              where
                                  (dersAcilanFilterDto.ProgramSecilen.Contains(55555) ? dersAcilanFilterDto.ProgramSecenektekiler.Contains(d.ProgramId) : dersAcilanFilterDto.ProgramSecilen.Contains(d.ProgramId))
                                  && (dersAcilanFilterDto.SinifSecilen == null ? true : (dersAcilanFilterDto.SinifSecilen.Contains(d.Sinif)))
                                  && (dersAcilanFilterDto.DonemSecilen == 0 ? true : (d.DonemId == dersAcilanFilterDto.DonemSecilen))
                                  && (string.IsNullOrWhiteSpace(dersAcilanFilterDto.DersAd) ? true : d.Ad.Contains(dersAcilanFilterDto.DersAd))
                                  && (string.IsNullOrWhiteSpace(dersAcilanFilterDto.DersKod) ? true : d.Kod.Contains(dersAcilanFilterDto.DersKod))
                              select d;

            return _autoMapper.Map<List<DersAcilanDto>>(await dersAcilans.ToListAsync());
        }

        public async Task<List<DersAcilanDto>> GetAcilanDersByMufredatId(int mufredatId)
        {
            var dersAcilans = from d in _db.DersAcilans.Where(x => x.MufredatId == mufredatId)
                              select d;

            return _autoMapper.Map<List<DersAcilanDto>>(await dersAcilans.ToListAsync());
        }
    }
}
