using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;

namespace UniLife.Shared.DataModels
{
    public class Program : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }

        public int BolumId { get; set; }
        public virtual Bolum Bolum { get; set; }
        public int FakulteId { get; set; }
        //public virtual Fakulte Fakulte{ get; set; }
        public int Kod { get; set; }
        public string AdEn { get; set; }
        public string KisaAd { get; set; }
        public string OptKod{ get; set; }
        public int AnaBolum { get; set; }
        public int ProgramTipId { get; set; }
        public int ProgramTurId { get; set; }
        public string Adres { get; set; }
        public string Iletisim { get; set; }
        
        public bool IsHazırlık { get; set; }
        public int AzamiSure { get; set; }
        public int NormalSure { get; set; }
        public string OsymKod { get; set; }
        public string OsymTur { get; set; }
        public bool StajDurum { get; set; }
        public decimal HarcTutar { get; set; }
        public int GenelKon { get; set; }
        public string BarajNot { get; set; }
        public string OsymKodBurslu { get; set; }
        public string OsymKodYariBurslu { get; set; }
        public int TuikKod { get; set; }
        public int Dil { get; set; }
        public string DiplomaAd { get; set; }
        public bool IsBologna { get; set; }
        public string BrnOgrOsymKod { get; set; }
        public string OsymKodCeyrekBurs { get; set; }
        public string DiplomaAdEn { get; set; }



        public virtual ICollection<Ogrenci> Ogrencis { get; set; }
        public virtual ICollection<Harc> Harcs { get; set; }

        public virtual ICollection<DersAcilan> DersAcilans{ get; set; }
        public virtual ICollection<Ders> Derss { get; set; }
    }
}
