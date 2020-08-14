using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.DataModels
{
    public class Fakulte : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Kod { get; set; }
        public int UniversiteId { get; set; }
        public virtual Universite Universite { get; set; }


        public string KisaAd { get; set; }

        public string AdEn { get; set; }
        public string AdEnKisa { get; set; }
        public string EPosta { get; set; }
        public string Tel { get; set; }
        public string Adres { get; set; }
        public string Adres2 { get; set; }
        public string Faks { get; set; }
        public string Web { get; set; }
        public int IlceId { get; set; }
        public int OgrenimTurId { get; set; }
        public virtual OgrenimTur OgrenimTur { get; set; }
        public int OgrenimSure { get; set; }
        public int IlKod { get; set; }
        public int Tip { get; set; }
        
        public bool IsBologna { get; set; }
        public string BolognaIcerikTR { get; set; }
        public string BolognaIcerikEN { get; set; }
        public int BirimID { get; set; }
        public bool Durum { get; set; }

        public virtual ICollection<Bolum> Bolums { get; set; }
        //public virtual ICollection<Program> Programs{ get; set; }

        public virtual ICollection<Ogrenci> Ogrencis{ get; set; }
        public virtual ICollection<Ders> Derss{ get; set; }
        public virtual ICollection<DersAcilan> DersAcilans { get; set; }

    }
}
