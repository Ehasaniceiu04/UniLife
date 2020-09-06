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
        //public bool OranOrTutar { get; set; } //oran true,tutar false
        //public double Tutar { get; set; }

        public static List<KeyValueDto> BasariTipiDtos { get; } = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "%10", Id = 1 },
            new KeyValueDto() { Ad = "Engelli", Id = 2 },
            new KeyValueDto() { Ad = "Şehit-Gazi Yakını", Id = 3 },
            new KeyValueDto() { Ad = "Araştırma Görevlisi", Id = 4 }
        };
    }
}
