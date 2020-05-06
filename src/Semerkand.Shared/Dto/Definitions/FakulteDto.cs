using Semerkand.Shared.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class FakulteDto : EntityDto<int>
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Isim { get; set; }


        public int UniversiteId { get; set; }
        public UniversiteDto Universite { get; set; }


        public ICollection<BolumDto> Bolums { get; set; }
    }
}