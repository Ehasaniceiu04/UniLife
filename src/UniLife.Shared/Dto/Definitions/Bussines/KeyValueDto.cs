using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class KeyValueDto : EntityDto<int>
    {
        public string Ad { get; set; }

        public int IntValue { get; set; }
    }
}
