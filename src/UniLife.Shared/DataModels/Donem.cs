using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UniLife.Shared.DataModels
{
    public class Donem : Entity<int>, IAuditable, ISoftDelete
    {
        public int DonemTipId { get; set; }
        public DonemTip DonemTip { get; set; }
        public string DonemTipAd { get; set; }
        public int Yil { get; set; }
        public string Ad { get; set; }
        public string KisaAd { get; set; }
        public string AdEn { get; set; }
        public string KisaAdEn { get; set; }
        public DateTime? BasTarih { get; set; }
        public DateTime? BitTarih { get; set; }
        public bool Durum { get; set; }
        public bool YokSisDurum { get; set; }
        public int YilTip{ get; set; }

        public virtual ICollection<Harc> Harcs { get; set; }

    }
}
