using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
                    JObject jObject = JsonConvert.DeserializeObject<JObject>(json);

                    return jObject;
                }
                else
                {
                    throw new Exception("Failed to download JSON from the URL.");
                }
            }
        }
    }
}
