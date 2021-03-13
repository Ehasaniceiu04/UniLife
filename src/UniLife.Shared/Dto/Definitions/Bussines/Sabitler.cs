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

        public static List<KeyValueDto> DiplomaTipDtos { get; } = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "birinci tip", Id = 1 },
            new KeyValueDto() { Ad = "ikinci tip", Id = 2 },
            new KeyValueDto() { Ad = "üç tip", Id = 3 },
            new KeyValueDto() { Ad = "döt tip", Id = 4 },
        };

        public static List<KeyValueDto> BasariTipiDtos { get; } = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "%10", Id = 1 },
            new KeyValueDto() { Ad = "Engelli", Id = 2 },
            new KeyValueDto() { Ad = "Şehit-Gazi Yakını", Id = 3 },
            new KeyValueDto() { Ad = "Araştırma Görevlisi", Id = 4 }
        };

        public static List<KeyValueDto> SinavKatilim { get; } = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "Katıldı", Id = 1 },
            new KeyValueDto() { Ad = "Katılmadı", Id = 2 },
            new KeyValueDto() { Ad = "Devamsız", Id = 3 }
        };

        public static List<KeyValueDto> CapYandal { get; } = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "Normal", Id = 1 },
            new KeyValueDto() { Ad = "Cap", Id = 2 },
            new KeyValueDto() { Ad = "Yandal", Id = 3 }
        };

        public static List<HarfGecBas> HarfGecBasList { get; } = new List<HarfGecBas>
        {
            new HarfGecBas() { Carpan= 4, Harf="AA",Basari="Başarılı",IsGecti=true},
            new HarfGecBas() { Carpan= 3.5, Harf="BA",Basari="Başarılı",IsGecti=true},
            new HarfGecBas() { Carpan= 3, Harf="BB",Basari="Başarılı",IsGecti=true},
            new HarfGecBas() { Carpan= 2.5, Harf="CB",Basari="Başarılı",IsGecti=true},
            new HarfGecBas() { Carpan= 2, Harf="CC",Basari="Başarılı",IsGecti=true},
            new HarfGecBas() { Carpan= 1.5, Harf="DC",Basari="Başarılı",IsGecti=true},
            new HarfGecBas() { Carpan= 1, Harf="DD",Basari="",IsGecti=null},
            new HarfGecBas() { Carpan= 0, Harf="FF",Basari="Başarılı",IsGecti=false},
            new HarfGecBas() { Carpan= 0, Harf="DZ",Basari="Başarılı",IsGecti=false},
            new HarfGecBas() { Carpan= 0, Harf="MU",Basari="Başarılı",IsGecti=true},
            new HarfGecBas() { Carpan= 0, Harf="BL",Basari="Başarılı",IsGecti=true},
            new HarfGecBas() { Carpan= 0, Harf="BZ",Basari="Başarılı",IsGecti=false}

        };

        public static List<KeyValueDto> DonemTipDtos { get; } = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "Güz Dönemi", Id = 1 },
            new KeyValueDto() { Ad = "Bahar Dönemi", Id = 2 },
            new KeyValueDto() { Ad = "Yaz Dönemi", Id = 3 }
        };

    }
}
