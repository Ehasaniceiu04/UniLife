using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.Dto.Definitions
{
    public class DersAcDto
    {
        public string[] MufredatSecilen { get; set; }
        public IEnumerable<int> MufredatSecenektekiler { get; set; }
        public int DonemSecilen { get; set; }
        public string[] SinifSecilen{ get; set; }
        public bool IsIntibak { get; set; }
        public bool IsActive { get; set; }
        public string DersKod { get; set; }
        public string DersAd { get; set; }
    }
}
