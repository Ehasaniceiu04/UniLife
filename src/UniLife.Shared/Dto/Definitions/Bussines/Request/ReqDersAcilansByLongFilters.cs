using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class ReqDersAcilansByLongFilters
    {
        public int? DonemId { get; set; }
        public int? ProgramId { get; set; }
        public int? Sinif { get; set; }
        public int? Sube { get; set; }
        public string Kod { get; set; }
        public string Ad{ get; set; }
        public string AkademisyenAd { get; set; }
    }
}
