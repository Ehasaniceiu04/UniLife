using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class OgrenimTur : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
    }
}
