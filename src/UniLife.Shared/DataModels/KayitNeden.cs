using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class KayitNeden : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
        public int YoksisKod { get; set; }
        public int YoksisStatusKod { get; set; }
        public string KisaAd { get; set; }
        public string AdEn { get; set; }
        public string KisaAdEn { get; set; }
        public bool YuzdeOn { get; set; }
        public bool DersKayitKuralDisi { get; set; }
        public bool OzelOgr { get; set; }
        public bool HarcTahakkukEtmez { get; set; }
    }
}
