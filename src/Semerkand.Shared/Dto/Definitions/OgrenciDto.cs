using System;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.Dto.Definitions
{
    public class OgrenciDto : EntityDto<int>
    {
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUserDto ApplicationUser { get; set; }

        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }

        public string OgrNo { get; set; }


        //public int FakulteId { get; set; }
        //public virtual FakulteDto Fakulte { get; set; }
        //public int BolumId { get; set; }
        //public virtual BolumDto Bolum { get; set; }
        //public int ProgramId { get; set; }
        //public virtual ProgramDto Program { get; set; }

        //public int KayitNedenId { get; set; }
        //public virtual KayitNeden KayitNeden { get; set; }

        //public int OgrenimDurumId { get; set; }
        //public virtual OgrenimDurum OgrenimDurum { get; set; }

        //public string AskerDurum { get; set; }
        //public DateTime KayitTarih { get; set; }
        //public DateTime? AyrilTarih { get; set; }
        //public string AnaOgrNo { get; set; }
    }
}
