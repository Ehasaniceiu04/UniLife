using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class BolumDto : EntityDto<int>
    {
        [Required]
        [MaxLength(400)]
        public string Ad { get; set; }


        public int FakulteId { get; set; }
        public FakulteDto Fakulte { get; set; }

        public ICollection<ProgramDto> Programs { get; set; }
    }
}
