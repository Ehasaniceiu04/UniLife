using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class HarcDto : EntityDto<int>
    {
        [MaxLength(300)]
        public string Ad { get; set; }

        public int ProgramId { get; set; }
        public virtual ProgramDto Program { get; set; }

        public int DonemId { get; set; }
        public virtual DonemDto Donem { get; set; }

        public double Tutar { get; set; }
        public double OnTutar { get; set; }
        public double YabanciTutar { get; set; }

        public int NormalSure { get; set; }
        public int IlkUzatma { get; set; }
        public int TakipYillar { get; set; }

        //public ICollection<DersDto> Derss { get; set; }
    }
}
