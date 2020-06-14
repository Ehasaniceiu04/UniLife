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

        //External
        public int DersId { get; set; }
        public virtual DersDto Ders { get; set; }

        public int ProgramId { get; set; }
        public virtual ProgramDto Program { get; set; }
        public string ProgramAd { get; set; }
        public int MufredatId { get; set; }
        public virtual MufredatDto Mufredat { get; set; }

        public string FakulteAd { get; set; }

        //public int  { get; set; }
        //External

        public int? OgretmenId { get; set; }
        public OgretmenDto Ogretmen { get; set; }

        public int DonemId { get; set; }
        public virtual DonemDto Donem { get; set; }

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
        public int? YerineSecilenId { get; set; }
        public string YerineSecilenAd { get; set; }
        public int Sinif { get; set; }

        public int? ODTekrar { get; set; }
        public int? ADKayit { get; set; }
    }
}
