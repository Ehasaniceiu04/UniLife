using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class ProgramDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }

        public int? BolumId { get; set; }
        public virtual BolumDto Bolum { get; set; }

        public int Kod { get; set; }
        public string AdEn { get; set; }
        public string KisaAd { get; set; }
        public string KisaAdEn { get; set; }
        public string OptKod { get; set; }
        public int AnaBolum { get; set; }
        public int ProgramTipId { get; set; }
        public int ProgramTurId { get; set; }
        public string Adres { get; set; }
        public string Iletisim { get; set; }
        public int? FakulteId { get; set; }
        public virtual FakulteDto Fakulte{ get; set; }
        public bool IsHazirlik { get; set; }
        public int AzamiSure { get; set; }
        public int NormalSure { get; set; }
        public string OsymKod { get; set; }
        public string OsymTur { get; set; }
        public bool StajDurum { get; set; }
        public decimal HarcTutar { get; set; }
        public int GenelKon { get; set; }
        public string BarajNot { get; set; }
        public string OsymKodBurslu { get; set; }
        public string OsymKodYariBurslu { get; set; }
        public int TuikKod { get; set; }
        public int Dil { get; set; }
        public string DiplomaAd { get; set; }
        public bool IsBologna { get; set; }
        public string BrnOgrOsymKod { get; set; }
        public string OsymKodCeyrekBurs { get; set; }
        public string DiplomaAdEn { get; set; }
        public int YoksisBirimKod { get; set; }
        public string MezuniyetUnvan { get; set; }
        public bool Durum { get; set; } = true;
        public bool Yillik { get; set; }
        public string Yerleske{ get; set; }
        public bool YoksisDurum { get; set; }
        public ICollection<MufredatDto> Mufredats { get; set; }
    }
}
