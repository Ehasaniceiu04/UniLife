using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrBursBasariDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }
        public virtual OgrenciDto Ogrenci { get; set; }

        public int DonemId { get; set; }
        public virtual DonemDto Donem { get; set; }

        public int BasariTipi { get; set; }
        public bool Durum { get; set; }
        public bool IsOran { get; set; } //oran true,tutar false
        public double Tutar { get; set; }

    }
}
