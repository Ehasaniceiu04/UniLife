using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.DataModels
{
    public class Bolum : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(400)]
        public string Ad { get; set; }

        public string KisaAd { get; set; }
        public int Kod { get; set; }
        public string AdEn { get; set; }
        public string OsymKod { get; set; }
        public int OgrenimTurId { get; set; }
        public virtual OgrenimTur OgrenimTur { get; set; }

        public int FakulteId { get; set; }
        public virtual Fakulte Fakulte { get; set; }

        public int OgrenimSure { get; set; }
        public bool Durum { get; set; }
        public string DiplomaAd { get; set; }
        public bool IsBologna { get; set; }
        public string DiplomaAdEn { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<Ogrenci> Ogrencis{ get; set; }
    }
}
