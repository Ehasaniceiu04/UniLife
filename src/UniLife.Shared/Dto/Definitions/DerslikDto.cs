using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class DerslikDto : EntityDto<int>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Ad { get; set; }
        public int Capacity { get; set; }
        public string Color { get; set; } = String.Format("#{0:X6}", new Random().Next(0x1000000));
        public string Type { get; set; }

        public int BinaId { get; set; }
        public virtual BinaDto Bina { get; set; }

        public virtual ICollection<DerslikRezervDto> DerslikRezervs { get; set; }
    }
}
