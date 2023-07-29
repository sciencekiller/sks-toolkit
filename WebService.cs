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
            return response.Content.Headers.ContentLength ?? -1;
        }
        public static bool DownloadFileByAria2(string url, string strFileName)
        {
            var tool = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\aria2-1.34.0-win-64bit-build1\\aria2c.exe";
            var fi = new FileInfo(strFileName);
            var command = " -c -s 10 -x 10  --file-allocation=none --check-certificate=false -d " + fi.DirectoryName + " -o " + fi.Name + " " + url;
            using (var p = new Process())
            {
                RedirectExcuteProcess(p, tool, command, (s, e) => ShowInfo(url, e.Data));
            }
            return File.Exists(strFileName) && new FileInfo(strFileName).Length > 0;
        }
        private static void ShowInfo(string url, string a)
        {
            if (a == null) return;

            const string re1 = ".*?"; // Non-greedy match on filler
            const string re2 = "(\\(.*\\))"; // Round Braces 1

            var r = new Regex(re1 + re2, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var m = r.Match(a);
            if (m.Success)
            {
                var rbraces1 = m.Groups[1].ToString().Replace("(", "").Replace(")", "").Replace("%", "").Replace("s", "0");
                if (rbraces1 == "OK")
                {
                    rbraces1 = "100";
                }

            }
        }
        public static async Task Download(string url, string fn) { var exe = "aria2c"; var dir = Path.GetDirectoryName(fn); var name = Path.GetFileName(fn); void Output(object sender, DataReceivedEventArgs args) { if (string.IsNullOrWhiteSpace(args.Data)) { return; } Console.WriteLine("Aria:{0}", args.Data?.Trim()); } var args = $"-x 8 -s 8 --dir={dir} --out={name} {url}"; var info = new ProcessStartInfo(exe, args) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true, }; if (File.Exists(fn)) { File.Delete(fn); } Console.WriteLine("启动 aria2c： {0}", args); using (var p = new Process { StartInfo = info, EnableRaisingEvents = true }) { if (!p.Start()) { throw new Exception("aria 启动失败"); } p.ErrorDataReceived += Output; p.OutputDataReceived += Output; p.BeginOutputReadLine(); p.BeginErrorReadLine(); await p.WaitForExitAsync(); p.OutputDataReceived -= Output; p.ErrorDataReceived -= Output; } var fi = new FileInfo(fn); if (!fi.Exists || fi.Length == 0) { throw new FileNotFoundException("文件下载失败", fn); } }
        private static void RedirectExcuteProcess(Process p, string exe, string arg, DataReceivedEventHandler output)
        {
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = arg;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += output;
            p.ErrorDataReceived += output;
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
        }
    }
}
