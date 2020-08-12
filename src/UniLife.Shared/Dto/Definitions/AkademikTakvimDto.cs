using System;
using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Definitions
{
    public class AkademikTakvimDto : EntityDto<int>
    {
        [Required]
        [MaxLength(600)]
        public string Ad { get; set; }
        public int Kod { get; set; }
        public int? DonemId { get; set; }
        public virtual DonemDto Donem { get; set; }
        public int? FakulteId { get; set; }
        public virtual FakulteDto Fakulte { get; set; }
        public DateTime? BasTarih { get; set; }
        public DateTime? BitTarih { get; set; }
    }
}