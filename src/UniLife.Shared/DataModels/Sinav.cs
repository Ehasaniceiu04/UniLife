using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class Sinav : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }

        public int DersAcilanId { get; set; }
        public virtual DersAcilan DersAcilan{ get; set; }

        public int SinavTipId { get; set; }
        public SinavTip SinavTip{ get; set; }

        public int SinavTurId{ get; set; }
        public SinavTur SinavTur{ get; set; }

        public string SablonAd { get; set; }
        [MaxLength(100)]
        public int EtkiOran { get; set; }

        public bool IsKilit{ get; set; }
        public DateTime? Tarih{ get; set; }
        public bool TarihIlan { get; set; }
        public string KisaAd { get; set; }
        public int? MazeretiSinavId { get; set; } //Bu sadece bazı öğrenciler için gerçekleşiyor.

        public virtual ICollection<SinavKayit> SinavKayits { get; set; }
    }
}
