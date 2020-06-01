using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.Dto.Definitions.Bussines
{
    public class DersKayitScrDto
    {
        public int AdSoyad { get; set; }
        public DateTime KayitTarih { get; set; }
        public string BagliMufredat { get; set; }
        public decimal GerekenTopUcret { get; set; }
        public decimal OdenenTopUcret { get; set; }
        public decimal GenelBakiye { get; set; }
        public int ProgramSuresi { get; set; }
    }
}
