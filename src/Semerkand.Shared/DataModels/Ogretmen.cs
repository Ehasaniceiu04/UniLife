﻿using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semerkand.Shared.DataModels
{
    public class Ogretmen : Entity<int>, IAuditable, ISoftDelete
    {
        public Guid ApplicationUserId{ get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string OgrtNo { get; set; }

        [MaxLength(11)]
        public string TCKN { get; set; }

        public string Email { get; set; }
        public string Eimg { get; set; }
        public bool Durum { get; set; }
        public DateTime? KayitTarih { get; set; }
        public DateTime? AyrilTarih { get; set; }
    }
}
