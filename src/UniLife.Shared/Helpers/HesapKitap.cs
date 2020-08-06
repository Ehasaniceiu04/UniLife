using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.Helpers
{
    public class HesapKitap
    {
        public readonly string NotHarflendirme = string.Empty;
        public HesapKitap()
        {

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            NotHarflendirme = root.GetSection("UniLife").GetSection("NotHarflendirme").Value;
            var appSetting = root.GetSection("ApplicationSettings");
        }
        public DersNotHarfDto OrtalamaHarflendir(double ortalama)
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

        //public readonly string _connectionString = string.Empty;
        //public string ConnectionString
        //{
        //    get => _connectionString;
        //}
    }
}
