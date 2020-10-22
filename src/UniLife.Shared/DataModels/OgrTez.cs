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

        [MaxLength(1000,ErrorMessage ="1000 karakterden fazla olmaz.")]
        public string TezKonu { get; set; }
        [MaxLength(100, ErrorMessage = "100 karakterden fazla olmaz.")]
        public string TezBilgi { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Danisman")]
        public int? DanismanId { get; set; }
        public virtual Akademisyen Danisman { get; set; }
        public bool Basarili { get; set; }
        public DateTime? TezTarih { get; set; }
    }
}
