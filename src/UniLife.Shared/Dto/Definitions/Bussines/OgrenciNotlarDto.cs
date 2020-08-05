using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciNotlarDto
    {
        public long SinavKayitId { get; set; }
        public int SinavId { get; set; }
        public string SinavTip { get; set; }
        public string DersKisaAd { get; set; }
        public string DersAd { get; set; }
        public int DersId { get; set; }
        public int OgrenciId { get; set; }
        public double OgrNot { get; set; }

        public string Donem { get; set; }
        public int Sinif { get; set; }

    }
}
