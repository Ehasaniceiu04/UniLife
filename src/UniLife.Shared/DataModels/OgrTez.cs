using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class OgrTez : Entity<int>, IAuditable, ISoftDelete
    {
        
        public int OgrenciId{ get; set; }

        public virtual Ogrenci Ogrenci{ get; set; }

        //Tez
        public string TezKonu { get; set; }
        public DateTime? TezTarih { get; set; }
    }
}
