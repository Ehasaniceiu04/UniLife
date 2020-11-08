using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrSinavSonucs
    {
        public long SinavKayitId { get; set; }
        public int OgrenciId { get; set; }
        public string SinavTipAd { get; set; }
        public int SinavTipId { get; set; }
        public int SinavTurId { get; set; }
        public long? MazeretiSinavKayitId { get; set; }
        public double EtkiOran { get; set; }
        public double Not { get; set; }
        public int Katilim { get; set; }
        public int SinavId { get; set; }
        public int DersAcilanId { get; set; }
    }
}
