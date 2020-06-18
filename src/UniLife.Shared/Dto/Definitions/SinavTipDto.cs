using System;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class SinavTipDto : EntityDto<int>
    {
        public string Ad { get; set; }
    }
}
