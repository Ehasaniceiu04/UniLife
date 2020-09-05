using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataModels
{
    public class DersAcilanForSinav : DersAcilan
    {
        public int DersKayitCount { get; set; }
        public string AkademisyenAd { get; set; }
    }
}
