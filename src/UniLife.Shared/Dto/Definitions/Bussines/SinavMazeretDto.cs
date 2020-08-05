using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class SinavMazeretDto : EntityDto<int>
    {
        public int DersAcilanId { get; set; }
        public string SinavAd { get; set; }
        public double OgrNot { get; set; }
        public double SEtkiOran { get; set; }

    }
}
