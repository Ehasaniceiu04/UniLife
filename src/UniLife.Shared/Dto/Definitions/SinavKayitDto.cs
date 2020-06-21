using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class SinavKayitDto : EntityDto<long>
    {
        public int SinavId { get; set; }
        public int OgrId { get; set; }
    }
}
