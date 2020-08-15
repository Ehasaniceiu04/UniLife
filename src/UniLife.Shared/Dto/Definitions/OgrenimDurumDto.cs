namespace UniLife.Shared.Dto.Definitions
{
    public class OgrenimDurumDto : EntityDto<int>
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
        public string OgrenciDurum { get; set; }
        public bool Aktif { get; set; }
        public int YoksisKod { get; set; }
        public int YoksisStatuKod { get; set; }
        public string AdEn { get; set; }
        public string KisaAd { get; set; }
        public string KisaAdEn { get; set; }

    }
}
