using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class DersAcilanDto : EntityDto<int>
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Kod { get; set; }

        public string EskiMufBagliDersId { get; set; }
        //External
        public int DersId { get; set; }
        public virtual DersDto Ders { get; set; }
        public int? FakulteId { get; set; }
        public virtual FakulteDto Fakulte { get; set; }
        public string FakulteAd { get; set; }
        public int? BolumId { get; set; }
        public virtual BolumDto Bolum { get; set; }
        public string BolumAd { get; set; }
        public int? ProgramId { get; set; }
        public virtual ProgramDto Program { get; set; }
        public string ProgramAd { get; set; }
        public int? MufredatId { get; set; }
        public virtual MufredatDto Mufredat { get; set; }
        public int? DersNedenId { get; set; }
        public virtual DersNedenDto DersNeden { get; set; }
        public int? DersDilId { get; set; }
        public virtual DersDilDto DersDil { get; set; }
        public int? Sube { get; set; } = 1;

        public int OgrCount { get; set; }

        //External

        public int? AkademisyenId { get; set; }
        public AkademisyenDto Akademisyen { get; set; }
        public string AkademisyenAd { get; set; }

        public int? DonemId { get; set; }
        public virtual DonemDto Donem { get; set; }

        public string KisaAd { get; set; }
        public int? Akts { get; set; }
        
        public string OptikKod { get; set; }
        public string AdEn { get; set; }
        public int UygSaat { get; set; }
        public int LabSaat { get; set; }
        public int TeoSaat { get; set; }
        public double? Kredi { get; set; }
        public bool Durum { get; set; } = true;
        public bool Zorunlu { get; set; }
        public string SecmeliKodu { get; set; }
        public int? YerineSecilenId { get; set; }
        public string YerineSecilenAd { get; set; }
        public int? Sinif { get; set; }
        public bool IsYillik { get; set; } = false;
        public bool IsKurul { get; set; }
        public bool IsKurulSon { get; set; }

        public int? ODTekrar { get; set; }
        public int? ADKayit { get; set; }
        public double? SinifOrtalamasi { get; set; }
        public double? SinifStandartSapma { get; set; }
        public int TopKont { get; set; } = 990;
        public int BolDisKont { get; set; }
        public int AltKont { get; set; }
        public int BolDisKota { get; set; } // Enum kurallar
        public int AltKota { get; set; } // Enum kurallar

        public string TU
        {
            get { return TeoSaat.ToString() + "+" + UygSaat.ToString(); }
        }

        public bool Kayitta { get; set; }
        public bool IsOnayli { get; set; }
        public int DersKayitId { get; set; }

        public virtual ICollection<SinavDto> Sinavs { get; set; }
    }
}
