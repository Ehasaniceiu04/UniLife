using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class Sabitler
    {
        public static List<SinifDto> SinifDtos { get; } = new List<SinifDto>
        {
            new SinifDto() { Ad = "Haz.", Id = 0 },
            new SinifDto() { Ad = "1", Id = 1 },
            new SinifDto() { Ad = "2", Id = 2 },
            new SinifDto() { Ad = "3", Id = 3 },
            new SinifDto() { Ad = "4", Id = 4 },
            new SinifDto() { Ad = "5", Id = 5 },
            new SinifDto() { Ad = "6", Id = 6 },
            new SinifDto() { Ad = "7", Id = 7 },
            new SinifDto() { Ad = "8", Id = 8 },
            new SinifDto() { Ad = "9", Id = 9 },
        };

        public static List<KeyValueDto> BasariTipiDtos { get; } = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "%10", Id = 1 },
            new KeyValueDto() { Ad = "Engelli", Id = 2 },
            new KeyValueDto() { Ad = "Şehit-Gazi Yakını", Id = 3 },
            new KeyValueDto() { Ad = "Araştırma Görevlisi", Id = 4 }
        };
    }
}
