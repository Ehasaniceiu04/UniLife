using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class DersKayitOgrOrtalamaDto
    {
        public int DersKayitId { get; set; }
        public int OgrenciId { get; set; }
        
        public string OgrenciAdSoyad { get; set; }
        public long OgrenciNo { get; set; }

        //public string DersAcilanKod { get; set; }
        //public string DersAcilanAd { get; set; }
        public int DersAcilanId { get; set; }
        public double OgrOrt { get; set; }
        public string HarfNot { get; set; }
        public bool IsGecti { get; set; }

        public string OgrSinavlarText { get; set; }
    }

}
