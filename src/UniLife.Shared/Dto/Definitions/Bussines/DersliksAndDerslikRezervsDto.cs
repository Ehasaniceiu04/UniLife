using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class DersliksAndDerslikRezervsDto
    {
        public List<DerslikDto> Dersliks { get; set; }
        public List<DerslikRezervDto> DerslikRezervs { get; set; }
    }
}
