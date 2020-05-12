using System.Collections.Generic;

namespace Semerkand.Shared.Dto.Definitions
{
    public class DonemTipDto : EntityDto<int>
    {
        public string Ad { get; set; }
        public virtual ICollection<DersDto> Derss { get; set; }
    }
}
