﻿using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semerkand.Shared.DataModels
{
    public class Ogrenci : Entity<int>, IAuditable, ISoftDelete
    {
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId{ get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [MaxLength(300)]
        public string Ad { get; set; }

        public string OgrNo { get; set; }


        public int FakulteId { get; set; }
        public virtual Fakulte Fakulte{ get; set; }
        public int BolumId { get; set; }
        public virtual Bolum Bolum{ get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program{ get; set; }

        public int KayitNedenId { get; set; }
        public virtual KayitNeden KayitNeden { get; set; }

        public int OgrenimDurumId { get; set; }
        public virtual OgrenimDurum OgrenimDurum{ get; set; }

        public string AskerDurum { get; set; }
        public DateTime KayitTarih { get; set; }
        public DateTime? AyrilTarih { get; set; }
        public string AnaOgrNo { get; set; }



    }
}