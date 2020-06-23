using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{

    public class SubeDersAcilanOgrenciCreateDto
    {
        public List<int> Subes { get; set; }
        public int DersAcilanId { get; set; }
        public List<OgrenciSubeDto> OgrenciIdsWithSubes { get; set; }
    }
    public class OgrenciSubeDto
    {
        public int Sube { get; set; }
        public int OgrId { get; set; }
    }

    

    
}
