using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class BolumDto : EntityDto<int>
    {
        [Required]
        [MaxLength(400)]
        public string Ad { get; set; }

        public string KisaAd { get; set; }
        public int Kod { get; set; }
        public string AdEn { get; set; }
        public string OsymKod { get; set; }
        public int OgrenimTurId { get; set; }
        public virtual OgrenimTurDto OgrenimTur { get; set; }

        public int FakulteId { get; set; }
        public virtual FakulteDto Fakulte { get; set; }

        public int OgrenimSure { get; set; }
        public bool Durum { get; set; }
        public string DiplomaAd { get; set; }
        public bool IsBologna { get; set; }
        public string DiplomaAdEn { get; set; }

        public virtual ICollection<ProgramDto> Programs { get; set; }
        public virtual ICollection<ApplicationUserDto> Ogrencis { get; set; }
    }
}
