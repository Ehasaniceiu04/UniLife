using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrTezDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }

        public virtual OgrenciDto Ogrenci { get; set; }

        //Tez
        public string TezKonu { get; set; }
        public DateTime? TezTarih { get; set; }
    }
}
