using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class HarcDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }

        public int ProgramId { get; set; }
        public virtual ProgramDto Program { get; set; }

        public int DonemId { get; set; }
        public virtual DonemDto Donem { get; set; }

        public int NormalSure { get; set; }
        public int IlkUzatma { get; set; }
        public int TakipYillar { get; set; }

        //public ICollection<DersDto> Derss { get; set; }
    }
}
