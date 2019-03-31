using System;
using System.Collections.Generic;
using System.Text;

namespace Standard.Infrastructure.Utils
{
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    namespace Standard.Infrastructure.Utils
    {
        public static class RestApiHelper
        {

            public static async Task<MemoryStream> DownloadFileAsync(string url)
            {
                if (!CrossConnectivity.Current.IsConnected)
                    throw new Exception("The conection is unestable, please try later");

                var stream = new MemoryStream();
                using (var httpClient = GetHttpClient())
                {
                    var downloadStream = await httpClient.GetStreamAsync(new Uri(url));
                    if (downloadStream != null)
                    {
                        await downloadStream.CopyToAsync(stream);
                    }
                }

                return stream;
            }

            public static async Task<string> GetStringAsync(string url)
            {
                if (!CrossConnectivity.Current.IsConnected)
                    throw new Exception("The conection is unestable, please try later");

                using (var httpClient = GetHttpClient())
                {
                    var result = await httpClient.GetAsync(url).ConfigureAwait(false);
                    var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return resultContent;
                }
            }

            public static async Task<List<Dictionary<string, object>>> GetDictionaryAsync(string url)
            {
                if (!CrossConnectivity.Current.IsConnected)
                    throw new Exception("The conection is unestable, please try later");

                var resultContent = await GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(resultContent);
            }

            private static HttpClient GetHttpClient()
            {
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, 5, 0);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }


            public static async Task<List<Dictionary<string, object>>> PostStringAsync(string url, string json, bool comprimirJson, bool retornoEstaComprimido)
            {
                using (var client = GetHttpClient())
                {
                    //string jsonCompactado = comprimirJson ? StringHelper.CompressString(json) : json;
                    HttpContent content = new StreamContent(StringHelper.GenerateStreamFromString(json));

                    var result = await client.PostAsync(url, content).ConfigureAwait(false);
                    var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return retornoEstaComprimido ? JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(StringHelper.DecompressString(resultContent))
                                         : JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(resultContent);
                }
            }

        }
    }

}
