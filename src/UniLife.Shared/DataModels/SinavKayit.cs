using System;
using System.ComponentModel.DataAnnotations;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class SinavKayit : Entity<long>, IAuditable, ISoftDelete
    {
        public int SinavId { get; set; }
        public virtual Sinav Sinav{ get; set; }
        public int OgrenciId { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }

        public double OgrNot { get; set; }
    }
}
