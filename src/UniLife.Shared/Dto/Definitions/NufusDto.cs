using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class NufusDto : EntityDto<int>
    {
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUserDto ApplicationUser { get; set; }
        public string Adres { get; set; }
        public int? IlId { get; set; }

        public virtual IlDto Il { get; set; }
        public DateTime DogumTarih { get; set; }
        public string DogumYer { get; set; }
        public string BabaAd { get; set; }

        public string Ilce { get; set; }
        public string Telefon { get; set; }
        public string Banka { get; set; }
        public string Sube { get; set; }
        public string SubeKod { get; set; }
        public string HesapNo { get; set; }
        public string Iban { get; set; }
    }
}
