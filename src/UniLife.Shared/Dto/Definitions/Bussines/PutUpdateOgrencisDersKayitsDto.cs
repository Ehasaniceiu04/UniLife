using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class PutUpdateOgrencisDersKayitsDto
    {
        public int SelectedDersAcilanId { get; set; }
        public int PointedDersAcilanId { get; set; }
        public IEnumerable<int> OgrenciIds { get; set; }
    }
}
