using Semerkand.Shared.DataInterfaces;

namespace Semerkand.Shared.DataModels
{
    public class OgrenimDurum : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
    }
}
