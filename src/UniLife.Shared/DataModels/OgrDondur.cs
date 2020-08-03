using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class OgrDondur : Entity<int>, IAuditable, ISoftDelete
    {
        
        public int OgrenciId{ get; set; }

        public virtual Ogrenci Ogrenci{ get; set; }
        //kayit dond.
        public DateTime? DondTarih { get; set; }
        public bool IsDond { get; set; }
    }
}
