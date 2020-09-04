using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class HedefKaynakDto
    {
        public List<int> KaynakIdList{ get; set; }
        public List<int> HedefIdList { get; set; }

        public int KaynakId { get; set; }
        public int HedefId { get; set; }
    }
}
