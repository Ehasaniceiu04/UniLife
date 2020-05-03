using System;
using System.Collections.Generic;

namespace Semerkand.CommonUI.Components
{
    internal static class SampleBrowser
    {
        public static List<SampleList> SampleList { get; set; } = new List<SampleList>();
        internal static SampleConfig Config { get; set; } = new SampleConfig();
        internal static string CurrentSampleName { get; set; }
        internal static string CurrentFileName { get; set; }
        internal static List<Sample> CurrentControl = new List<Sample>();
        internal static string CurrentControlName { get; set; }
        internal static string CurrentControlCategory { get; set; }
        internal static string CurrentFilePath { get; set; }
        internal static string CurrentUrl { get; set; }
        internal static string TitleTag { get; set; }
        internal static string MetaDescription { get; set; }
        internal static string[] ActionDescription { get; set; }
        internal static string[] Description { get; set; }
        internal static List<String> SampleUrls = new List<String>();
        //internal static List<SourceCollection> CurrentSourceFiles { get; set; } = new List<SourceCollection>();
        //internal static string CurrentSampleDirectory { get; set; }
        //internal static void UpdateSampleData(string url, string baseUrl)
        //{
        //    string updatedUrl = url.Replace(baseUrl, "").Trim();
        //    if (updatedUrl.IndexOf("?") >= 0)
        //    {
        //        updatedUrl = updatedUrl.Substring(0, updatedUrl.IndexOf("?"));
        //    }
        //    if (updatedUrl != "")
        //    {                
        //        if (updatedUrl.LastIndexOf("/") == updatedUrl.Length - 1)
        //        {
        //            updatedUrl = updatedUrl.Substring(0, updatedUrl.LastIndexOf("/"));
        //        }

        //        string[] splittedUrl = updatedUrl.Split("/");
        //        if (splittedUrl.Length >= 2)
        //        {
        //            string CategoryName = splittedUrl[splittedUrl.Length - 2];
        //            string SampleName = splittedUrl[splittedUrl.Length - 1];
        //            foreach (var Control in SampleBrowser.SampleList)
        //            {
        //                if (Control.ControllerName == CategoryName)
        //                {
        //                    SampleBrowser.CurrentControl = Control.Samples;
        //                    SampleBrowser.CurrentControlName = Control.Name;
        //                    SampleBrowser.CurrentControlCategory = Control.Category;                            
        //                    break;
        //                }
        //            }

        //            foreach (var sample in SampleBrowser.CurrentControl)
        //            {
        //                if (sample.Url.IndexOf(SampleName) >= 0)
        //                {
        //                    SampleBrowser.CurrentSampleName = sample.Name;
        //                    SampleBrowser.TitleTag = sample.TitleTag;
        //                    SampleBrowser.MetaDescription = sample.MetaDescription;
        //                    SampleBrowser.ActionDescription = sample.ActionDescription;
        //                    SampleBrowser.Description = sample.Description;
        //                    SampleBrowser.CurrentSourceFiles = sample.SourceFiles;
        //                    SampleBrowser.CurrentSampleDirectory = sample.Directory;
        //                    SampleBrowser.CurrentFileName = sample.FileName;
        //                    break;
        //                }
        //            }
        //        }
        //    }




        //}
        //internal static void Refresh()
        //{
        //    SampleBrowser.ActionDescription = null;
        //    SampleBrowser.CurrentControl = new List<Sample>(); ;
        //    SampleBrowser.CurrentControlCategory = null;
        //    SampleBrowser.CurrentControlName = null;
        //    SampleBrowser.CurrentSampleName = null;
        //    SampleBrowser.Description = null;
        //    SampleBrowser.TitleTag = null;
        //    SampleBrowser.MetaDescription = null;
        //    SampleBrowser.CurrentSourceFiles = new List<SourceCollection>();
        //    SampleBrowser.CurrentSampleDirectory = null;
        //    SampleBrowser.CurrentFileName = null;

        //}
    }
}
