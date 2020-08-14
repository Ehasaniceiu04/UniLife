using System;

namespace UniLife.Shared.Dto.Definitions
{
    public class OgrHarcDto : EntityDto<int>
    {
        public int OgrenciId { get; set; }

        public virtual OgrenciDto Ogrenci { get; set; }
        public int DonemId { get; set; }
        public virtual DonemDto Donem { get; set; }

        public string Tipi { get; set; }
        public string Turu { get; set; }
        public double Tutar { get; set; }
        public double IadeTutar { get; set; }
        public DateTime? TahakkukTarih { get; set; }
    }
}
