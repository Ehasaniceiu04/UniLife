using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class DersDto : EntityDto<int>
    {
        public string Ad { get; set; }
        public string Kod { get; set; }

        //public int? DonemId { get; set; }
        //public virtual DonemDto Donem { get; set; }

        public int? DonemTipId { get; set; }
        public virtual DonemTipDto DonemTip { get; set; }

        public int? MufredatId { get; set; }
        public virtual MufredatDto Mufredat { get; set; }
        public int? FakulteId { get; set; }
        public virtual FakulteDto Fakulte { get; set; }
        public int? BolumId { get; set; }
        public virtual BolumDto Bolum { get; set; }
        public int? ProgramId { get; set; }
        public virtual ProgramDto Program { get; set; }
        public int? DersNedenId { get; set; }
        public virtual DersNedenDto DersNeden { get; set; }
        public int? DersDilId { get; set; }
        public virtual DersDilDto DersDil { get; set; }
        public string KisaAd { get; set; }
        public int? Akts { get; set; }
        public string OptikKod { get; set; }
        public string AdEn { get; set; }
        public int? UygSaat { get; set; }
        public int? LabSaat { get; set; }
        public int? TeoSaat { get; set; }
        public double Kredi { get; set; }
        public bool Durum { get; set; } = true;
        public bool Zorunlu { get; set; } = true;
        public bool AktifDonemdeAcik { get; set; }
        
        public string SecmeliKodu { get; set; }
        public int? Sinif { get; set; }
        public string KancalananDersAd { get; set; }
        public bool IsYillik { get; set; } = false;

        private string kancalananDersKod;
        public string KancalananDersKod {
            get {
                if (Durum)
                {
                    return null;
                }
                else
                  return kancalananDersKod;
            }
            set { kancalananDersKod = value; }
        }
    }
}
