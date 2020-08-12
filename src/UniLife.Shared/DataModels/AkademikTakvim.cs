using System;
using System.ComponentModel.DataAnnotations;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class AkademikTakvim : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(600)]
        public string Ad { get; set; }
        public int Kod { get; set; }
        public int DonemId { get; set; }
        public virtual Donem Donem { get; set; }
        public int? FakulteId { get; set; }
        public virtual Fakulte Fakulte { get; set; }
        public DateTime? BasTarih { get; set; }
        public DateTime? BitTarih { get; set; }
    }
}
