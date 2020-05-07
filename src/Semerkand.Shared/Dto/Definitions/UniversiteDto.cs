using Semerkand.Shared.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class UniversiteDto: EntityDto<int>
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }
        public ICollection<FakulteDto> Fakultes { get; set; }
    }
}