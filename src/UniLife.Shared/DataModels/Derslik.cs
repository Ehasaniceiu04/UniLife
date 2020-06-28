using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class Derslik : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public int Capacity { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }

        public int BinaId { get; set; }
        public virtual Bina Bina { get; set; }

        public virtual ICollection<DerslikRezerv> DerslikRezervs { get; set; }
    }
}
