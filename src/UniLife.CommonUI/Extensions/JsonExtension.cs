using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace UniLife.CommonUI.Extensions
{
    public static class JsonExtension
    {
        public static StringContent AsJson(this object o)
        => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
    }
}
