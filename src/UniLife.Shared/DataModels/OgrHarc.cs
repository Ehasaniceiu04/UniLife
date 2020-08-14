using System;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class OgrHarc : Entity<int>, IAuditable, ISoftDelete
    {

        public int OgrenciId { get; set; }

        public virtual Ogrenci Ogrenci { get; set; }

        public int DonemId { get; set; }
        public virtual Donem Donem { get; set; }

        public string Tipi { get; set; }
        public string Turu { get; set; }
        public double Tutar { get; set; }
        public double IadeTutar { get; set; }
        public DateTime? TahakkukTarih { get; set; }

    }
}
