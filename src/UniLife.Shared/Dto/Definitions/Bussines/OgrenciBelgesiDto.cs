using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciBelgesiDto
    {
        public Guid AppUserId { get; set; }
        public string TCKN { get; set; }
        public long OgrNo { get; set; }
        public string AdSoyad { get; set; }
        public string BabaAd { get; set; }
        public DateTime DogumTarih { get; set; }
        public string DogumYer { get; set; }
        public string FakulteAd { get; set; }
        public string BolumAd { get; set; }
        public string Sinif { get; set; }
        public DateTime KayitTarih { get; set; }
        public string KayitNeden { get; set; }
    }
}
