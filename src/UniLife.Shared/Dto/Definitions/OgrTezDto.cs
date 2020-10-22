using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrTezDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }

        public virtual OgrenciDto Ogrenci { get; set; }

        [MaxLength(1000, ErrorMessage = "1000 karakterden fazla olmaz.")]
        public string TezKonu { get; set; }
        [MaxLength(100, ErrorMessage = "100 karakterden fazla olmaz.")]
        public string TezBilgi { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Danisman")]
        public int? DanismanId { get; set; }
        public virtual AkademisyenDto Danisman { get; set; }
        public bool Basarili { get; set; }
        public DateTime? TezTarih { get; set; }
    }
}
