﻿using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.IO;
using System;
using UniLife.Shared.Helpers;

namespace UniLife.Storage.Stores
{
    public class DersKayitStore : BaseStore<DersKayit, DersKayitDto>, IDersKayitStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public DersKayitStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task DeleteByOgrId_DersId(int ogrenciId, int dersId)
        {
            var dersKayit = _db.DersKayits.SingleOrDefault(t => t.OgrenciId == ogrenciId && t.DersAcilanId == dersId);

            if (dersKayit == null)
            {
                //throw new InvalidDataException($"Unable to find Todo with ogrenciId: {ogrenciId} dersId : {dersId}");
            }
            else
            {
                _db.DersKayits.Remove(dersKayit);
                await _db.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<List<OgrenciDersKayitDto>> GetOgrenciDersKayitsByDers(int dersAcilanId)
        {
            var ogrenciDersKayits = from dk in _db.DersKayits.Where(x => x.DersAcilanId == dersAcilanId)
                                    join o in _db.Ogrencis on dk.OgrenciId equals o.Id
                                    select new OgrenciDersKayitDto
                                    {
                                        OgrId = o.Id,
                                        OgrNo = o.OgrNo,
                                        OgrAd = o.Ad,
                                        OgrSoy = o.Soyad,
                                        AlTip = dk.AlTip,
                                        HBN = dk.HBN,
                                        TSkor = dk.TSkor,
                                        HarfN = dk.HarfNot,
                                        GecDurum = dk.GecDurum ? "Geçti" : "Kaldı"
                                    };
            return await ogrenciDersKayits.ToListAsync();
        }



        public async Task HedefKaynakOgrAktar(HedefKaynakDto hedefKaynakDto)
        {
            var resultKaynak = hedefKaynakDto.KaynakIdList.Where(x => !hedefKaynakDto.HedefIdList.Contains(x));

            var silinecekler = await _db.DersKayits.Where(x => x.DersAcilanId == hedefKaynakDto.KaynakId && hedefKaynakDto.KaynakIdList.Contains(x.OgrenciId)).ToListAsync();

            _db.DersKayits.RemoveRange(silinecekler);

            List<DersKayit> dersKayits = new List<DersKayit>();
            foreach (var item in resultKaynak)
            {
                dersKayits.Add(new DersKayit
                {
                    DersAcilanId = hedefKaynakDto.HedefId,
                    OgrenciId = item,
                    DersYerineSecilenId = hedefKaynakDto.HedefId,

                });
            }
            _db.DersKayits.AddRange(dersKayits);
            await _db.SaveChangesAsync(CancellationToken.None);
        }


        public async Task HedefKaynakOgrDersKayit(HedefKaynakDto hedefKaynakDto)
        {
            var existDersKayits = await _db.DersKayits.Where(x => hedefKaynakDto.KaynakIdList.Contains(x.OgrenciId) && hedefKaynakDto.HedefIdList.Contains(x.DersAcilanId)).ToListAsync();

            List<DersKayit> dersKayits = new List<DersKayit>();
            foreach (var ogrenciId in hedefKaynakDto.KaynakIdList)
            {
                foreach (var dersAcId in hedefKaynakDto.HedefIdList)
                {
                    if (!existDersKayits.Any(x => x.DersAcilanId == dersAcId && x.OgrenciId == ogrenciId))
                    {
                        dersKayits.Add(new DersKayit
                        {
                            DersAcilanId = dersAcId,
                            OgrenciId = ogrenciId,
                            DersYerineSecilenId = dersAcId
                        });
                    }
                }
            }

            _db.DersKayits.AddRange(dersKayits);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos)
        {
            var silinecekler = from k in _db.DersKayits.Where(x => (x.OgrenciId == dersKayitDtos.FirstOrDefault().OgrenciId) && dersKayitDtos.Select(x => x.DersAcilanId).Contains(x.DersAcilanId))
                               select k;
            _db.DersKayits.RemoveRange(silinecekler.ToList());

            var dersKayits = _autoMapper.Map<IEnumerable<DersKayitDto>, IEnumerable<DersKayit>>(dersKayitDtos);
            _db.DersKayits.AddRange(dersKayits);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task Onayla(List<int> ids)
        {
            var kayitliDersler = _db.DersKayits.Where(x => ids.Contains(x.Id));
            await kayitliDersler.ForEachAsync(x => { x.IsOnayli = true; });

            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> PutUpdateOgrencisDersKayits(PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto)
        {

            var dersKayits = _db.DersKayits.Where(x => x.DersAcilanId == putUpdateOgrencisDersKayitsDto.SelectedDersAcilanId
                                                    && putUpdateOgrencisDersKayitsDto.OgrenciIds.Contains(x.OgrenciId));

            await dersKayits.ForEachAsync(x => x.DersAcilanId = putUpdateOgrencisDersKayitsDto.PointedDersAcilanId);

            return await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> PutUpdateOgrencisDersKayitsDeleteExSubes(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            using (var context = _db.Context.Database.BeginTransaction())
            {
                var dersKayits = _db.DersKayits.Where(x => reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.DersAcilanId));
                await dersKayits.ForEachAsync(x => x.DersAcilanId = reqEntityIdWithOtherEntitiesIds.EntityId);

                var updateOgr = await _db.SaveChangesAsync(CancellationToken.None);

                var fazlalikSubes = _db.DersAcilans.Where(x => reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.Id)).ToArray();
                _db.DersAcilans.RemoveRange(fazlalikSubes);

                await _db.SaveChangesAsync(CancellationToken.None);

                context.Commit();
                return updateOgr;
            }
        }

        public async Task Harflendir(int dersAcilanId)
        {
            HesapKitap hesapKitap = new HesapKitap();

            //kaçtane öğrenci derse kayıtlı. ama sınava kayıtlı olmayabilir dikkat.
            var dersKayitlari = await _db.DersKayits.Where(x => x.DersAcilanId == dersAcilanId).ToListAsync();

            //ara sınav fina büt mazeret ayrı kayıtlar olarak geliyor.
            var derseKayitliOgrlerinTumSinavlari = await (from s in _db.Sinavs.Where(x => x.DersAcilanId == dersAcilanId)
                                                          join st in _db.SinavTips on s.SinavTipId equals st.Id
                                                          join sk in _db.SinavKayits on s.Id equals sk.SinavId
                                                          select new OgrSinavSonucs
                                                          {
                                                              SinavKayitId = sk.Id,
                                                              OgrenciId = sk.OgrenciId,
                                                              SinavTipAd = st.Ad,
                                                              SinavTipId = st.Id,
                                                              MazeretiSinavKayitId = sk.MazeretiSinavKayitId,
                                                              Not = sk.OgrNot,
                                                              EtkiOran = s.EtkiOran
                                                          }).AsNoTracking().ToListAsync();

            var derseKayitliOgrlerinTumSinavlariButsuz = derseKayitliOgrlerinTumSinavlari.Where(x => x.SinavTipId != 3).ToList();

            #region Sinif Ortalaması
            double OrtlamaSum = 0;
            foreach (var item in derseKayitliOgrlerinTumSinavlariButsuz)
            {
                if (derseKayitliOgrlerinTumSinavlariButsuz.Select(x => x.MazeretiSinavKayitId).Contains(item.SinavKayitId))
                {

                }
                else
                {
                    OrtlamaSum += item.Not * (item.EtkiOran / 100);
                }
            }
            double Ortalama = OrtlamaSum / dersKayitlari.Count();
            #endregion


            List<DersKayit> bagilHesabaGirenler = new List<DersKayit>();

            //Mutlak
            if (dersKayitlari.Count < 11 || Ortalama > 79)
            {
                List<DerKayitHarfCapOrt> hesapOrtalamalar = new List<DerKayitHarfCapOrt>();

                foreach (var item in dersKayitlari)
                {
                    double OgrOrtalama = 0;
                    var derseKayitliOgrlerinTumSinavlariButsuzOgrenci = derseKayitliOgrlerinTumSinavlariButsuz.Where(x => x.OgrenciId == item.OgrenciId);
                    //Burada bir örencinin bir dersi için 3-4 tane sınavı olabilir. Büt hariç.
                    foreach (var ogrencinot in derseKayitliOgrlerinTumSinavlariButsuzOgrenci)
                    {
                        if (derseKayitliOgrlerinTumSinavlariButsuzOgrenci.Select(x => x.MazeretiSinavKayitId).Contains(ogrencinot.SinavKayitId))
                        {

                        }
                        else
                        {
                            OgrOrtalama += ogrencinot.Not * (ogrencinot.EtkiOran / 100);
                        }
                    }

                    DersNotHarfDto dersNotHarfDto = hesapKitap.OrtalamaHarflendir(OgrOrtalama);
                    hesapOrtalamalar.Add(new DerKayitHarfCapOrt {
                        DersKayitId= item.Id,
                        Carpan = dersNotHarfDto.carpan,
                        Harf= dersNotHarfDto.harf,
                        Ortalama= dersNotHarfDto.ort
                    });
                }


                var dersKayitExists = await _db.DersKayits.Where(x=> hesapOrtalamalar.Select(y=>y.DersKayitId).Contains(x.Id)).ToListAsync();
                foreach (var item in dersKayitExists)
                {
                    var denkgel = hesapOrtalamalar.FirstOrDefault(x => x.DersKayitId == item.Id);
                    item.Carpan = denkgel.Carpan;
                    item.HarfNot = denkgel.Harf;
                    item.Ort = Convert.ToInt32(denkgel.Ortalama);
                }
                _db.DersKayits.UpdateRange(dersKayitExists);

                await _db.SaveChangesAsync(CancellationToken.None);

                return;
            }

            //Oransal Bağıl
            else if (dersKayitlari.Count < 30)
            {
                foreach (var item in dersKayitlari)
                {
                    //final veya büt <50 ise direk FF
                    if (derseKayitliOgrlerinTumSinavlari.Any(x => x.OgrenciId == item.OgrenciId &&
                                                                 (x.SinavTipId == 2 && x.Not < 50) &&
                                                                 (x.SinavTipId == 3 && x.Not < 50)))
                    {
                        item.HarfNot = "FF";
                    }
                }
            }

            //Normal Bağıl
            else if (30 <= dersKayitlari.Count)
            {
                foreach (var item in dersKayitlari)
                {
                    //final veya büt <50 ise direk FF
                    if (derseKayitliOgrlerinTumSinavlari.Any(x => x.OgrenciId == item.OgrenciId &&
                                                                 (x.SinavTipId == 2 && x.Not < 50) &&
                                                                 (x.SinavTipId == 3 && x.Not < 50)))
                    {
                        item.HarfNot = "FF";
                    }
                }
            }

            //return kayitliOgrenciler;
        }

        private static string GetDigerSinavsByText(List<OgrSinavSonucs> derseKayitliOgrlerinTumSinavlari, Ogrenci o)
        {
            return String.Join(" ", derseKayitliOgrlerinTumSinavlari.Where(y => y.OgrenciId == o.Id).OrderBy(x => x.SinavTipAd).Select(x => x.SinavTipAd + ":" + x.Not));
        }

        public async Task<List<DersKayitOgrOrtalamaDto>> GetOgrDersHarfs(int dersAcilanId)
        {
            var derseKayitliOgrlerinTumSinavlari = await (from s in _db.Sinavs.Where(x => x.DersAcilanId == dersAcilanId)
                                                          join st in _db.SinavTips on s.SinavTipId equals st.Id
                                                          join sk in _db.SinavKayits on s.Id equals sk.SinavId
                                                          select new OgrSinavSonucs
                                                          {
                                                              OgrenciId = sk.OgrenciId,
                                                              SinavTipAd = st.Ad,
                                                              Not = sk.OgrNot,
                                                              EtkiOran = s.EtkiOran
                                                          }).AsNoTracking().ToListAsync();


            var kayitliOgrenciler = await (from dk in _db.DersKayits.Where(x => x.DersAcilanId == dersAcilanId)
                                           join o in _db.Ogrencis on dk.OgrenciId equals o.Id
                                           select new DersKayitOgrOrtalamaDto
                                           {
                                               DersKayitId = dk.Id,
                                               OgrenciId = o.Id,
                                               OgrenciAdSoyad = o.Ad + " " + o.Soyad,
                                               OgrenciNo = o.OgrNo,
                                               DersAcilanId = dk.DersAcilanId,
                                               OgrOrt = dk.Ort,
                                               OgrSinavlarText = GetDigerSinavsByText(derseKayitliOgrlerinTumSinavlari, o)
                                           }).AsNoTracking().ToListAsync();

            return kayitliOgrenciler;
        }
    }
}
