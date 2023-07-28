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
        internal static async Task DownloadFileNew(string url, string path,BackgroundWorker worker)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var file=new FileInfo(path);
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
                            double temp = (double)(readLength) / (double)n * 100;
                            MainWindow.deploy_env_data.Message = "下载C++ " + temp + "%";
                            worker.ReportProgress((int)temp);
                            fileStream.Write(buffer, 0, length);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }
    }
}
