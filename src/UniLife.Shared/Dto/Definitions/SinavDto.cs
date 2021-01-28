using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class SinavDto : EntityDto<int>
    {
        public string Ad { get; set; }

        public int DersAcilanId { get; set; }
        public virtual DersAcilanDto DersAcilan { get; set; }

        public string DersAcilanAd { get; set; }

        public int? SinavTipId { get; set; }
        public virtual SinavTipDto SinavTip { get; set; }

        public int? SinavTurId { get; set; }
        public virtual SinavTurDto SinavTur { get; set; }

        public string SablonAd { get; set; }
        //[Range(0, 100, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int? EtkiOran { get; set; } = 0;

        public bool IsKilit { get; set; }
        public bool IsYayinli { get; set; }
        public DateTime? Tarih { get; set; }
        public bool TarihIlan { get; set; }
        public string KisaAd { get; set; }
        public string HarfYontem { get; set; }

        public int OgrCount { get; set; }
        public int? MazeretiSinavId { get; set; } //Bu sadece bazı öğrenciler için gerçekleşiyor.

        public virtual IEnumerable<int> DersAcilanIds{ get; set; }
    }
}
