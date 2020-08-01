using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciDigerDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }

        public virtual OgrenciDto Ogrenci { get; set; }

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
