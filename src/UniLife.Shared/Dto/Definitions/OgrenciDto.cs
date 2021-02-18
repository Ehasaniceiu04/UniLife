using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciDto : EntityDto<int>
    {
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUserDto ApplicationUser { get; set; }

        [Required(ErrorMessage = "Ad bilgisi zorunludur")]
        //[StringLength(64, ErrorMessage = "{0} uzunluğu {2} ila {1} arasında olmalıdır.", MinimumLength = 2)]
        //[RegularExpression(@"[^\s]+", ErrorMessage = "Boşluk bırakmayınız.")]
        //[Display(Name = "Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad bilgisi zorunludur")]
        public string Soyad { get; set; }
        [Required(ErrorMessage = "Öğrenci No bilgisi zorunludur")]
        public long OgrNo { get; set; }

        [StringLength(11, ErrorMessage = "{0} uzunluğu 11 rakamdan oluşmalıdır.", MinimumLength = 11)]
        //[MaxLength(11)]
        //[MinLength(11)]
        [Required(ErrorMessage = "TCKN bilgisi zorunludur")]
        [RegularExpression("([0-9]+)",ErrorMessage ="TCKN Sadece rakamlardan oluşmalıdır.")]
        public string TCKN { get; set; }

        [Required(ErrorMessage = "Fakulte bilgisi zorunludur")]
        public int? FakulteId { get; set; }
        public virtual FakulteDto Fakulte { get; set; }
        [Required(ErrorMessage = "Bolum bilgisi zorunludur")]
        public int? BolumId { get; set; }
        public virtual BolumDto Bolum { get; set; }
        [Required(ErrorMessage = "Program bilgisi zorunludur")]
        public int? ProgramId { get; set; }
        public virtual ProgramDto Program { get; set; }
        [Required(ErrorMessage = "Mufredat bilgisi zorunludur")]
        public int? MufredatId { get; set; }
        public virtual MufredatDto Mufredat { get; set; }

        public string Eimg { get; set; }
        [Required(ErrorMessage = "Kayıt Neden bilgisi zorunludur")]
        public int? KayitNedenId { get; set; }
        public virtual KayitNedenDto KayitNeden { get; set; }
        [Required(ErrorMessage = "Öğrenim Durum bilgisi zorunludur")]
        public int? OgrenimDurumId { get; set; }
        public virtual OgrenimDurumDto OgrenimDurum { get; set; }
        //public int? OgrenimTurId { get; set; }
        //public virtual OgrenimTurDto OgrenimTur { get; set; }
        public bool Durum { get; set; } = true;

        //[Required(ErrorMessage = "Danişman bilgisi zorunludur")]
        public int? DanismanId { get; set; }
        public virtual AkademisyenDto Danisman { get; set; }

        public string DnmSnfGecBilgi { get; set; }

        public string AskerDurum { get; set; }
        [Required(ErrorMessage = "Kayıt Tarih bilgisi zorunludur")]
        public DateTime? KayitTarih { get; set; }
        public DateTime? AyrilTarih { get; set; }
        [Required(ErrorMessage = "Sınıf bilgisi zorunludur")]
        public int? Sinif { get; set; }
        public decimal GerekenTopUcret { get; set; }
        public decimal OdenenTopUcret { get; set; }
        public decimal GenelBakiye { get; set; }
        public bool IsMale { get; set; }
        public string Adres { get; set; }
        public string BilgNotu { get; set; }
        public bool MultiUni { get; set; }
        public string IlaveDonem { get; set; }
        public int? CapYan { get; set; } = 1;

        //from UserInfoDTO
        public bool IsAuthenticated { get; set; }

        public bool DersKayitOnayli { get; set; }

        //[Required]
        //[StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        //[RegularExpression(@"[^\s]+", ErrorMessage = "Spaces are not permitted.")]
        //[Display(Name = "KullanıcıTCKN")]
        //public string UserName { get; set; }
        public int TenantId { get; set; }

        //[Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]        
        public string Email { get; set; }
        public string EmailOutValid {
            get { return Email; }
            //set { Email = value; }
        }
        public virtual List<string> Roles { get; set; }
        public virtual List<KeyValuePair<string, string>> ExposedClaims { get; set; }
        public bool DisableTenantFilter { get; set; }
        //Bussines needs
        public string FakulteAdi { get; set; }
        public string MufredatAdi { get; set; }
        public string ProgramAdi { get; set; }
        public string BolumAdi { get; set; }
        public long SinavKayitId { get; set; }
        public int Sube { get; set; }

        public decimal AgNo1 { get; set; }
        public decimal AgNo2 { get; set; }
        public int TopKredi1 { get; set; }
        public int TopKredi2 { get; set; }
        public int TopAkts1 { get; set; }
        public int TopAkts2 { get; set; }
        public int HasStaj { get; set; }
        public int HasHazirlik { get; set; }
        public int SDersler1 { get; set; }
        public int SDersler2 { get; set; }
        public int ZDersler1 { get; set; }
        public int ZDersler2 { get; set; }
        public int BasarisizDersler { get; set; }
        [MaxLength(11)]
        public string SonDonem { get; set; }

    }
}
