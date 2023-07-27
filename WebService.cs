﻿using Newtonsoft.Json;
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
                    JObject notnull=new JObject();
                    JObject jObject = JsonConvert.DeserializeObject<JObject>(json)??notnull;

                    return jObject;
                }
                else
                {
                    throw new Exception("Failed to download JSON from the URL.");
                }
            }
        }
        public static async void DownloadFile(string url,string path)
        {
            var http=new HttpClient();
            var request=new HttpRequestMessage(HttpMethod.Get, url);
            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            using(var fs = File.Open(path, FileMode.Create))
            {
                using(var ms = response.Content.ReadAsStream())
                {
                    await ms.CopyToAsync(fs);
                }
            }
        }
    }
}
