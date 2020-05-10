using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class ProgramDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }

        public int BolumId { get; set; }
        public BolumDto Bolum { get; set; }

        public ICollection<MufredatDto> Mufredats { get; set; }
    }
}
