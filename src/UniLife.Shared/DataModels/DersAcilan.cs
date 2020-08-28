using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataModels
{
    public class DersAcilan : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Kod { get; set; }

        public string EskiMufBagliDersKod { get; set; }

        public int DersId { get; set; }
        public virtual Ders Ders { get; set; }
        public int FakulteId { get; set; }
        public virtual Fakulte Fakulte { get; set; }
        public int BolumId { get; set; }
        public virtual Bolum Bolum { get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        public int MufredatId { get; set; }
        public virtual Mufredat Mufredat{ get; set; }
        public int DersNedenId { get; set; }
        public virtual DersNeden DersNeden { get; set; }
        public int? DersDilId { get; set; }
        public virtual DersDil DersDil { get; set; }

        public int Sube { get; set; } = 1;

        public int? AkademisyenId { get; set; }
        public virtual Akademisyen Akademisyen { get; set; }
        public int DonemId { get; set; }
        public virtual Donem Donem { get; set; }
        
        public string KisaAd { get; set; }
        public int Akts { get; set; }
        public int GecmeNotu { get; set; }
        public string OptikKod { get; set; }
        public string AdEn { get; set; }
        public int UygSaat { get; set; }
        public int LabSaat { get; set; }
        public int TeoSaat { get; set; }
        public double Kredi { get; set; }
        public bool Durum { get; set; }
        public bool Zorunlu { get; set; }
        public string SecmeliKodu { get; set; }
        public int Sinif { get; set; }

        public int? ODTekrar { get; set; }
        public int? ADKayit { get; set; }

        public int TopKont { get; set; } = 990;
        public int BolDisKont { get; set; }
        public int AltKont { get; set; }
        public int BolDisKota { get; set; } // Enum kurallar
        public int AltKota { get; set; } // Enum kurallar

        public virtual ICollection<DersKayit> DersKayits { get; set; }
        public virtual ICollection<Sinav> Sinavs { get; set; }
        public virtual ICollection<DerslikRezerv> DerslikRezervs { get; set; }
    }
}
