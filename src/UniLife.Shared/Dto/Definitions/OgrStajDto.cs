using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrStajDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }

        public virtual OgrenciDto Ogrenci { get; set; }

        //Staj
        public DateTime? StajTarihBas { get; set; }
        public DateTime? StajTarihSon { get; set; }
        public string StajSirket { get; set; }

    }
}
