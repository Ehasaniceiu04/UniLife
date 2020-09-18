using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class DersKayitDto : EntityDto<int>
    {
        public int DersAcilanId { get; set; }
        public virtual DersAcilanDto DersAcilan { get; set; }

        public int OgrenciId { get; set; }
        public virtual OgrenciDto Ogrenci { get; set; }

        public int? DersYerineSecilenId { get; set; } //zorunluysa hem DersAcilanId a hem buraya, seçmeliyse burada seçilen DersAcilanId a yerine seçilen.
        public string DersYerineSecilenAd { get; set; }
        public double Ort { get; set; }
        public double Carpan { get; set; }
        public int SonucDurum { get; set; } // UniLife.Shared.Dto.DersSonucDurum enumu
        public int Sonuc { get; set; }
        public int HBN { get; set; }
        public double TSkor { get; set; }
        public string HarfNot { get; set; }
        public bool GecDurum { get; set; }
        public string AlTip { get; set; }
        public bool IsOnayli { get; set; }
    }
}
