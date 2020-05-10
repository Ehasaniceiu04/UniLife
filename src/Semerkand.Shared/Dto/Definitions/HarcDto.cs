using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class HarcDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }

        public int ProgramId { get; set; }
        public ProgramDto Program { get; set; }

        //public ICollection<DersDto> Derss { get; set; }
    }
}
