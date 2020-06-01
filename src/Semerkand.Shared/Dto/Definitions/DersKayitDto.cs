using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.Dto.Definitions
{
    public class DersKayitDto : EntityDto<int>
    {
        public int DersAcilanId { get; set; }
        public DersAcilanDto DersAcilan { get; set; }

        public int OgrenciId { get; set; }
        public OgrenciDto Ogrenci { get; set; }
    }
}
