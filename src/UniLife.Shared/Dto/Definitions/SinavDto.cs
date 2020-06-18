using System;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class SinavDto : EntityDto<int>
    {
        public string Ad { get; set; }

        public int DersAcilanId { get; set; }
        public virtual DersAcilanDto DersAcilan { get; set; }

        public int SinavTipId { get; set; }
        public virtual SinavTipDto SinavTip { get; set; }

        public int SinavTurId { get; set; }
        public virtual SinavTurDto SinavTur { get; set; }

        public string SablonAd { get; set; }
        [MaxLength(100)]
        public int EtkiOran { get; set; }

        public bool IsKilit { get; set; }
        public DateTime Tarih { get; set; }
        public bool TarihIlan { get; set; }
        public string KisaAd { get; set; }

        public int OgrCount { get; set; }
    }
}
