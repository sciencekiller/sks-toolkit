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
        private static DownloadConfiguration GetDownloadConfiguration()
        {
            var cookies = new CookieContainer();
            cookies.Add(new Cookie("download-type", "test") { Domain = "domain.com" });

            return new DownloadConfiguration
            {
                BufferBlockSize = 10240,    // usually, hosts support max to 8000 bytes, default values is 8000
                ChunkCount = 64,             // file parts to download, default value is 1
                MaximumBytesPerSecond = 1024 * 1024 * 10, // download speed limited to 10MB/s, default values is zero or unlimited
                MaxTryAgainOnFailover = 5,  // the maximum number of times to fail
                MaximumMemoryBufferBytes = 1024 * 1024 * 50, // release memory buffer after each 50 MB
                ParallelDownload = true,    // download parts of file as parallel or not. Default value is false
                ParallelCount = 4,          // number of parallel downloads. The default value is the same as the chunk count
                Timeout = 3000,             // timeout (millisecond) per stream block reader, default value is 1000
                RangeDownload = false,      // set true if you want to download just a specific range of bytes of a large file
                RangeLow = 0,               // floor offset of download range of a large file
                RangeHigh = 0,              // ceiling offset of download range of a large file
                ClearPackageOnCompletionWithFailure = true, // Clear package and downloaded data when download completed with failure, default value is false
                MinimumSizeOfChunking = 1024, // minimum size of chunking to download a file in multiple parts, default value is 512                                              
                ReserveStorageSpaceBeforeStartingDownload = true, // Before starting the download, reserve the storage space of the file as file size, default value is false
                RequestConfiguration =
                {
                    // config and customize request headers
                    Accept = "*/*",
                    CookieContainer = cookies,
                    Headers = new WebHeaderCollection(),     // { your custom headers }
                    KeepAlive = true,                        // default value is false
                    ProtocolVersion = HttpVersion.Version11, // default value is HTTP 1.1
                    UseDefaultCredentials = false,
                    // your custom user agent or your_app_name/app_version.
                    UserAgent = $"DownloaderSample/{Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)}"
                    // Proxy = new WebProxy() {
                    //    Address = new Uri("http://YourProxyServer/proxy.pac"),
                    //    UseDefaultCredentials = false,
                    //    Credentials = System.Net.CredentialCache.DefaultNetworkCredentials,
                    //    BypassProxyOnLocal = true
                    // }
                }
            };
        }
        private static DownloadService CreateDownloadService(DownloadConfiguration config)
        {
            var downloadService = new DownloadService(config);

            // Provide `FileName` and `TotalBytesToReceive` at the start of each downloads
            downloadService.DownloadStarted += OnDownloadStarted;

            // Provide any information about chunker downloads, 
            // like progress percentage per chunk, speed, 
            // total received bytes and received bytes array to live streaming.
            downloadService.ChunkDownloadProgressChanged += OnChunkDownloadProgressChanged;

            // Provide any information about download progress, 
            // like progress percentage of sum of chunks, total speed, 
            // average speed, total received bytes and received bytes array 
            // to live streaming.
            downloadService.DownloadProgressChanged += OnDownloadProgressChanged;

            // Download completed event that can include occurred errors or 
            // cancelled or download completed successfully.
            downloadService.DownloadFileCompleted += OnDownloadFileCompleted;

            return downloadService;
        }

        private static async Task<DownloadService> DownloadFile(string url,string path)
        {
            var CurrentDownloadConfiguration = GetDownloadConfiguration();
            var CurrentDownloadService = CreateDownloadService(CurrentDownloadConfiguration);

            if (string.IsNullOrWhiteSpace(downloadItem.FileName))
            {
                await CurrentDownloadService.DownloadFileTaskAsync(downloadItem.Url, new DirectoryInfo(downloadItem.FolderPath)).ConfigureAwait(false);
            }
            else
            {
                await CurrentDownloadService.DownloadFileTaskAsync(downloadItem.Url, downloadItem.FileName).ConfigureAwait(false);
            }

            return CurrentDownloadService;
        }
    }
}
