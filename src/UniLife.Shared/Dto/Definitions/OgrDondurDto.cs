using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrDondurDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }

        public virtual OgrenciDto Ogrenci { get; set; }

        //kayit dond.
        public DateTime? DondTarih { get; set; }
        public bool IsDond { get; set; }

    }
}
