using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CapSharp.Extensions;

namespace CapSharp.Handlers
{
    public class HttpClientHandler
    {
        private readonly HttpClient _httpClient;
        private Uri apiBase;

        public HttpClientHandler(Uri ApiBase, HttpClient httpClient = null)
        {
            apiBase = ApiBase;
            _httpClient = httpClient ?? new HttpClient();
        }

        public void SetNewUri(Uri ApiBase)
        {
            apiBase = ApiBase;
        }

        public async Task<T> GetResponseJsonAsync<T>(
            HttpMethod Method, Dictionary<string, string> GetQueries = null, Dictionary<string, string> Parameters = null)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = Method
            };

            if (GetQueries != null)
            {
                var HttpUtilityQuery = HttpUtility.ParseQueryString(String.Empty);
                
                GetQueries.ToList().ForEach(x => HttpUtilityQuery.Add(x.Key, x.Value));

                httpRequestMessage.RequestUri = new Uri(apiBase + "?" + HttpUtilityQuery.ToString());
            }
            else { httpRequestMessage.RequestUri = apiBase; }

            if (Parameters != null) httpRequestMessage.Content = new FormUrlEncodedContent(Parameters);

            using var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return default;
            }

            //if (!WaitingForJson)
            //{
            //  https://stackoverflow.com/a/21807786 <3

            //  return (T)Convert
            //    .ChangeType(await httpResponseMessage.Content.ReadAsStringAsync()
            //    .ConfigureAwait(false), typeof(T));
            //}

            /*
             * This is where I remember that the parameter 'json=1' existed 
             * to be able to convert the response to Json and that the
             * above was just wasting time lmao
             */

            return await httpResponseMessage.DeserializeJsonAsync<T>().ConfigureAwait(false);
        }
    }
}
