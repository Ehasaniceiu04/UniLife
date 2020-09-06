using System;
using System.Security.Principal;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class OgrBursBasari : Entity<int>, IAuditable, ISoftDelete
    {

        public int OgrenciId { get; set; }

        public virtual Ogrenci Ogrenci { get; set; }

        public int DonemId { get; set; }
        public virtual Donem Donem { get; set; }

        public int BasariTipi { get; set; }
        //public bool OranOrTutar { get; set; } //oran true,tutar false
        //public double Tutar { get; set; }

    }
}
