using Semerkand.Shared.DataInterfaces;

namespace Semerkand.Shared.DataModels
{
    public class DersKayit : Entity<int>, IAuditable, ISoftDelete
    {
        public int DersAcilanId { get; set; }
        public DersAcilan DersAcilan{ get; set; }

        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; }
    }
}
