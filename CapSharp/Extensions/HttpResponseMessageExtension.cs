using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CapSharp.Extensions
{
    internal static class HttpResponseMessageExtension
    {
        internal static async Task<T> DeserializeJsonAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            // Se almacena en una nueva variable para optimizar la respuesta en memoria

            var responseString = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
