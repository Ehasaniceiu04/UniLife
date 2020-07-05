using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciDto : EntityDto<int>
    {
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUserDto ApplicationUser { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "{0} uzunluğu {2} ila {1} arasında olmalıdır.", MinimumLength = 2)]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Boşluk bırakmayınız.")]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        public string Soyad { get; set; }
        public string OgrNo { get; set; }

        [MaxLength(11)]
        [Required]
        public string TCKN { get; set; }

        [Required]
        public int? FakulteId { get; set; }
        public virtual FakulteDto Fakulte { get; set; }
        [Required]
        public int? BolumId { get; set; }
        public virtual BolumDto Bolum { get; set; }
        [Required]
        public int? ProgramId { get; set; }
        public virtual ProgramDto Program { get; set; }
        [Required]
        public int? MufredatId { get; set; }
        public virtual MufredatDto Mufredat { get; set; }

        public string Eimg { get; set; }
        [Required]
        public int? KayitNedenId { get; set; }
        public virtual KayitNedenDto KayitNeden { get; set; }
        [Required]
        public int? OgrenimDurumId { get; set; }
        public virtual OgrenimDurumDto OgrenimDurum { get; set; }
        public bool Durum { get; set; }
        

        public int? DanismanId { get; set; }
        public virtual AkademisyenDto Danisman { get; set; }

        
        public int? DanismanIkiId { get; set; }
        public virtual AkademisyenDto DanismanIki { get; set; }

        public string AskerDurum { get; set; }
        public DateTime? KayitTarih { get; set; }
        public DateTime? AyrilTarih { get; set; }
        public string AnaOgrNo { get; set; }
        public int? Sinif { get; set; }

        public decimal GerekenTopUcret { get; set; }
        public decimal OdenenTopUcret { get; set; }
        public decimal GenelBakiye { get; set; }
        public bool IsMale { get; set; }

        //from UserInfoDTO
        public bool IsAuthenticated { get; set; }

        //[Required]
        //[StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        //[RegularExpression(@"[^\s]+", ErrorMessage = "Spaces are not permitted.")]
        //[Display(Name = "KullanıcıTCKN")]
        //public string UserName { get; set; }
        public int TenantId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
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

    }
}
