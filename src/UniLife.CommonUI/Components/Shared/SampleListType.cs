using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniLife.CommonUI.Components
{
    internal class SampleListType
    {
        public string UID { get; set; }
        public List<SampleListType> SourceData { get; set; }
        public string Name { get; set; }

        public string IconCss { get; set; }
        public bool Expanded { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SampleType Type { get; set; }

        public List<Sample> Samples { get; set; }
    }
}
