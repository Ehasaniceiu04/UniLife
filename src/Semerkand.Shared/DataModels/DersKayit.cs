using Semerkand.Shared.DataInterfaces;

namespace Semerkand.Shared.DataModels
{
    public class DersKayit : Entity<int>, IAuditable, ISoftDelete
    {
        public int DersAcilanId { get; set; }
        public virtual DersAcilan DersAcilan{ get; set; }

        public int OgrenciId { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }

        public int? DersYerineSecilenId { get; set; } //zorunluysa hem DersAcilanId a hem buraya, seçmeliyse burada seçilen DersAcilanId a yerine seçilen.

        public string DersYerineSecilenAd { get; set; }

    }
}
