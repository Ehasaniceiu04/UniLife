using System.Collections.Generic;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class Kampus : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Adres { get; set; }

        public virtual ICollection<Bina> Binas { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}
