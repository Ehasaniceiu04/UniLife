using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class MufredatDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public int Yil { get; set; }
        public string KisaAd { get; set; }
        public string AdEn { get; set; }
        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }
        public int Durum { get; set; }

        public int ProgramId { get; set; }
        public virtual ProgramDto Program { get; set; }
        public DateTime KararTarih { get; set; }
        public string KararAcik { get; set; }
        public int ProgDersGec { get; set; }
        public int AraSinavEo { get; set; }
        public int YariSonuSinavEo { get; set; }
        public int GecmeNot { get; set; }
        public int FinalBaraj { get; set; }

        public virtual ICollection<DersDto> Derss { get; set; }
    }
}
