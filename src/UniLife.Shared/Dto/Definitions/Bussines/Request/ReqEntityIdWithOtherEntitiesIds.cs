using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class ReqEntityIdWithOtherEntitiesIds
    {
        public int EntityId { get; set; }
        public List<int> OtherEntityIds { get; set; }
        public List<int> Other2EntityIds { get; set; }
    }
}
