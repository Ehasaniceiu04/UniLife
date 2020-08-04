using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrCezaDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }

        
        //public virtual OgrenciDto Ogrenci { get; set; }

        //Ceza İşlem
        public DateTime? CezaTarih { get; set; }
        public string CezaAd { get; set; }
        public string CezaDesc { get; set; }

    }
}
