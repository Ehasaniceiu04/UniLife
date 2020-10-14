using AutoMapper;
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
using UniLife.Shared.Dto;

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

        public async Task OnayKaldir(List<int> ids)
        {
            var kayitliDersler = _db.DersKayits.Where(x => ids.Contains(x.Id));
            await kayitliDersler.ForEachAsync(x => { x.IsOnayli = false; });

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
            var dersAcilan = await _db.DersAcilans.FirstOrDefaultAsync(x => x.Id == dersAcilanId);

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
                                                              EtkiOran = s.EtkiOran,
                                                              SinavId = s.Id
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




            //Mutlak
            if (dersKayitlari.Count < 11 || Ortalama > 79 || (dersAcilan.DersNedenId != (int)DersNedenEnum.Dönemsel))
            {
                await Mutlak(dersKayitlari, derseKayitliOgrlerinTumSinavlariButsuz);
            }

            //Oransal Bağıl
            else if (dersKayitlari.Count < 30)
            {
                await OransalBagil(dersKayitlari, derseKayitliOgrlerinTumSinavlariButsuz);
            }

            //Normal Bağıl
            else if (30 <= dersKayitlari.Count)
            {
                await NormalBagil(dersKayitlari, derseKayitliOgrlerinTumSinavlariButsuz);

            }
        }

        private async Task NormalBagil(List<DersKayit> dersKayitlari, List<OgrSinavSonucs> derseKayitliOgrlerinTumSinavlariButsuz)
        {
            List<DersKayit> normalBagilHesabaGiremeyenler = new List<DersKayit>();
            List<DersKayit> normalBagilHesabaGirenler = new List<DersKayit>();

            foreach (var item in dersKayitlari)
            {
                //final veya büt <15 ise  FF - hesaba katma
                if (derseKayitliOgrlerinTumSinavlariButsuz.Any(x => x.OgrenciId == item.OgrenciId &&
                                                             (x.SinavTipId == 2 && x.Not <= 15)))
                {

                    double tembelOgrOrtalama = 0;
                    var derseKayitliOgrlerinTumSinavlariButsuzOgrenci = derseKayitliOgrlerinTumSinavlariButsuz.Where(x => x.OgrenciId == item.OgrenciId);
                    foreach (var ogrencinot in derseKayitliOgrlerinTumSinavlariButsuzOgrenci)
                    {
                        if (derseKayitliOgrlerinTumSinavlariButsuzOgrenci.Select(x => x.MazeretiSinavKayitId).Contains(ogrencinot.SinavKayitId))
                        {

                        }
                        else
                        {
                            tembelOgrOrtalama += ogrencinot.Not * (ogrencinot.EtkiOran / 100);
                        }
                    }

                    var finalSinavi = derseKayitliOgrlerinTumSinavlariButsuzOgrenci.FirstOrDefault(x => x.SinavTipId == 2);

                    item.Carpan = 0;
                    item.Ort = Math.Round( tembelOgrOrtalama,2);
                    item.HarfNot = finalSinavi.Katilim == (int)SinavKatilimEnum.Katılmadı ? "GR" : "FF";
                    item.GecDurum = false;

                    normalBagilHesabaGiremeyenler.Add(item);
                }
                else
                {
                    normalBagilHesabaGirenler.Add(item);
                }
            }
            if (normalBagilHesabaGirenler.Count > 0)
            {
                double normalBagilHesabaGirenlerDouble = (double)normalBagilHesabaGirenler.Count;

                //Sinif Ortalaması
                List<DersKayit> KirkAltiOrtalamalilar = new List<DersKayit>();
                double OgrOrtalamaSum = 0;
                foreach (var item in normalBagilHesabaGirenler)
                {
                    double tekOgrenciOrtalama = 0;
                    var derseKayitliOgrlerinTumSinavlariButsuzOgrenci = derseKayitliOgrlerinTumSinavlariButsuz.Where(x => x.OgrenciId == item.OgrenciId);
                    foreach (var ogrencinot in derseKayitliOgrlerinTumSinavlariButsuzOgrenci)
                    {
                        if (derseKayitliOgrlerinTumSinavlariButsuzOgrenci.Select(x => x.MazeretiSinavKayitId).Contains(ogrencinot.SinavKayitId))
                        {

                        }
                        else
                        {
                            tekOgrenciOrtalama += ogrencinot.Not * (ogrencinot.EtkiOran / 100);
                            OgrOrtalamaSum += ogrencinot.Not * (ogrencinot.EtkiOran / 100);// 15 üstü öğrencilerin ortalamaları burada giriliyor.
                        }

                    }
                    item.Ort = Math.Round(tekOgrenciOrtalama,2);
                    if (tekOgrenciOrtalama < 40) // ortalaması 40 altı olanlar bağıl hesaba dahil ancak hesaptan sonra FF lemek üzere kayıt edildi.
                    {
                        item.Carpan = 0;
                        item.HarfNot = "FF";
                        item.GecDurum = false;
                        KirkAltiOrtalamalilar.Add(item);
                    }
                }

                double bagilOrtalama = OgrOrtalamaSum / normalBagilHesabaGirenlerDouble;

                //Standart Sapma

                double XKareToplam = 0;
                double XToplam = 0;

                foreach (var item in normalBagilHesabaGirenler)
                {
                    XToplam += item.Ort;
                    XKareToplam += Math.Pow(item.Ort, 2);
                }

                double kokIc = (normalBagilHesabaGirenlerDouble * XKareToplam) - Math.Pow(XToplam, 2);

                double standartSapma = (1 / normalBagilHesabaGirenlerDouble) * Math.Sqrt(kokIc);


                //Büt için Standart sapma ve ortalamanın saklanması Begin
                var dersAcilan = await _db.DersAcilans.FirstOrDefaultAsync(x => x.Id == normalBagilHesabaGirenler.FirstOrDefault().DersAcilanId);
                dersAcilan.SinifStandartSapma = standartSapma;
                dersAcilan.SinifOrtalamasi = bagilOrtalama;
                _db.DersAcilans.Update(dersAcilan);
                //Büt için Standart sapma ve ortalamanın saklanması End


                //TSkor from Z Puanı

                foreach (var item in normalBagilHesabaGirenler)
                {
                    item.TSkor = 50 + 10 * ((item.Ort - bagilOrtalama) / standartSapma);
                }



                HesapKitap hesapKitap = new HesapKitap(bagilOrtalama);

                List<DersKayit> harfliortalamaliDersKayits = hesapKitap.NormalBagilOrtalamaHarflendir(normalBagilHesabaGirenler);

                //bagıl hesaba girip ortalaması 40 altında olanları FF leme
                foreach (var item in harfliortalamaliDersKayits)
                {
                    DersKayit kirkAlti = KirkAltiOrtalamalilar.FirstOrDefault(x => x.Id == item.Id);
                    if (kirkAlti != null)
                    {
                        item.Carpan = kirkAlti.Carpan;
                        item.HarfNot = kirkAlti.HarfNot;
                        item.GecDurum = kirkAlti.GecDurum;
                    }
                }


                harfliortalamaliDersKayits.AddRange(normalBagilHesabaGiremeyenler);

                _db.DersKayits.UpdateRange(harfliortalamaliDersKayits);
            }
            else
            {
                _db.DersKayits.UpdateRange(normalBagilHesabaGiremeyenler);
            }

            Sinav sinav = await _db.Sinavs.FirstOrDefaultAsync(x => x.Id == derseKayitliOgrlerinTumSinavlariButsuz.FirstOrDefault(y => y.SinavTipId == (int)SinavTipEnum.Final).SinavId);
            sinav.HarfYontem = "Normal Bağıl";
            _db.Sinavs.Update(sinav);

            await _db.SaveChangesAsync(CancellationToken.None);
        }

        private async Task Mutlak(List<DersKayit> dersKayitlari, List<OgrSinavSonucs> derseKayitliOgrlerinTumSinavlariButsuz)
        {
            HesapKitap hesapKitap = new HesapKitap();

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


                var finalSinavi = derseKayitliOgrlerinTumSinavlariButsuzOgrenci.FirstOrDefault(x => x.SinavTipId == 2);

                if (finalSinavi != null && finalSinavi.Not < 50)
                {
                    hesapOrtalamalar.Add(new DerKayitHarfCapOrt
                    {
                        DersKayitId = item.Id,
                        Carpan = 0,
                        Harf = finalSinavi.Katilim == (int)SinavKatilimEnum.Katılmadı ? "GR" : "FF",
                        Ortalama = OgrOrtalama,
                        GecDurum = false
                    });

                }
                else
                {
                    DersNotHarfDto dersNotHarfDto = hesapKitap.MutlakOrtalamaHarflendir(OgrOrtalama);
                    hesapOrtalamalar.Add(new DerKayitHarfCapOrt
                    {
                        DersKayitId = item.Id,
                        Carpan = dersNotHarfDto.carpan,
                        Harf = dersNotHarfDto.harf,
                        Ortalama = dersNotHarfDto.ort,
                        GecDurum = dersNotHarfDto.gecti
                    });
                }


            }


            var dersKayitExists = await _db.DersKayits.Where(x => hesapOrtalamalar.Select(y => y.DersKayitId).Contains(x.Id)).ToListAsync();
            foreach (var item in dersKayitExists)
            {
                var denkgel = hesapOrtalamalar.FirstOrDefault(x => x.DersKayitId == item.Id);
                item.Carpan = denkgel.Carpan;
                item.HarfNot = denkgel.Harf;
                item.Ort = Convert.ToInt32(denkgel.Ortalama);
                item.GecDurum = denkgel.GecDurum;
            }


            _db.DersKayits.UpdateRange(dersKayitExists);
            Sinav sinav = await _db.Sinavs.FirstOrDefaultAsync(x => x.Id == derseKayitliOgrlerinTumSinavlariButsuz.FirstOrDefault(y => y.SinavTipId == (int)SinavTipEnum.Final).SinavId);
            sinav.HarfYontem = "Mutlak";
            _db.Sinavs.Update(sinav);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        private async Task OransalBagil(List<DersKayit> dersKayitlari, List<OgrSinavSonucs> derseKayitliOgrlerinTumSinavlariButsuz)
        {
            List<DersKayit> oransalBagilHesabaGiremeyenler = new List<DersKayit>();
            List<DersKayit> oransalBagilHesabaGirenler = new List<DersKayit>();

            foreach (var item in dersKayitlari)
            {
                //final veya büt <15 ise  FF - hesaba katma
                if (derseKayitliOgrlerinTumSinavlariButsuz.Any(x => x.OgrenciId == item.OgrenciId &&
                                                             (x.SinavTipId == (int)SinavTipEnum.Final && x.Not <= 15)))
                {

                    double tembelOgrOrtalama = 0;
                    var derseKayitliOgrlerinTumSinavlariButsuzOgrenci = derseKayitliOgrlerinTumSinavlariButsuz.Where(x => x.OgrenciId == item.OgrenciId);
                    foreach (var ogrencinot in derseKayitliOgrlerinTumSinavlariButsuzOgrenci)
                    {
                        if (derseKayitliOgrlerinTumSinavlariButsuzOgrenci.Select(x => x.MazeretiSinavKayitId).Contains(ogrencinot.SinavKayitId))
                        {

                        }
                        else
                        {
                            tembelOgrOrtalama += ogrencinot.Not * (ogrencinot.EtkiOran / 100);
                        }
                    }

                    var finalSinavi = derseKayitliOgrlerinTumSinavlariButsuzOgrenci.FirstOrDefault(x => x.SinavTipId == 2);

                    item.Carpan = 0;
                    item.Ort = Math.Round(tembelOgrOrtalama,2);
                    item.HarfNot = finalSinavi.Katilim == (int)SinavKatilimEnum.Katılmadı ? "GR" : "FF";
                    item.GecDurum = false;

                    oransalBagilHesabaGiremeyenler.Add(item);
                }
                else
                {
                    oransalBagilHesabaGirenler.Add(item);
                }
            }

            if (oransalBagilHesabaGirenler.Count > 0)
            {
                double oransalBagilHesabaGirenlerDouble = (double)oransalBagilHesabaGirenler.Count;

                List<DersKayit> KirkAltiOrtalamalilar = new List<DersKayit>();
                List<DersKayit> ElliAltiFinaller = new List<DersKayit>();
                

                double OgrOrtalamaSum = 0;
                foreach (var item in oransalBagilHesabaGirenler)
                {
                    double tekOgrenciOrtalama = 0;
                    var derseKayitliOgrlerinTumSinavlariButsuzOgrenci = derseKayitliOgrlerinTumSinavlariButsuz.Where(x => x.OgrenciId == item.OgrenciId);
                    foreach (var ogrencinot in derseKayitliOgrlerinTumSinavlariButsuzOgrenci)
                    {
                        if (derseKayitliOgrlerinTumSinavlariButsuzOgrenci.Select(x => x.MazeretiSinavKayitId).Contains(ogrencinot.SinavKayitId))
                        {

                        }
                        else
                        {
                            tekOgrenciOrtalama += ogrencinot.Not * (ogrencinot.EtkiOran / 100);// 15 üstü öğrencilerin ortalamaları burada giriliyor.
                            OgrOrtalamaSum += ogrencinot.Not * (ogrencinot.EtkiOran / 100);


                        }
                    }
                    item.Ort = Math.Round( tekOgrenciOrtalama,2);

                    var finalSinavi = derseKayitliOgrlerinTumSinavlariButsuzOgrenci.FirstOrDefault(x => x.SinavTipId == 2);

                    if (finalSinavi != null && finalSinavi.Not < 50)
                    {
                        item.Carpan = 0;
                        item.HarfNot = "FF";
                        item.GecDurum = false;
                        ElliAltiFinaller.Add(item);
                    }


                    if (tekOgrenciOrtalama < 40) // ortalaması 40 altı olanlar bağıl hesaba dahil ancak hesaptan sonra FF lemek üzere kayıt edildi.
                    {
                        item.Carpan = 0;
                        item.HarfNot = "FF";
                        item.GecDurum = false;

                        KirkAltiOrtalamalilar.Add(item);
                    }
                }

                double bagilOrtalama = OgrOrtalamaSum / oransalBagilHesabaGirenlerDouble;

                HesapKitap hesapKitap = new HesapKitap(bagilOrtalama);

                List<DersKayit> harfliortalamaliDersKayits = hesapKitap.OranBagilOrtalamaHarflendir(oransalBagilHesabaGirenler.OrderByDescending(x => x.Ort).ToList());


                //bagıl hesaba girip ortalaması 40 altında olanları FF leme
                //YUkarıda FF ledik ancak bağıl yüzünden 40 altı ortalama FF üstü çıkabilir . burada bir daha FF liyoruız.
                foreach (var item in harfliortalamaliDersKayits)
                {
                    DersKayit kirkAlti = KirkAltiOrtalamalilar.FirstOrDefault(x => x.Id == item.Id);
                    if (kirkAlti != null)
                    {
                        item.Carpan = 0;
                        item.HarfNot = "FF";
                        item.GecDurum = false;
                    }
                }

                //50 altı
                //YUkarıda FF ledik ancak bağıl yüzünden 50 altı final FF üstü çıkabilir . burada bir daha FF liyoruız.
                foreach (var item in harfliortalamaliDersKayits)
                {
                    DersKayit ellialti = ElliAltiFinaller.FirstOrDefault(x => x.Id == item.Id);
                    if (ellialti != null)
                    {
                        item.Carpan = 0;
                        item.HarfNot = "FF";
                        item.GecDurum = false;
                    }
                }


                harfliortalamaliDersKayits.AddRange(oransalBagilHesabaGiremeyenler);

                _db.DersKayits.UpdateRange(harfliortalamaliDersKayits);
            }
            else
            {
                _db.DersKayits.UpdateRange(oransalBagilHesabaGiremeyenler);
            }
            Sinav sinav = await _db.Sinavs.FirstOrDefaultAsync(x => x.Id == derseKayitliOgrlerinTumSinavlariButsuz.FirstOrDefault(y=>y.SinavTipId == (int)SinavTipEnum.Final).SinavId);
            sinav.HarfYontem = "Oransal Bağıl";
            _db.Sinavs.Update(sinav);
            await _db.SaveChangesAsync(CancellationToken.None);
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
                                               HarfNot = dk.HarfNot,
                                               IsGecti = dk.GecDurum,
                                               OgrSinavlarText = GetDigerSinavsByText(derseKayitliOgrlerinTumSinavlari, o)
                                           }).AsNoTracking().ToListAsync();

            return kayitliOgrenciler;
        }

        public async Task ButHarflendir(int dersAcilanId)
        {
            var dersAcilan = await _db.DersAcilans.FirstOrDefaultAsync(x => x.Id == dersAcilanId);

            //kaçtane öğrenci derse kayıtlı. ama sınava kayıtlı olmayabilir dikkat.
            var dersKayitlari = await _db.DersKayits.Where(x => x.DersAcilanId == dersAcilanId).AsNoTracking().ToListAsync();

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
                                                              EtkiOran = s.EtkiOran,
                                                              Katilim = sk.Katilim
                                                          }).AsNoTracking().ToListAsync();

            var derseKayitliOgrlerinTumSinavlariFinalsiz = derseKayitliOgrlerinTumSinavlari.Where(x => x.SinavTipId != (int)SinavTipEnum.Final).ToList();
            var derseKayitliOgrlerinTumSinavlariButsuz = derseKayitliOgrlerinTumSinavlari.Where(x => x.SinavTipId != (int)SinavTipEnum.But).ToList();

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




            //Mutlak
            if (dersKayitlari.Count < 11 || Ortalama > 79 || (dersAcilan.DersNedenId != (int)DersNedenEnum.Dönemsel))
            {
                await MutlakBut(dersKayitlari.Where(x => x.HarfNot == "FF" || x.HarfNot == "GR" || x.HarfNot == "DD" || x.HarfNot == "DC").ToList()
                              , derseKayitliOgrlerinTumSinavlariFinalsiz);
            }

            //Oransal Bağıl
            else if (dersKayitlari.Count < 30)
            {
                await OransalBagilBut(dersKayitlari, derseKayitliOgrlerinTumSinavlariFinalsiz);
            }

            //Normal Bağıl
            else if (30 <= dersKayitlari.Count)
            {
                await NormalBagilBut(dersKayitlari.Where(x => x.HarfNot == "FF" || x.HarfNot == "GR" || x.HarfNot == "DD" || x.HarfNot == "DC").ToList()
                                   , derseKayitliOgrlerinTumSinavlariFinalsiz);

            }
        }

        private async Task MutlakBut(List<DersKayit> buteGirebilenDersKayitlari, List<OgrSinavSonucs> derseKayitliOgrlerinTumSinavlariFinalsiz)
        {
            HesapKitap hesapKitap = new HesapKitap();

            List<DerKayitHarfCapOrt> hesapOrtalamalar = new List<DerKayitHarfCapOrt>();

            foreach (var item in buteGirebilenDersKayitlari)
            {
                double OgrOrtalama = 0;
                var derseKayitliOgrlerinTumSinavlariFinalsizOgrenci = derseKayitliOgrlerinTumSinavlariFinalsiz.Where(x => x.OgrenciId == item.OgrenciId);

                //Büte katılmadıysa işlemlerin finale göre kalması lazım.
                if (derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.FirstOrDefault(x => x.SinavTipId == (int)SinavTipEnum.But).Katilim == (int)SinavKatilimEnum.Katılmadı)
                {
                    continue;
                }

                //Burada bir örencinin bir dersi için 3-4 tane sınavı olabilir. Fİnal hariç.
                foreach (var ogrencinot in derseKayitliOgrlerinTumSinavlariFinalsizOgrenci)
                {
                    if (derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.Select(x => x.MazeretiSinavKayitId).Contains(ogrencinot.SinavKayitId))
                    {

                    }
                    else
                    {
                        OgrOrtalama += ogrencinot.Not * (ogrencinot.EtkiOran / 100);
                    }
                }


                var butSinavi = derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.FirstOrDefault(x => x.SinavTipId == (int)SinavTipEnum.But);

                if (butSinavi != null && butSinavi.Not < 50)
                {
                    hesapOrtalamalar.Add(new DerKayitHarfCapOrt
                    {
                        DersKayitId = item.Id,
                        Carpan = 0,
                        Harf = "FF",
                        Ortalama = OgrOrtalama
                    });
                }
                else
                {
                    DersNotHarfDto dersNotHarfDto = hesapKitap.MutlakOrtalamaHarflendir(OgrOrtalama);
                    hesapOrtalamalar.Add(new DerKayitHarfCapOrt
                    {
                        DersKayitId = item.Id,
                        Carpan = dersNotHarfDto.carpan,
                        Harf = dersNotHarfDto.harf,
                        Ortalama = dersNotHarfDto.ort
                    });
                }


            }

            //Databaseden derskayıtları çekilip spesifik kayıtlar için yaptıgımız düzenlemeler update edilir.
            var dersKayitExists = await _db.DersKayits.Where(x => hesapOrtalamalar.Select(y => y.DersKayitId).Contains(x.Id)).ToListAsync();
            foreach (var item in dersKayitExists)
            {
                var denkgel = hesapOrtalamalar.FirstOrDefault(x => x.DersKayitId == item.Id);
                item.Carpan = denkgel.Carpan;
                item.HarfNot = denkgel.Harf;
                item.Ort = Math.Round(denkgel.Ortalama,2);
            }


            _db.DersKayits.UpdateRange(dersKayitExists);

            await _db.SaveChangesAsync(CancellationToken.None);
        }

        private async Task OransalBagilBut(List<DersKayit> TumNotlar, List<OgrSinavSonucs> derseKayitliOgrlerinTumSinavlariFinalsiz)
        {
            List<DersKayit> buttenKalanDersKayits = new List<DersKayit>();
            List<DersKayit> buttenHarflenenDersKayits = new List<DersKayit>();

            var buteGirebilenDersKayitlari = TumNotlar.Where(x => x.HarfNot == "FF" || x.HarfNot == "GR" || x.HarfNot == "DD" || x.HarfNot == "DC").ToList();
            var finalHesapliDersKayitlari = TumNotlar.Where(x => x.HarfNot != "GR").ToList().DeepClone();
            //var eskiHesap = finalHesapliDersKayitlari;

            foreach (var item in buteGirebilenDersKayitlari)
            {

                double OgrOrtalama = 0;

                var derseKayitliOgrlerinTumSinavlariFinalsizOgrenci = derseKayitliOgrlerinTumSinavlariFinalsiz.Where(x => x.OgrenciId == item.OgrenciId);

                //Büte katılmadıysa işlemlerin finale göre kalması lazım.
                var butSinavi= derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.FirstOrDefault(x => x.SinavTipId == (int)SinavTipEnum.But);
                if (butSinavi==null || butSinavi.Katilim == (int)SinavKatilimEnum.Katılmadı)
                {
                    continue;
                }
                //if (derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.FirstOrDefault(x => x.SinavTipId == (int)SinavTipEnum.But).Katilim == (int)SinavKatilimEnum.Katılmadı)
                //{
                    
                //}

                foreach (var ogrencinot in derseKayitliOgrlerinTumSinavlariFinalsizOgrenci)
                {
                    if (derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.Select(x => x.MazeretiSinavKayitId).Contains(ogrencinot.SinavKayitId))
                    {

                    }
                    else
                    {
                        OgrOrtalama += ogrencinot.Not * (ogrencinot.EtkiOran / 100);
                    }
                }
                item.Ort = Math.Round(OgrOrtalama,2);

                //büt <50 ise  FF 
                if (derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.Any(x =>
                                                             (x.SinavTipId == (int)SinavTipEnum.But && x.Not < 50)))
                {
                    item.Carpan = 0;
                    item.Ort = OgrOrtalama;
                    item.HarfNot = "FF";
                    item.GecDurum = false;

                    buttenKalanDersKayits.Add(item);
                }
                else if (OgrOrtalama < 40)
                {
                    item.Carpan =  0;
                    item.HarfNot =  "FF";
                    item.GecDurum = false;
                    item.Ort =  OgrOrtalama;
                    buttenKalanDersKayits.Add(item);
                }
                else
                {
                    buttenHarflenenDersKayits.Add(item);
                }
            }

            if (buttenHarflenenDersKayits.Count > 0)
            {
                var orderedfinalHesapliDersKayitlari = finalHesapliDersKayitlari.OrderBy(x => x.Ort).ToList();

                foreach (var butHarfdersKayit in buttenHarflenenDersKayits)
                {
                    var ayniOrtalamaliDersKayit = finalHesapliDersKayitlari.FirstOrDefault(x => x.Ort == butHarfdersKayit.Ort);

                    if (ayniOrtalamaliDersKayit != null)
                    {
                        butHarfdersKayit.HarfNot = ayniOrtalamaliDersKayit.HarfNot;
                        butHarfdersKayit.Carpan = ayniOrtalamaliDersKayit.Carpan;
                        butHarfdersKayit.GecDurum = ayniOrtalamaliDersKayit.GecDurum;
                    }



                    for (int i = 0; i < orderedfinalHesapliDersKayitlari.Count - 1; i++)
                    {
                        if (orderedfinalHesapliDersKayitlari[i].Ort < butHarfdersKayit.Ort && butHarfdersKayit.Ort < orderedfinalHesapliDersKayitlari[i + 1].Ort)
                        {
                            butHarfdersKayit.HarfNot = orderedfinalHesapliDersKayitlari[i + 1].HarfNot;
                            butHarfdersKayit.Carpan = orderedfinalHesapliDersKayitlari[i + 1].Carpan;
                            butHarfdersKayit.GecDurum = orderedfinalHesapliDersKayitlari[i + 1].GecDurum;
                            break;
                        }
                        else if (orderedfinalHesapliDersKayitlari[i].Ort > butHarfdersKayit.Ort)
                        {
                            butHarfdersKayit.HarfNot = orderedfinalHesapliDersKayitlari[i].HarfNot;
                            butHarfdersKayit.Carpan = orderedfinalHesapliDersKayitlari[i].Carpan;
                            butHarfdersKayit.GecDurum = orderedfinalHesapliDersKayitlari[i].GecDurum;
                            break;
                        }
                        else if (butHarfdersKayit.Ort > orderedfinalHesapliDersKayitlari[i + 1].Ort)
                        {
                            butHarfdersKayit.HarfNot = orderedfinalHesapliDersKayitlari[i + 1].HarfNot;
                            butHarfdersKayit.Carpan = orderedfinalHesapliDersKayitlari[i + 1].Carpan;
                            butHarfdersKayit.GecDurum = orderedfinalHesapliDersKayitlari[i + 1].GecDurum;
                            continue;
                        }
                    }
                }


                buttenHarflenenDersKayits.AddRange(buttenKalanDersKayits);

                _db.DersKayits.UpdateRange(buttenHarflenenDersKayits);
            }
            else
            {
                _db.DersKayits.UpdateRange(buttenKalanDersKayits);
            }
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        private async Task NormalBagilBut(List<DersKayit> dersKayitlari, List<OgrSinavSonucs> derseKayitliOgrlerinTumSinavlariFinalsiz)
        {
            List<DersKayit> normalBagilButKalanlar = new List<DersKayit>();
            List<DersKayit> normalBagilButHarfliler = new List<DersKayit>();
            List<DersKayit> KirkAltiOrtalamalilar = new List<DersKayit>();
            foreach (var item in dersKayitlari)
            {

                double OgrOrtalama = 0;
                var derseKayitliOgrlerinTumSinavlariFinalsizOgrenci = derseKayitliOgrlerinTumSinavlariFinalsiz.Where(x => x.OgrenciId == item.OgrenciId);

                //Büte katılmadıysa işlemlerin finale göre kalması lazım.
                if (derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.FirstOrDefault(x => x.SinavTipId == (int)SinavTipEnum.But).Katilim == (int)SinavKatilimEnum.Katılmadı)
                {
                    continue;
                }


                foreach (var ogrencinot in derseKayitliOgrlerinTumSinavlariFinalsizOgrenci)
                {
                    if (derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.Select(x => x.MazeretiSinavKayitId).Contains(ogrencinot.SinavKayitId))
                    {

                    }
                    else
                    {
                        OgrOrtalama += ogrencinot.Not * (ogrencinot.EtkiOran / 100);
                    }
                }


                item.Ort = OgrOrtalama;
                //büt <50 ise  FF
                if (derseKayitliOgrlerinTumSinavlariFinalsizOgrenci.Any(x =>
                                                             (x.SinavTipId == (int)SinavTipEnum.But && x.Not < 50)))
                {
                    item.Carpan = 0;
                    
                    item.HarfNot = "FF";
                    item.GecDurum = false;

                    normalBagilButKalanlar.Add(item);
                }
                else if (OgrOrtalama < 40) //bütte Hesap yok. ORt 40 altı olanlar gider.
                {
                    item.Carpan = 0;
                    item.HarfNot = "FF";
                    item.GecDurum = false;

                    normalBagilButKalanlar.Add(item);
                }
                else
                {
                    normalBagilButHarfliler.Add(item);
                }
            }



            if (normalBagilButHarfliler.Count > 0)
            {

                var dersAcilan = await _db.DersAcilans.FirstOrDefaultAsync(x => x.Id == normalBagilButHarfliler.FirstOrDefault().DersAcilanId);

                //Sinif Ortalaması
                double bagilOrtalama = (double)dersAcilan.SinifOrtalamasi;

                //Standart Sapma
                double standartSapma = (double)dersAcilan.SinifStandartSapma;


                //TSkor from Z Puanı

                foreach (var item in normalBagilButHarfliler)
                {
                    item.TSkor = 50 + 10 * ((item.Ort - bagilOrtalama) / standartSapma);
                }


                HesapKitap hesapKitap = new HesapKitap(bagilOrtalama);

                List<DersKayit> harfliortalamaliDersKayits = hesapKitap.NormalBagilOrtalamaHarflendir(normalBagilButHarfliler);



                harfliortalamaliDersKayits.AddRange(normalBagilButKalanlar);

                _db.DersKayits.UpdateRange(harfliortalamaliDersKayits);
            }
            else
            {
                _db.DersKayits.UpdateRange(normalBagilButHarfliler);
            }

            await _db.SaveChangesAsync(CancellationToken.None);
        }



    }
}
