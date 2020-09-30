using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.DataModels
{
    public class DersKanca : Entity<int>, IAuditable, ISoftDelete
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AktifMufDers")]
        public int AktifMufredatDersId { get; set; }
        public virtual Ders AktifMufredatDers { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PasifMufredatDers")]
        public int PasifMufredatDersId { get; set; }
        public virtual Ders PasifMufredatDers { get; set; }
        public string PasifMufredatDersKod { get; set; }
        public string PasifMufredatDersAd { get; set; }
        public double PasifMufredatKredi { get; set; }
        public int PasifMufredatAkts { get; set; }
        public string AktifMufredatDersKod { get; set; }
        public string AktifMufredatDersAd { get; set; }
        public double AktifMufredatKredi { get; set; }
        public int AktifMufredatAkts { get; set; }
    }
}
