using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class ProgramTurDto : EntityDto<int>
    {
        [Required]
        [MaxLength(100)]
        public string Ad { get; set; }
    }
}
