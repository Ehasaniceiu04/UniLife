using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.DataModels
{
    public class Ders : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Kod { get; set; }

        public int DonemId { get; set; }
        public virtual Donem Donem { get; set; }

        public int MufredatId { get; set; }
        public virtual Mufredat Mufredat { get; set; }
        public int FakulteId { get; set; }
        public virtual Fakulte Fakulte { get; set; }
        public int BolumId { get; set; }
        public virtual Bolum Bolum { get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        public int DersNedenId { get; set; }
        public virtual DersNeden DersNeden { get; set; }
        public int? DersDilId { get; set; }
        public virtual DersDil DersDil{ get; set; }
        public string KisaAd { get; set; }
        public int Akts { get; set; }
        public int GecmeNotu { get; set; }
        public string OptikKod { get; set; }
        public string AdEn { get; set; }
        public int UygSaat { get; set; }
        public int LabSaat { get; set; }
        public int TeoSaat { get; set; }
        public double Kredi { get; set; }
        public bool Durum { get; set; } = true;
        public bool Zorunlu { get; set; }
        public string SecmeliKodu { get; set; }
        public int Sinif { get; set; }

        public virtual ICollection<DersAcilan> DersAcilans { get; set; }
    }
}
