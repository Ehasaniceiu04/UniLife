using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto
{
    public class OData<T>
    {
        [JsonProperty("@odata.context")]
        public string context { get; set; }
        public List<T> Value { get; set; }
    }
}
