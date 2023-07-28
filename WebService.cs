using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace sks_toolkit
{
    internal class WebService
    {
        public static async Task<JObject> DownloadJson(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject notnull = new JObject();
                    JObject jObject = JsonConvert.DeserializeObject<JObject>(json) ?? notnull;

                    return jObject;
                }
                else
                {
                    throw new Exception("Failed to download JSON from the URL.");
                }
            }
        }
        public static async void DownloadFile(string url, string path)
        {
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            using (var fs = File.Open(path, FileMode.Create))
            {
                using (var ms = response.Content.ReadAsStream())
                {
                    await ms.CopyToAsync(fs);
                }
            }
        }
        private static async Task DownloadFileNew(string url, FileInfo file)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            try
            {
                var n = response.Content.Headers.ContentLength;
                var stream = await response.Content.ReadAsStreamAsync();
                using (var fileStream = file.Create()) {
                    using (stream)
                    {
                        byte[] buffer = new byte[1024];
                        var readLength = 0; 
                        int length;
                        while ((length = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                        {
                            readLength += length;

                            Console.WriteLine("下载进度" + ((double)readLength) / n * 100);
                            fileStream.Write(buffer, 0, length);
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
