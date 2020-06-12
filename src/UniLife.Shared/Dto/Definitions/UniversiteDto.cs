using UniLife.Shared.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class UniversiteDto: EntityDto<int>
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public ICollection<FakulteDto> Fakultes { get; set; }
    }
}