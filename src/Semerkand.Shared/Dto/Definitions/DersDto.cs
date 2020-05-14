using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class DersDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Kod { get; set; }

        public int DonemTipId { get; set; }
        public virtual DonemTipDto DonemTip { get; set; }

        public int MufredatID { get; set; }
        public virtual MufredatDto Mufredat { get; set; }
        public string KisaAd { get; set; }
        public int Akts { get; set; }
        public int GeçmeNotu { get; set; }
        public string OptikKod { get; set; }
        public string AdEn { get; set; }
        public int UygSaat { get; set; }
        public int LabSaat { get; set; }
        public int TeoSaat { get; set; }
        public double Kredi { get; set; }
        public bool Durum { get; set; }
        public int Zorunlu { get; set; }
        public int Sinif { get; set; }
        public int MyProperty { get; set; }
    }
}
