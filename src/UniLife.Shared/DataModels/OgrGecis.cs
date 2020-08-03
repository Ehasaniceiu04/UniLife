using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class OgrGecis : Entity<int>, IAuditable, ISoftDelete
    {
        
        public int OgrenciId{ get; set; }

        public virtual Ogrenci Ogrenci{ get; set; }
        public string GelUniv { get; set; }
        public string GelBolum { get; set; }
        public string GelBirim { get; set; }
    }
}
