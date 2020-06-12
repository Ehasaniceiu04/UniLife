using System;
using System.Collections.Generic;

namespace UniLife.CommonUI.Components
{
    internal class SampleData
    {
        public List<SampleList> SampleList { get; set; } = new List<SampleList>();
        //internal SampleConfig Config = SampleBrowser.SampleList
        internal string CurrentSampleName { get; set; }
        internal List<Sample> CurrentControl { get; set; }
        internal string CurrentControlName { get; set; }
        internal string CurrentControlCategory { get; set; }
        internal string CurrentFilePath { get; set; }
        internal string CurrentUrl { get; set; }
        internal string TitleTag { get; set; }
        internal string MetaDescription { get; set; }
        internal string[] ActionDescription { get; set; }
        internal string[] Description { get; set; }
        internal List<String> SampleUrls = new List<String>();
    }
}
