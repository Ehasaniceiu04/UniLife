using Syncfusion.Blazor;
using System.Resources;

namespace UniLife.Server.Helpers
{
    public class SyncLocalizer : ISyncfusionStringLocalizer
    {
        // To get the locale key from mapped resources file
        public string Get(string key)
        {
            return this.ResourceManager.GetString(key);
        }

        public string GetText(string key)
        {
            throw new System.NotImplementedException();
        }

        // To access the resource file and get the exact value for locale key

        public ResourceManager ResourceManager
        {
            get
            {
                return UniLife.Server.Resources.SfResources.ResourceManager;
            }
        }
    }
}
