using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Argentex.ClientSite.Service.Http
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ContentAsType<T>(this HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(data) ?
                            default(T) :
                            JsonConvert.DeserializeObject<T>(data);
        }

        public static async Task<string> ContentAsJson(this HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.SerializeObject(data);
        }

        public static async Task<string> ContentAsString(this HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }
    }
}
