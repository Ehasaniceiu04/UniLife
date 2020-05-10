using Semerkand.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataModels
{
    public class OgrenimDurum : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
    }
}
