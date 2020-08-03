using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrGecisDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }

        public virtual OgrenciDto Ogrenci { get; set; }

        //erasmus,farabi, yatay geçiş
        public string GelUniv { get; set; }
        public string GelBolum { get; set; }
        public string GelBirim { get; set; }

    }
}
