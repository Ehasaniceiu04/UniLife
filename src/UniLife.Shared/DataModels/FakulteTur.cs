using UniLife.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataModels
{
    public class FakulteTur : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
        public string Tur { get; set; }
        public string TurEn { get; set; }
        public string YokasId { get; set; }

    }
}
