using Semerkand.Shared.DataInterfaces;
using System.Collections.Generic;

namespace Semerkand.Shared.DataModels
{
    public class DonemTip : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }
    }
}
