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
    }
}
