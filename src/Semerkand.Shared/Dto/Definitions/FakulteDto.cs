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
        public string Ad { get; set; }


        public int UniversiteId { get; set; }
        public virtual UniversiteDto Universite { get; set; }

        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public string UniversiteAd
        //{
        //    get
        //    {
        //        return Universite.Ad;
        //    }
        //    set
        //    {
        //        Universite.Ad = value;
        //    }
        //}


        public ICollection<BolumDto> Bolums { get; set; }
    }
}