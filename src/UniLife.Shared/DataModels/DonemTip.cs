using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;

namespace UniLife.Shared.DataModels
{
    public class DonemTip : Entity<int>, IAuditable, ISoftDelete
    {
        public string Ad { get; set; }

        public bool isActiveDonemTip { get; set; }
    }
}
