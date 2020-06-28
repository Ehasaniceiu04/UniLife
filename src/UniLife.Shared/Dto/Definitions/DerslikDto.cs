using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class DerslikDto : EntityDto<int>
    {
        public string Ad { get; set; }
        public int Capacity { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }

        public int BinaId { get; set; }
        public virtual BinaDto Bina { get; set; }

        public virtual ICollection<DerslikRezervDto> DerslikRezervs { get; set; }
    }
}
