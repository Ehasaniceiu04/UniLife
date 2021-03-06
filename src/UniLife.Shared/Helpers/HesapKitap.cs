using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.Helpers
{
    public class HesapKitap
    {
        private readonly string NotHarflendirme = string.Empty;
        //private readonly string OranBagilMukemmel = string.Empty;
        //private readonly string OranBagilCokIyi = string.Empty;
        //private readonly string OranBagilIyi = string.Empty;
        //private readonly string OranBagilOrtaUst = string.Empty;
        //private readonly string OranBagilOrta = string.Empty;
        //private readonly string OranBagilZayif = string.Empty;
        //private readonly string OranBagilKotu = string.Empty;

        private readonly string OranBagilKararJson = string.Empty;
        private readonly string NormalBagilKararJson = string.Empty;

        private readonly string OranBagilOranlar = string.Empty;
        private readonly string NormalBagilOranlar = string.Empty;

        public HesapKitap()
        {

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            NotHarflendirme = root.GetSection("UniLife").GetSection("NotHarflendirme").Value;
            //OranBagilMukemmel = root.GetSection("UniLife").GetSection("OranBagilMukemmel").Value;
            //OranBagilCokIyi = root.GetSection("UniLife").GetSection("OranBagilCokIyi").Value;
            //OranBagilIyi = root.GetSection("UniLife").GetSection("OranBagilIyi").Value;
            //OranBagilOrtaUst = root.GetSection("UniLife").GetSection("OranBagilOrtaUst").Value;
            //OranBagilOrta = root.GetSection("UniLife").GetSection("OranBagilOrta").Value;
            //OranBagilZayif = root.GetSection("UniLife").GetSection("OranBagilZayif").Value;
            //OranBagilKotu = root.GetSection("UniLife").GetSection("OranBagilKotu").Value;
            //OranBagilKararJson = root.GetSection("UniLife").GetSection("OranBagilKarar").Value;
            var appSetting = root.GetSection("ApplicationSettings");
        }

        public HesapKitap(double sinifOrtalama)
        {

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);


            var root = configurationBuilder.Build();
            var appSetting = root.GetSection("ApplicationSettings");

            NotHarflendirme = root.GetSection("UniLife").GetSection("NotHarflendirme").Value;
            OranBagilKararJson = root.GetSection("UniLife").GetSection("OranBagilKarar").Value;
            NormalBagilKararJson = root.GetSection("UniLife").GetSection("NormalBagilKarar").Value;

            OranBagilOranlar = root.GetSection("UniLife").GetSection(KararBagil(sinifOrtalama, OranBagilKararJson)).Value;
            NormalBagilOranlar = root.GetSection("UniLife").GetSection(KararBagil(sinifOrtalama, NormalBagilKararJson)).Value;
        }



        public DersNotHarfDto MutlakOrtalamaHarflendir(double ortalama)
        {

            List<DersNotHarfDto> harflendirmeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DersNotHarfDto>>(NotHarflendirme.Replace("\"[", "[").Replace("]\"", "]").Replace("\\", ""));

            foreach (var item in harflendirmeList)
            {
                if (item.az <= Math.Floor(ortalama) && item.cok >= Math.Floor(ortalama))
                {
                    item.ort = ortalama;
                    return item;
                }
            }

            throw new Exception($"Dikkat! Bu ortalamaya harf kodu bulunamadı:{ortalama}");
        }

        private string KararBagil(double sinifOrtalama, string BagilKararJson)
        {
            List<BagilKarar> bagilKarars = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BagilKarar>>(BagilKararJson.Replace("\"[", "[").Replace("]\"", "]").Replace("\\", ""));
            foreach (var item in bagilKarars)
            {
                if (item.az <= Math.Round(sinifOrtalama, 2) && item.cok > Math.Round(sinifOrtalama, 2))
                {
                    return item.snfDuzey;
                }
            }
            throw new DomainException(description: $"Sınıf ortalamasına({sinifOrtalama}) uygun sınıf düzeyi bulunamadı.");
        }

        public List<DersKayit> OranBagilOrtalamaHarflendir(List<DersKayit> ortalamayaGoreSiraliDersKayitlar)
        {
            try
            {
                List<BagilPersOran> harflendirmeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BagilPersOran>>(OranBagilOranlar.Replace("\"[", "[").Replace("]\"", "]").Replace("\\", ""));

                int i = 0;
                foreach (var item in harflendirmeList)
                {
                    double harfOgrSayisi = ortalamayaGoreSiraliDersKayitlar.Count * (item.persOgr / 100); //Kaç öğrencinin Bu harfi alacağını küsüratlı bulduk.
                    int yuvarlakHarfOgrSayisi = i + Convert.ToInt32(harfOgrSayisi);//Küsüratı yuvarladık. Aşağıdaki loopta bir önceki atamada kalınan öğrenciden devam etmek için i ile topladık.

                    for (; i < yuvarlakHarfOgrSayisi; i++) //Hesaplanan öğrenci sayısı kadar sıralı listemizi harflendirdik.
                    {
                        if (ortalamayaGoreSiraliDersKayitlar.Count()>= yuvarlakHarfOgrSayisi)
                        {
                            ortalamayaGoreSiraliDersKayitlar[i].HarfNot = item.harf;
                            ortalamayaGoreSiraliDersKayitlar[i].Carpan = item.carpan;
                            ortalamayaGoreSiraliDersKayitlar[i].GecDurum = item.gecti;
                        }
                    }
                }

                //Her harf gurubunun en küçük ortalamalı üyesi alınıp Listede aynı ortalama varmı diye bakılıyor
                //Eğer varsa onların harf notunuda bir üste, kendi bulunuduğu harfe çekiyoruz.
                foreach (var item in harflendirmeList)
                {
                    var harfGurubunu = ortalamayaGoreSiraliDersKayitlar.Where(x => x.HarfNot == item.harf);
                    if (harfGurubunu.Count() > 0)
                    {
                        var harfGurubununMinOrtalamasi = harfGurubunu.Min(x => x.Ort);
                        ortalamayaGoreSiraliDersKayitlar.Where(x => x.Ort == harfGurubununMinOrtalamasi).ToList().ForEach(x => { x.HarfNot = item.harf; });
                    }
                }

                return ortalamayaGoreSiraliDersKayitlar;
            }
            catch (Exception ex)
            {
                throw new DomainException(description: $"Dikkat! OranBagilOrtalamaHarflendir hatası oluştu!{ex.Message}");
            }
        }






        public List<DersKayit> NormalBagilOrtalamaHarflendir(List<DersKayit> ortalamayaGoreSiraliDersKayitlar)
        {
            try
            {
                List<DersNotHarfDto> harflendirmeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DersNotHarfDto>>(OranBagilOranlar.Replace("\"[", "[").Replace("]\"", "]").Replace("\\", ""));

                foreach (var dersKayit in ortalamayaGoreSiraliDersKayitlar)
                {
                    foreach (var item in harflendirmeList)
                    {
                        if (item.az <= dersKayit.Ort && item.cok > dersKayit.Ort)
                        {
                            dersKayit.HarfNot = item.harf;
                            dersKayit.Carpan = item.carpan;
                        }
                    }
                }


                return ortalamayaGoreSiraliDersKayitlar;
            }
            catch (Exception ex)
            {
                throw new DomainException(description: $"Dikkat! NormalBagilOrtalamaHarflendir hatası oluştu!{ex.Message}");
            }
        }


        //public readonly string _connectionString = string.Empty;
        //public string ConnectionString
        //{
        //    get => _connectionString;
        //}
    }
}
