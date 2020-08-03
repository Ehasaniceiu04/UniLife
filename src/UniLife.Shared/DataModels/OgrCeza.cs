using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class OgrCeza : Entity<int>, IAuditable, ISoftDelete
    {
        
        public int OgrenciId{ get; set; }

        public virtual Ogrenci Ogrenci{ get; set; }
        //Ceza İşlem
        public DateTime? CezaTarih { get; set; }
        public string CezaAd { get; set; }
        public string CezaDesc { get; set; }
    }
}
