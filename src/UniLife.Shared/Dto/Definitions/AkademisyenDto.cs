﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class AkademisyenDto : EntityDto<int>
    {
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUserDto ApplicationUser { get; set; }

        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string OgrtNo { get; set; }

        [MaxLength(11)]
        public string TCKN { get; set; }

        public string Eimg { get; set; }
        public bool Durum { get; set; }
        public DateTime? KayitTarih { get; set; }
        public DateTime? AyrilTarih { get; set; }
        public string Diller { get; set; }
        public string EgitimBilg { get; set; }
        public string Telefon { get; set; }
        public string Telefon2 { get; set; }


        public bool IsAuthenticated { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public virtual List<string> Roles { get; set; }
        public virtual List<KeyValuePair<string, string>> ExposedClaims { get; set; }

        public virtual ICollection<DersAcilanDto> DersAcilans { get; set; }


    }
}
