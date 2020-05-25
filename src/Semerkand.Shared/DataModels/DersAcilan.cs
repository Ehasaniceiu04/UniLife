﻿using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataModels
{
    public class DersAcilan : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }
        public string Kod { get; set; }

        //External
        public int DersId { get; set; }
        public virtual Ders Ders { get; set; }

        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        //External

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
        public int Zorunlu { get; set; }
        public int Sinif { get; set; }
    }
}