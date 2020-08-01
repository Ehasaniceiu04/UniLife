using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.DataModels
{
    public class OgrenciDiger : Entity<int>, IAuditable, ISoftDelete
    {
        
        public int OgrenciId{ get; set; }

        public virtual Ogrenci Ogrenci{ get; set; }

        //erasmus,farabi, yatay geçiş
        public string GelUniv { get; set; }
        public string GelBolum { get; set; }
        public string GelBirim { get; set; }

        //kayit dond.
        public DateTime? DondTarih { get; set; }
        public bool IsDond { get; set; }

        //Ceza İşlem
        public DateTime? CezaTarih { get; set; }
        public string CezaAd { get; set; }
        public string CezaDesc { get; set; }

        //Staj
        public DateTime? StajTarihBas { get; set; }
        public DateTime? StajTarihSon { get; set; }
        public string StajSirket { get; set; }

        //Hazırlık


        //Tez
        public string TezKonu { get; set; }
        public DateTime? TezTarih { get; set; }
    }
}
