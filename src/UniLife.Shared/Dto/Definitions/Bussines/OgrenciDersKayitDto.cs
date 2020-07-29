using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenciDersKayitDto
    {
        public int OgrId { get; set; }
        public long OgrNo { get; set; }
        public string OgrAd { get; set; }
        public string OgrSoy { get; set; }
        public string AlTip { get; set; }

        public int HBN { get; set; }
        public double TSkor { get; set; }
        public string HarfN { get; set; }
        public string GecDurum { get; set; }


    }
}
