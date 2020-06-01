using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.Dto.Definitions
{
    public class DersAcDto
    {
        public int RefProgramSecilen { get; set; }
        public IEnumerable<int> RefProgramSecenektekiler { get; set; }
        public int AcProgramSecilen { get; set; }
        public IEnumerable<int> AcProgramSecenektekiler { get; set; }

        public IEnumerable<int> AcSinifSecilenler { get; set; }

        public IEnumerable<int> DersAcIds { get; set; }

        public int AcDonemSecilen { get; set; }
    }
}
