using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniLife.Shared;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class SinavStore : BaseStore<Sinav, SinavDto>, ISinavStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public SinavStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

       

        public async Task<List<SinavDto>> GetSinavListByAcilanDersId(int dersId)
        {

            //var sinavList = await (from s in _db.Sinavs.Where(x => x.DersAcilanId == dersId)
            //                            let cCount =
            //                            (
            //                              from c in _db.DersKayits
            //                              where s.DersAcilanId == c.DersAcilanId
            //                              select c
            //                            ).Count()
            //                            select new SinavDto
            //                            {
            //                                Id = s.Id,
            //                                Ad = s.Ad,
            //                                DersAcilanId = s.DersAcilanId,
            //                                SinavTipId = s.SinavTipId,
            //                                SinavTurId = s.SinavTurId,
            //                                SablonAd = s.SablonAd,
            //                                EtkiOran = s.EtkiOran,
            //                                IsKilit = s.IsKilit,
            //                                Tarih = s.Tarih,
            //                                TarihIlan = s.TarihIlan,
            //                                KisaAd = s.KisaAd,
            //                                OgrCount = cCount,
            //                                IsYayinli = s.IsYayinli
            //                            }).ToListAsync();



            var sinavList = await (from s in _db.Sinavs.Where(x => x.DersAcilanId == dersId)
                                   let cCount =
                                   (
                                     from c in _db.SinavKayits
                                     where s.Id == c.SinavId
                                     select c
                                   ).Count()
                                   select new SinavDto
                                   {
                                       Id = s.Id,
                                       Ad = s.Ad,
                                       DersAcilanId = s.DersAcilanId,
                                       SinavTipId = s.SinavTipId,
                                       SinavTurId = s.SinavTurId,
                                       SablonAd = s.SablonAd,
                                       EtkiOran = s.EtkiOran,
                                       IsKilit = s.IsKilit,
                                       Tarih = s.Tarih,
                                       TarihIlan = s.TarihIlan,
                                       KisaAd = s.KisaAd,
                                       OgrCount = cCount,
                                       IsYayinli = s.IsYayinli,
                                       MazeretiSinavId = s.MazeretiSinavId
                                   }).ToListAsync();


            //var ogrenciDto = await _autoMapper.ProjectTo<OgrenciDto>(_db.Ogrencis).FirstOrDefaultAsync(x => x.Id == id); //_db.Ogrencis.SingleOrDefaultAsync(t => t.Id == id);

            if (sinavList == null)
                throw new InvalidDataException($"Unable to find Sinav with ID: {dersId}");

            return sinavList;
        }

        public async Task PostBulkCreate(SinavDto sinavDto)
        {


            var enteredSinav = _autoMapper.Map<SinavDto, Sinav>(sinavDto);

            var selectedDersAcilans = await (from da in _db.DersAcilans.Where(x => sinavDto.DersAcilanIds.Contains(x.Id)).Include(dersAcilan=>dersAcilan.DersKayits)
                                             //join dk in _db.DersKayits on da.Id equals dk.DersAcilanId
                                             select da).ToListAsync();

            

            List<Sinav> generatedSinavList = new List<Sinav>();

            int islenecekAkademikTakvimKodu;
            switch (sinavDto.SinavTipId)
            {
                case (int)SinavTipEnum.Ara_Sinav:
                    islenecekAkademikTakvimKodu = 7;
                    break;
                case (int)SinavTipEnum.Final:
                    islenecekAkademikTakvimKodu = 9;
                    break;
                case (int)SinavTipEnum.But:
                    islenecekAkademikTakvimKodu = 10;
                    break;
                default:
                    throw new DomainException("Sınav tipi anlaşılamadı");
            }

            var getAkademikTakvimByDonem = await _db.AkademikTakvims.Where(x=>x.Kod == islenecekAkademikTakvimKodu).ToListAsync();

            foreach (var item in selectedDersAcilans)
            {
                //Eğer sadece aktif döneme sına voluşturulur falan derlerse.
                getAkademikTakvimByDonem = getAkademikTakvimByDonem.Where(x => x.DonemId == item.DonemId).ToList();

                var fakulteAkademikTakvim = getAkademikTakvimByDonem.FirstOrDefault(x => x.FakulteId == item.FakulteId)
                                            ??
                                            getAkademikTakvimByDonem.FirstOrDefault(x => x.FakulteId == null);

                Sinav sinav = new Sinav()
                {
                    Ad = enteredSinav.Ad,
                    DersAcilanId = item.Id,
                    SinavTipId = enteredSinav.SinavTipId,
                    SinavTurId = enteredSinav.SinavTurId,
                    SablonAd = enteredSinav.SablonAd,
                    EtkiOran = enteredSinav.EtkiOran,
                    IsKilit = enteredSinav.IsKilit,
                    Tarih = fakulteAkademikTakvim.BasTarih,  //enteredSinav.Tarih,
                    TarihIlan = enteredSinav.TarihIlan,
                    KisaAd = enteredSinav.KisaAd,
                };
                
                List<SinavKayit> sinavKayits = new List<SinavKayit>();
                foreach (var dkItems in item.DersKayits)
                {
                    if (sinavDto.SinavTipId == (int)SinavTipEnum.But)
                    {
                        if (dkItems.GecDurum == false || dkItems.HarfNot == "DC")
                        {
                            SinavKayit sinavKayit = new SinavKayit()
                            {
                                OgrenciId = dkItems.OgrenciId,
                                SinavId = item.Id
                            };
                            sinavKayits.Add(sinavKayit);
                        }
                    }
                    else
                    {
                        SinavKayit sinavKayit = new SinavKayit()
                        {
                            OgrenciId = dkItems.OgrenciId,
                            SinavId = item.Id
                        };
                        sinavKayits.Add(sinavKayit);
                    }
                                       
                }

                sinav.SinavKayits = sinavKayits;

                generatedSinavList.Add(sinav);
            }

            

            using (var context = _db.Context.Database.BeginTransaction())
            {
                await _db.Sinavs.AddRangeAsync(generatedSinavList);
                await _db.SaveChangesAsync(CancellationToken.None);
                context.Commit();
            }
        }

        public async Task<List<AkademisyenSinavDto>> GetSinavlarByAkademisyenId(int akaId)
        {
            var sinavList = from da in _db.DersAcilans.Where(x => x.AkademisyenId == akaId)
                            join s in _db.Sinavs on da.Id equals s.DersAcilanId
                            join st in _db.SinavTips on s.SinavTipId equals st.Id
                            join d in _db.Donems on da.DonemId equals d.Id
                            select new AkademisyenSinavDto
                            {
                                SinavId = s.Id,
                                DersAd = da.Ad,
                                SinavTip = st.Ad,
                                Donem = d.Ad,
                                Sinif = da.Sinif
                            };

            return await sinavList.ToListAsync();

        }

        public async Task Yayinla(int sinavId)
        {
            var sinav =  await _db.Sinavs.FirstOrDefaultAsync(x => x.Id == sinavId);
            sinav.IsYayinli = true;
            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
