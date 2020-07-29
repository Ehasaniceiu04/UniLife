using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class SinavOgrNotlarDto
    {
        public long SinavKayitId { get; set; }
        public int SinavId { get; set; }
        public int OgrenciId { get; set; }
        public double OgrNot { get; set; }
        public string OgrenciAd { get; set; }
        public long OgrenciNo { get; set; }
    }
}
