using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class ResDersAcilansByLongFilters
    {
        public int DersAcilanId { get; set; }
        public int Sube { get; set; }
        public string DersKod { get; set; }
        public string DersAd { get; set; }
        public int Teo { get; set; }
        public int Uyg { get; set; }
        public string TU
        {
            get { return Teo.ToString() + "+" + Uyg.ToString(); }
        }
        public double Kredi { get; set; }
        public int Akts { get; set; }
        public int Sinif { get; set; }
        public int Kont { get; set; }
        public int Kayit { get; set; }
        public string Kon
        {
            get { return Kayit.ToString() + "/" + Kont.ToString(); }
        }
        public bool Zorunlu { get; set; }
        public string ProgramAd { get; set; }
        public string AkademisyenAd { get; set; }
    }
}
