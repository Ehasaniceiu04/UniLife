using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.Dto.Definitions
{
    public class DersKayitDto : EntityDto<int>
    {
        public int DersAcilanId { get; set; }
        public virtual DersAcilanDto DersAcilan { get; set; }

        public int OgrenciId { get; set; }
        public virtual OgrenciDto Ogrenci { get; set; }

        public int? DersSecilenId { get; set; } //zorunluysa hem DersAcilanId a hem buraya, seçmeliyse burada seçilen DersAcilanId a yerine seçilen.
        public string DersSecilenAd { get; set; }
    }
}
