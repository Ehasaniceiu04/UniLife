using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
//using UniLife.Storage.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Extensions;

namespace UniLife.Storage.Stores
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

        public async Task<List<DersAcilanDto>> ByZorunlu(bool isZorunlu)
        {
            var dersAcilans = from da in _db.DersAcilans.Where(x => x.Zorunlu == isZorunlu)
                              join p in _db.Programs on da.ProgramId equals p.Id
                              join f in _db.Fakultes on p.FakulteId equals f.Id
                              select new DersAcilanDto
                              {
                                  Id = da.Id,
                                  Kod = da.Kod,
                                  Ad = da.Ad,
                                  ProgramAd = p.Ad,
                                  FakulteAd = f.Ad,
                                  Zorunlu = da.Zorunlu,
                                  SecmeliKodu = da.SecmeliKodu,
                                  Kredi = da.Kredi,
                                  Akts = da.Akts,
                                  DersId = da.DersId,
                                  ProgramId = da.ProgramId,
                                  DonemId = da.DonemId,
                                  KisaAd = da.KisaAd,
                                  Durum = da.Durum,
                                  Sinif = da.Sinif
                              };

            return _autoMapper.Map<List<DersAcilanDto>>(await dersAcilans.ToListAsync());
        }

        [Obsolete("Ders Aç içine fakulteID ve BOlumId eklendi. hata verir!")]
        public async Task<bool> CreateDersAcilanByDers(DersAcDto dersAcDto)
        {
            using (var context = _db.Context.Database.BeginTransaction())
            {
                //try
                //{
                IQueryable<DersAcilan> acilacakDersler;

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
                                          Zorunlu = d.Zorunlu,
                                          SecmeliKodu = d.SecmeliKodu
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
                                          Zorunlu = d.Zorunlu,
                                          SecmeliKodu = d.SecmeliKodu
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
                                          Zorunlu = d.Zorunlu,
                                          SecmeliKodu = d.SecmeliKodu
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
                                          Zorunlu = d.Zorunlu,
                                          SecmeliKodu = d.SecmeliKodu
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
                //}
                //catch (System.Exception ex)
                //{
                //    throw;
                //}
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


        public async Task<List<DersAcilanDto>> GetAcilanDerssByOgrenciId(int ogrenciId, int pageSinif, int pageDonemId)
        {
            var ogrenci = await _db.Ogrencis.FirstOrDefaultAsync(x => x.Id == ogrenciId);
            var donem = await _db.Donems.FirstOrDefaultAsync(x => x.Id == pageDonemId);

            var mufredat = await _db.Mufredats.FirstOrDefaultAsync(x => x.Id == ogrenci.MufredatId);

            var ustMufredatIds = await _db.Mufredats.Where(x => x.Yil >= mufredat.Yil && x.ProgramId == mufredat.ProgramId).Select(x => x.Id).ToListAsync();

            //var OgrenciNormalMufDersleri = await (_db.Derss.Where(x => x.MufredatId == ogrenci.MufredatId
            //                                          && x.Sinif == pageSinif
            //                                          && x.Durum == true
            //                                          && x.DonemTipId == donem.DonemTipId)).ToListAsync();

            


            var normalDersler = await (from d in _db.Derss.Where(x => x.MufredatId == ogrenci.MufredatId
                                                      && x.Sinif == pageSinif
                                                      && x.Durum == true
                                                      && x.DonemTipId == donem.DonemTipId)
                                       join da in _db.DersAcilans.Where(x => ustMufredatIds.Contains(x.MufredatId)
                                                                          && x.Sinif == pageSinif
                                                                          && x.DonemId == pageDonemId) on d.Kod equals da.Kod
                                       into ps
                                       from da in ps.DefaultIfEmpty()
                                       select new DersAcilanDto
                                       {
                                           Id = da != null ? da.Id : 999999999,
                                           Ad = da != null ? da.Ad : d.Ad,
                                           //Kod = da != null ? da.Kod + "(" + d.Kod + ")" : d.Kod,
                                           Kod = da != null ? da.Kod : d.Kod,
                                           Zorunlu = da != null ? da.Zorunlu : true,
                                           Kredi = da != null ? da.Kredi : d.Kredi,
                                           Akts = da != null ? da.Akts:d.Akts,
                                           Sinif = da != null ? da.Sinif:d.Sinif,
                                           ODTekrar = da.ODTekrar,
                                           ADKayit = da.ADKayit,
                                           Durum = da != null ? true : false,
                                       }).ToListAsync();
            //var normalDerslerMapped = _autoMapper.Map<List<DersAcilanDto>>(normalDersler);

            var kancaliDersler = await (from d in _db.Derss.Where(x => x.MufredatId == ogrenci.MufredatId
                                                      && x.Sinif == pageSinif
                                                      && x.DonemTipId == donem.DonemTipId)
                                        join dk in _db.DersKancas on d.Id equals dk.PasifMufredatDersId
                                        join da in _db.DersAcilans on dk.AktifMufredatDersId equals da.DersId
                                        select new DersAcilanDto
                                        {
                                            Id = da.Id,
                                            Ad = da.Ad,
                                            Kod = da.Kod + "(" + d.Kod + ")",
                                            Zorunlu = da.Zorunlu,
                                            Kredi = da.Kredi,
                                            Akts = da.Akts,
                                            Sinif = da.Sinif,
                                            ODTekrar = da.ODTekrar,
                                            ADKayit = da.ADKayit,
                                        }).ToListAsync();
            normalDersler.AddRange(kancaliDersler);

            return normalDersler;

        }

        public async Task<List<SubeDersAcilanDto>> PostDersAcilansByFilters(SinavDersAcDto sinavDersAcDto)
        {
            //var dersAcilans = from d in _db.DersAcilans.WhereIf(sinavDersAcDto.donemId != null, x => x.DonemId == sinavDersAcDto.donemId)
            //                                           .WhereIf(sinavDersAcDto.programId != null, x => x.ProgramId == sinavDersAcDto.programId)
            //                                           .WhereIf((sinavDersAcDto.sinif != null && sinavDersAcDto.sinif != 0), x => x.Sinif == sinavDersAcDto.sinif)
            //                                           .WhereIf(!string.IsNullOrEmpty(sinavDersAcDto.dersKodu), x => x.Kod == sinavDersAcDto.dersKodu)
            //                                           .Include(x => x.Akademisyen)

            //                  let cCount = d.DersKayits.Count()
            //                  select new DersAcilanDto
            //                  {
            //                      Id = d.Id,
            //                      Kod = d.Kod,
            //                      Ad = d.Ad,
            //                      Akademisyen = new AkademisyenDto { Ad = d.Akademisyen.Ad },
            //                      Sinif = d.Sinif,
            //                      Zorunlu = d.Zorunlu,
            //                      Sube=d.Sube,
            //                      OgrCount = cCount
            //                  };

            //var dersAcilans = from d in _db.DersAcilans.WhereIf(sinavDersAcDto.donemId != null, x => x.DonemId == sinavDersAcDto.donemId)
            //                                          .WhereIf(sinavDersAcDto.programId != null, x => x.ProgramId == sinavDersAcDto.programId)
            //                                          .WhereIf((sinavDersAcDto.sinif != null && sinavDersAcDto.sinif != 0), x => x.Sinif == sinavDersAcDto.sinif)
            //                                          .WhereIf(!string.IsNullOrEmpty(sinavDersAcDto.dersKodu), x => x.Kod == sinavDersAcDto.dersKodu)
            //                                          .Include(x => x.DersKayits)
            //                  group d by new { d.Kod, d.Ad }
            //                  into grp
            //                  select new SubeDersAcilanDto
            //                  {
            //                      //Id = grp.Key.Ad,
            //                      Kod = grp.Key.Kod,
            //                      Ad = grp.Key.Ad,
            //                      OgrCount = grp.Sum(t => t.DersKayits.Count()),
            //                      SubeCount = grp.Count()
            //                  };

            IQueryable<SubeDersAcilanDto> dersAcilans;

            if (sinavDersAcDto.SubeGroup)
            {
                dersAcilans = (from d in _db.DersAcilans.WhereIf(sinavDersAcDto.donemId != null, x => x.DonemId == sinavDersAcDto.donemId)
                                                       .WhereIf(sinavDersAcDto.programId != null, x => x.ProgramId == sinavDersAcDto.programId)
                                                       .WhereIf((sinavDersAcDto.sinif != null && sinavDersAcDto.sinif != 0), x => x.Sinif == sinavDersAcDto.sinif)
                                                       .WhereIf(!string.IsNullOrEmpty(sinavDersAcDto.dersKodu), x => x.Kod == sinavDersAcDto.dersKodu)
                                   //.Include(x => x.Akademisyen)

                               let cCount = d.DersKayits.Count()
                               select new DersAcilanDto
                               {
                                   Kod = d.Kod,
                                   Ad = d.Ad,
                                   OgrCount = cCount
                               }).GroupBy(g => new
                               {
                                   g.Kod,
                                   g.Ad,
                               }).Select(gcs => new SubeDersAcilanDto()
                               {
                                   Ad = gcs.Key.Ad,
                                   Kod = gcs.Key.Kod,
                                   SubeCount = gcs.Count(),
                                   OgrCount = gcs.Sum(x => x.OgrCount)
                               });
            }
            else
            {
                dersAcilans = (from d in _db.DersAcilans.WhereIf(sinavDersAcDto.donemId != null, x => x.DonemId == sinavDersAcDto.donemId)
                                                       .WhereIf(sinavDersAcDto.programId != null, x => x.ProgramId == sinavDersAcDto.programId)
                                                       .WhereIf((sinavDersAcDto.sinif != null && sinavDersAcDto.sinif != 0), x => x.Sinif == sinavDersAcDto.sinif)
                                                       .WhereIf(!string.IsNullOrEmpty(sinavDersAcDto.dersKodu), x => x.Kod == sinavDersAcDto.dersKodu)
                                   //.Include(x => x.Akademisyen)

                               let cCount = d.DersKayits.Count()
                               select new DersAcilanDto
                               {
                                   Id = d.Id,
                                   Kod = d.Kod,
                                   Ad = d.Ad,
                                   OgrCount = cCount
                               }).GroupBy(g => new
                               {
                                   g.Kod,
                                   g.Ad,
                                   g.Id,
                               }).Select(gcs => new SubeDersAcilanDto()
                               {
                                   Id = gcs.Key.Id,
                                   Ad = gcs.Key.Ad,
                                   Kod = gcs.Key.Kod,
                                   SubeCount = gcs.Count(),
                                   OgrCount = gcs.Sum(x => x.OgrCount)
                               });
            }



            return await dersAcilans.ToListAsync();
        }

        public async Task<List<ResDersAcilansByLongFilters>> DersAcilansByLongFilters(ReqDersAcilansByLongFilters reqDersAcilansByLongFilters)
        {
            var dersAcilans = from d in _db.DersAcilans.WhereIf(reqDersAcilansByLongFilters.DonemId != null, x => x.DonemId == reqDersAcilansByLongFilters.DonemId)
                                                       .WhereIf(reqDersAcilansByLongFilters.ProgramId != null, x => x.ProgramId == reqDersAcilansByLongFilters.ProgramId)
                                                       .WhereIf(reqDersAcilansByLongFilters.Sube != null, x => x.Sube == reqDersAcilansByLongFilters.Sube)
                                                       .WhereIf((reqDersAcilansByLongFilters.Sinif != null && reqDersAcilansByLongFilters.Sinif != 0), x => x.Sinif == reqDersAcilansByLongFilters.Sinif)
                                                       .WhereIf(!string.IsNullOrEmpty(reqDersAcilansByLongFilters.Kod), x => x.Kod == reqDersAcilansByLongFilters.Kod)
                                                       .WhereIf(!string.IsNullOrEmpty(reqDersAcilansByLongFilters.Ad), x => x.Ad == reqDersAcilansByLongFilters.Ad)
                              join p in _db.Programs on d.ProgramId equals p.Id
                              join a in _db.Akademisyens.WhereIf(!string.IsNullOrEmpty(reqDersAcilansByLongFilters.AkademisyenAd), x => x.Ad == reqDersAcilansByLongFilters.AkademisyenAd)
                              on d.AkademisyenId equals a.Id into AkademisyensLeft
                              from akaLeft in AkademisyensLeft.DefaultIfEmpty()
                              let cCount = d.DersKayits.Count()
                              select new ResDersAcilansByLongFilters
                              {
                                  DersAcilanId = d.Id,
                                  Sube = d.Sube,
                                  DersKod = d.Kod,
                                  DersAd = d.Ad,
                                  Teo = d.TeoSaat,
                                  Uyg = d.UygSaat,
                                  Kredi = d.Kredi,
                                  Akts = d.Akts,
                                  Sinif = d.Sinif,
                                  Kont = d.TopKont,
                                  Kayit = cCount,
                                  Zorunlu = d.Zorunlu,
                                  ProgramAd = p.Ad,
                                  AkademisyenAd = akaLeft.Ad

                              };

            return await dersAcilans.ToListAsync();
        }


        public async Task<List<DersAcilanDto>> GetKayitliDerssByOgrenciId(int ogrenciId, int sinif, int donemId)
        {
            var dersAcilans = from a in _db.DersAcilans.Where(x => x.Sinif == sinif && x.DonemId == donemId)
                              join k in _db.DersKayits.Where(x => x.OgrenciId == ogrenciId) on a.Id equals k.DersAcilanId
                              select new DersAcilanDto
                              {
                                  Id = a.Id,
                                  Kod = a.Kod,
                                  Ad = a.Ad,
                                  YerineSecilenId = k.DersYerineSecilenId,
                                  YerineSecilenAd = k.DersYerineSecilenAd,
                                  Zorunlu = a.Zorunlu,
                                  SecmeliKodu = a.SecmeliKodu,
                                  Kredi = a.Kredi,
                                  Akts = a.Akts,
                                  DersId = a.DersId,
                                  ProgramId = a.ProgramId,
                                  DonemId = a.DonemId,
                                  KisaAd = a.KisaAd,
                                  Durum = a.Durum,
                                  Sinif = a.Sinif,
                                  IsOnayli = k.IsOnayli,
                                  DersKayitId = k.Id
                              };

            return _autoMapper.Map<List<DersAcilanDto>>(await dersAcilans.ToListAsync());
        }

        public async Task<List<DersAcilanDto>> GetKayitliDerssByOgrenciIdDonemId(int ogrenciId, int donemId)
        {
            var dersAcilans = from a in _db.DersAcilans.Where(x => x.DonemId == donemId)
                              join k in _db.DersKayits.Where(x => x.OgrenciId == ogrenciId) on a.Id equals k.DersAcilanId
                              select a;

            return _autoMapper.Map<List<DersAcilanDto>>(await dersAcilans.ToListAsync());
        }






        public async Task PostCreateNewSubesAndUpdateOgrenciSubes(SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto)
        {
            var acilacakDersler = _db.DersAcilans.Where(x => x.Id == subeDersAcilanOgrenciCreateDto.DersAcilanId).FirstOrDefault();

            List<DersAcilan> acılacakSubeliDersler = new List<DersAcilan>();

            foreach (var item in subeDersAcilanOgrenciCreateDto.Subes.Where(x => x != 1)) //Sube Idsi 1 olanzaten default.ONU ALMA!
            {
                DersAcilan subeliDersAcilan = new DersAcilan();
                subeliDersAcilan = acilacakDersler.DeepClone();
                subeliDersAcilan.Sube = (int)item;
                subeliDersAcilan.Id = 0;
                acılacakSubeliDersler.Add(subeliDersAcilan);
            }

            using (var context = _db.Context.Database.BeginTransaction())
            {


                _db.DersAcilans.AddRange(acılacakSubeliDersler);

                await _db.SaveChangesAsync(CancellationToken.None);

                foreach (var item in acılacakSubeliDersler)
                {

                    var groupedOgrIds = subeDersAcilanOgrenciCreateDto.OgrenciIdsWithSubes.Where(x => x.Sube == item.Sube).Select(x => x.OgrId).ToList();
                    var kayits = _db.DersKayits.Where(x => x.DersAcilanId == subeDersAcilanOgrenciCreateDto.DersAcilanId && groupedOgrIds.Contains(x.OgrenciId)).ToList();

                    //var kayits = await from dk in _db.DersKayits.Where(x => x.DersAcilanId == subeDersAcilanOgrenciCreateDto.DersAcilanId)
                    //                   join l in subeDersAcilanOgrenciCreateDto.OgrenciIdsWithSubes on dk.OgrenciId equals l.OgrId
                    //                   where searchIds.Contains(l.Id)
                    //                   select p).Distinct().ToList();

                    //var kayits = await _db.DersKayits.Where(x => x.DersAcilanId == subeDersAcilanOgrenciCreateDto.DersAcilanId && (subeDersAcilanOgrenciCreateDto.OgrenciIdsWithSubes
                    //                                                                    .Where(x => x.Sube == item.Sube).Any(y=>y.OgrId == x.OgrenciId))).ToListAsync();

                    kayits.ForEach(x => x.DersAcilanId = item.Id);

                    _db.DersKayits.UpdateRange(kayits);
                }

                await _db.SaveChangesAsync(CancellationToken.None);



                context.Commit();
            }
        }

        public async Task<List<DersAcilanDto>> GetDersAcilanSubelerByDersKod(string dersKod)
        {
            var dersAcilans = from d in _db.DersAcilans.Where(x => x.Kod == dersKod).Include(x => x.Akademisyen)
                              let cCount = d.DersKayits.Count()
                              select new DersAcilanDto
                              {
                                  Id = d.Id,
                                  Kod = d.Kod,
                                  Ad = d.Ad,
                                  Akademisyen = new AkademisyenDto { Ad = d.Akademisyen.Ad },
                                  Sinif = d.Sinif,
                                  Zorunlu = d.Zorunlu,
                                  Kredi = d.Kredi,
                                  Sube = d.Sube,
                                  OgrCount = cCount
                              };

            return _autoMapper.Map<List<DersAcilanDto>>(await dersAcilans.ToListAsync());
        }

        public async Task<DersAcilanDto> GetDersAcilanSpecByDersAcId(int dersAcilanId)
        {
            var dersAcilan = from d in _db.DersAcilans.Where(x => x.Id == dersAcilanId)
                                                        .Include(x => x.Akademisyen)

                             let cCount = d.DersKayits.Count()
                             select new DersAcilanDto
                             {
                                 Id = d.Id,
                                 Kod = d.Kod,
                                 Ad = d.Ad,
                                 Akademisyen = new AkademisyenDto { Ad = d.Akademisyen.Ad },
                                 Kredi = d.Kredi,
                                 Sube = d.Sube,
                                 OgrCount = cCount
                             };

            return _autoMapper.Map<DersAcilanDto>(await dersAcilan.FirstOrDefaultAsync());
        }

        public async Task UpdateDersAcilanAkademsiyen(int dersAcilanId, int akademisyenId)
        {
            var dersAcilan = await _db.DersAcilans.FirstOrDefaultAsync(x => x.Id == dersAcilanId);
            dersAcilan.AkademisyenId = akademisyenId;
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<OgrenciDerslerDto>> GetDersSonucByOgrenciId(int ogrenciId)
        {
            var dersSonucs = await (from dk in _db.DersKayits.Where(x => x.OgrenciId == ogrenciId)
                                    join da in _db.DersAcilans on dk.DersAcilanId equals da.Id
                                    join d in _db.Donems on da.DonemId equals d.Id
                                    select new OgrenciDerslerDto
                                    {
                                        OgrenciId = dk.OgrenciId,
                                        DersAcilanId = da.Id,
                                        Sube = da.Sube,
                                        DersKod = da.Kod,
                                        DersAd = da.Ad,
                                        SonucDurum = ((DersSonucDurum)dk.SonucDurum).ToString(),
                                        Ort = dk.Ort,
                                        HarfNot = dk.HarfNot,
                                        Carpan = dk.Carpan,
                                        Durumu = ((DersSonuc)dk.Sonuc).ToString(),
                                        Sinif = da.Sinif,
                                        Donem = d.Ad,
                                        IsZorunlu = da.Zorunlu,
                                        Kredi = da.Kredi,
                                        Akts = da.Akts,
                                        AkademisyenId = da.AkademisyenId
                                    }).ToListAsync();

            var sinavSonucs = await (from sk in _db.SinavKayits.Where(x => x.OgrenciId == ogrenciId)
                                     join s in _db.Sinavs on sk.SinavId equals s.Id
                                     select new DersNotHesaplamaDto
                                     {
                                         Id = s.Id,
                                         DersAcilanId = s.DersAcilanId,
                                         SinavAd = s.Ad,
                                         OgrNot = sk.OgrNot,
                                         SEtkiOran = s.EtkiOran,
                                         MazeretiSinavKayitId = sk.MazeretiSinavKayitId,
                                         MazeretiSinavId = s.MazeretiSinavId
                                     }).ToListAsync();

            foreach (var item in dersSonucs)
            {
                var dersOgrencisSinavs = sinavSonucs.Where(x => x.DersAcilanId == item.DersAcilanId);

                item.SinavNotlari = String.Join(" ", dersOgrencisSinavs.Select(x => x.SinavAd + ":" + x.OgrNot));

                //foreach (var sinavSonuc in dersOgrencisSinavs)
                //{
                //    //if (dersOgrencisSinavs.Any(x => x.MazeretiSinavKayitId == sinavSonuc.MazeretiSinavKayitId))
                //    // Yani: Bu sinav Kayidinin ID'si başka bir sınav kayıdında MazeretiSinavKayitId olarak kayıtlımı?
                //    if (dersOgrencisSinavs.Any(x => x.MazeretiSinavId == sinavSonuc.MazeretiSinavId))
                //    {

                //    }
                //    else
                //    {
                //        item.Ort += sinavSonuc.OgrNot * (sinavSonuc.SEtkiOran / 100);
                //    }

                //}

            }

            return _autoMapper.Map<List<OgrenciDerslerDto>>(dersSonucs);
            //return null;
        }

        public async Task<List<DersAcilanDto>> GetDersAcilansByMufredat(int mufredatId, int donemId)
        {
            var mufredat = await _db.Mufredats.FirstOrDefaultAsync(x => x.Id == mufredatId);

            var mufredatdersKods = await _db.Derss.Where(x => x.MufredatId == mufredatId).Select(x=>x.Kod).ToListAsync();

            var ustMufredatIds = await _db.Mufredats.Where(x => x.Yil >= mufredat.Yil && x.ProgramId == mufredat.ProgramId).Select(x => x.Id).ToListAsync();

            var donemdeMufredatProgramininAcilanDersleri
                =await _db.DersAcilans.Where(x => x.DonemId == donemId
                                           && ustMufredatIds.Contains(x.MufredatId)
                                           && mufredatdersKods.Contains(x.Kod)).ToListAsync();

            donemdeMufredatProgramininAcilanDersleri.ForEach(x => { x.Mufredat = null; });

            return _autoMapper.Map<List<DersAcilanDto>>(donemdeMufredatProgramininAcilanDersleri);
        }
    }
}
