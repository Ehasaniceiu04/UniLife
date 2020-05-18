using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semerkand.Shared.DataModels
{
    public class Mufredat : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public int Yil { get; set; }
        public string KisaAd { get; set; }
        public string AdEn { get; set; }
        public DateTime BasTarih{ get; set; }
        public DateTime BitTarih { get; set; }
        public int Durum { get; set; }
        public bool Aktif { get; set; }

        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        public DateTime KararTarih { get; set; }
        public string KararAcik { get; set; }
        public int ProgDersGec { get; set; }
        public int AraSinavEo { get; set; }
        public int YariSonuSinavEo { get; set; }
        public int GecmeNot { get; set; }
        public int FinalBaraj { get; set; }

        public virtual ICollection<Ders> Derss { get; set; }
    }
}
