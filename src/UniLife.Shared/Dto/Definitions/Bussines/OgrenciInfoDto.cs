using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciInfoDto
    {
        public string Soyad { get; set; }
        public long OgrNo { get; set; }
        public int OgrenciId { get; set; }
        public Guid UserId { get; set; }
        public string FakulteAd { get; set; }
        public string BolumAd { get; set; }
        public string ProgramAd { get; set; }
        public string MufredatAd { get; set; }

        public bool Durum { get; set; }
        public string Tckn { get; set; }
        public string Ad { get; set; }
        public string Email { get; set; }
        public int Sinif { get; set; }
        public string IlaveDonem { get; set; }
        public string KayitNedenAd { get; set; }
        public string OgrenimDurumAd { get; set; }
        public string DanismanAd { get; set; }
        public string CapYandal { get; set; }
        public string BilgNot { get; set; }
        public DateTime KayitTarih { get; set; }
        public DateTime? AyrilTarih { get; set; }

    }
}
