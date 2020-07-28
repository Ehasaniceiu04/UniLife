using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class DersKayit : Entity<int>, IAuditable, ISoftDelete
    {
        public int DersAcilanId { get; set; }
        public virtual DersAcilan DersAcilan{ get; set; }

        public int OgrenciId { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }

        public int? DersYerineSecilenId { get; set; } //zorunluysa hem DersAcilanId a hem buraya, seçmeliyse burada seçilen DersAcilanId a yerine seçilen.

        public string DersYerineSecilenAd { get; set; }

        public double Ort { get; set; }
        public string Not { get; set; }
        public int SonucDurum { get; set; } // UniLife.Shared.Dto.DersSonucDurum enumu
        public int Sonuc { get; set; }
    }
}
