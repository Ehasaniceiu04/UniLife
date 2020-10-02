using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class BinaDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public int? KampusId { get; set; }
        public virtual KampusDto Kampus { get; set; }
        public virtual ICollection<DerslikDto> Dersliks { get; set; }
    }
}
