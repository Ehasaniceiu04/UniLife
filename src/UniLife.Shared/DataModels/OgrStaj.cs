using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class OgrStaj : Entity<int>, IAuditable, ISoftDelete
    {
        
        public int OgrenciId{ get; set; }

        public virtual Ogrenci Ogrenci{ get; set; }

        //Staj
        public DateTime? StajTarihBas { get; set; }
        public DateTime? StajTarihSon { get; set; }
        public string StajSirket { get; set; }

    }
}
