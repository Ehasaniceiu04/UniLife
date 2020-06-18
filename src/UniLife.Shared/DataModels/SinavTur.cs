using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class SinavTur : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
    }
}
