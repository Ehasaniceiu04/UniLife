using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class DonemDto : EntityDto<int>
    {
        public int DonemTipId { get; set; }
        public DonemTipDto DonemTip { get; set; }
        public int Yil { get; set; }
        public string Ad { get; set; }
        public string KisaAd { get; set; }
        public string AdEn { get; set; }
        public string KisaAdEn { get; set; }
        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }
        public bool Durum { get; set; }
        public bool YokSisDurum { get; set; }
        public int YilTip { get; set; }

        public virtual ICollection<HarcDto> Harcs { get; set; }
    }
}
