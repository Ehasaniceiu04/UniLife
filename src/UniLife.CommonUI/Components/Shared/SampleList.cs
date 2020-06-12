using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniLife.CommonUI.Components
{
    public class SampleList
    {

        public string Name { get; set; }
        public string Directory { get; set; }
        public string Category { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SampleType Type { get; set; }
        public int Order { get; set; }
        public int UID { get; set; }

        public List<Sample> Samples { get; set; } = new List<Sample>();

        public String ControllerName { get; set; }
    }
}
