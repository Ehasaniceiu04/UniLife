using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions.Bussines
{
    public class MazunOnayDto
    {
        public IEnumerable<int> SelectedOgrIds { get; set; }
        public int SelectedDonemId { get; set; }
    }
}
