using System.Collections.Generic;

namespace UniLife.Shared.Dto.Definitions
{
    public class DonemTipDto : EntityDto<int>
    {
        public string Ad { get; set; }
        //public virtual ICollection<DersDto> Derss { get; set; }
        public bool isActiveDonemTip { get; set; }
    }
}
