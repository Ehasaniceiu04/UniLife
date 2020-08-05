using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Helpers
{
    public class HesapKitap
    {
        private readonly IConfiguration _configuration;

        public HesapKitap(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DersNotHarfDto OrtalamaHarflendir(int ortalama)
        {
            
            List<DersNotHarfDto> harflendirmeList = Newtonsoft.Json.JsonConvert.DeserializeObject<DersNotHarfDto[]>(_configuration["UniLife:NotHarflendirme"].ToString()).ToList<DersNotHarfDto>();

            foreach (var item in harflendirmeList)
            {
                if (item.az<= ortalama && item.cok<=ortalama)
                {
                    item.ort = ortalama;
                    return item;
                }
            }

            throw new Exception($"Dikkat! Bu ortalamaya harf kodu bulunamadı:{ortalama}");
        }
    }
}
