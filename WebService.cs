using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        public static async Task DownloadFile(string url, string path)
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
        internal static async Task<long> GetFileSize(string url)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            return response.Content.Headers.ContentLength??-1;
        }
    }
}
