using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace UniLife.CommonUI.Components
{
    public class Sample
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public string Category { get; set; }
        public int Order { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string TitleTag { get; set; }
        public string MetaDescription { get; set; }

        public string MappingSampleName { get; set; }

        public string[] ActionDescription { get; set; }

        public string[] Description { get; set; }
        public List<SourceCollection> SourceFiles { get; set; } = new List<SourceCollection>();
        [JsonConverter(typeof(StringEnumConverter))]
        public SampleType Type { get; set; }
    }

    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum SampleType
    {
        [EnumMember(Value = "none")]
        None,
        [EnumMember(Value = "new")]
        New,
        [EnumMember(Value = "updated")]
        Updated,
        [EnumMember(Value = "preview")]
        Preview
    }
}
