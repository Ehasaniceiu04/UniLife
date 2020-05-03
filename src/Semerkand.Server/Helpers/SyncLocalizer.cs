using Syncfusion.Blazor;

namespace Semerkand.Server.Helpers
{
    public class SyncLocalizer : ISyncfusionStringLocalizer
    {
        // To get the locale key from mapped resources file
        public string Get(string key)
        {
            return this.Manager.GetString(key);
        }

        // To access the resource file and get the exact value for locale key

        public System.Resources.ResourceManager Manager
        {
            get
            {
                return Semerkand.Server.Resources.SfResources.ResourceManager;
            }
        }
    }
}
