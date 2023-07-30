using Downloader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
        
    }
}
