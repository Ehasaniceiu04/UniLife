using Semerkand.Shared.DataInterfaces;

namespace Semerkand.Shared.DataModels
{
    public class KayitNeden : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public int Kod { get; set; }
    }
}
