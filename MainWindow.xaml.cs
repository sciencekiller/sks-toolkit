using Downloader;
using HandyControl.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using MessageBox = HandyControl.Controls.MessageBox;

namespace sks_toolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //声明全局变量
        private bool isselectfolder = false;//是否已经提醒更改路径有风险 false没有 true有
        private string cppversion = String.Empty;
        private BackgroundWorker worker;
        private bool installcpp=false;//是否安装C++
        private string workDictionary = System.AppDomain.CurrentDomain.BaseDirectory;//获取工作目录
        //数据绑定
        BindingData data = new BindingData();
        internal static Deploy_ENV_Data deploy_env_data = new Deploy_ENV_Data();

        public MainWindow()
        {
            InitializeComponent();
            Init();
            worker = (BackgroundWorker)FindResource("Worker");//获取BackgroundWorker
        }
        public async void Init()
        {
            data.CurrentUser = System.Environment.UserName;//获取用户名
            int TimeNow = System.DateTime.Now.Hour;//获取时间

            //根据时间指定问候
            if (TimeNow >= 1 && TimeNow < 5)
            {
                data.GreetingWord = "凌晨好,";
                data.GreetingSentence = "怎么起床这么早?";
            }
            else if (TimeNow >= 5 && TimeNow < 8)
            {
                data.GreetingWord = "早上好,";
                data.GreetingSentence = "美好的一天又开始了!";
            }
            else if (TimeNow >= 8 && TimeNow < 11)
            {
                data.GreetingWord = "上午好,";
                data.GreetingSentence = "忙碌的一天~";
            }
            else if (TimeNow >= 11 && TimeNow < 13)
            {
                data.GreetingWord = "中午好,";
                data.GreetingSentence = "可以休息一下了~";
            }
            else if (TimeNow >= 13 && TimeNow < 17)
            {
                data.GreetingWord = "下午好,";
                data.GreetingSentence = "精神不好就去喝杯咖啡吧!";
            }
            else if (TimeNow >= 17 && TimeNow < 19)
            {
                data.GreetingWord = "傍晚好,";
                data.GreetingSentence = "累了就看看窗外，休息一下~";
            }
            else
            {
                data.GreetingWord = "晚上好,";
                data.GreetingSentence = "你在熬夜吗?";
            }

            //读取版本号
            string configPath = workDictionary + "Assets\\\\Config.json";//配置文件目录
            JObject Config;
            using (System.IO.StreamReader configFile = System.IO.File.OpenText(configPath))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(configFile))
                {
                    Config = (JObject)JToken.ReadFrom(jsonReader);
                }
            }
            data.Version = (Config["version"] ?? "1.0.0").ToString();
            data.Channel = (Config["channel"] ?? "beta").ToString();
            data.Build = (Config["build"] ?? "10000").ToString();

            //获取最新版本号
            JObject LatestRelease = await WebService.DownloadJson("https://hub.njuu.cf/sciencekiller/sks-toolkit/raw/main/Assets/Config.json");
            if (LatestRelease != null)
            {
                data.latestVersion = (LatestRelease["version"] ?? "1.0.0").ToString();
            }
            else
            {
                data.latestVersion = "NetWorkConnectError";
                data.latestOrNot = "连接到Gitee失败，请开启代理";
            }
            if (data.Version == data.latestVersion)
            {
                data.latestOrNot = "你正在使用最新的Sciencekill's Toolkit";
            }
            else if (data.Version != data.latestVersion && LatestRelease != null)
            {
                data.latestOrNot = "有新版本可用，请尽快查看";
            }
            //获取C++下载列表
            List<string> gpp_version_list = new List<string>();
            string latest_gpp_version_list_url = "https://hub.njuu.cf/sciencekiller/sks-toolkit/raw/main/Assets/Gpp_Download_List.json";
            JObject latest_gpp_version_list = await WebService.DownloadJson(latest_gpp_version_list_url);
            foreach (var gpp_version in latest_gpp_version_list)
            {
                gpp_version_list.Add(gpp_version.Key);
            }
            foreach (var vs in gpp_version_list)
            {
                Console.WriteLine(vs);
            }
            deploy_env_data.Gpp_version_list = gpp_version_list;
            deploy_env_data.Download_Link = latest_gpp_version_list;
            //设置绑定源
            MainTab.DataContext = data;
            DeployEnvTab.DataContext = deploy_env_data;
        }
        //重载设置进度的BackgroundWorker ReportProgress方法，使其可以更新文字
        public void report(int i, string message)
        {
            deploy_env_data.Message = message;
            deploy_env_data.Persent = i;
            worker.ReportProgress(i);
        }
        public void report(int i)
        {
            deploy_env_data.Persent = i;
            worker.ReportProgress(i);
        }
        public void report(string message)
        {
            deploy_env_data.Message = message;
            worker.ReportProgress(deploy_env_data.Persent);
        }
        //选择目录
        private void SelectInstallFolder(object sender, RoutedEventArgs e)
        {
            if (!isselectfolder)
            {

                MessageBoxResult iscontinue = MessageBox.Ask("更改路径可能引发各种各样奇奇怪怪的错误并且难以解决，我们推荐安装在默认目录，如果你需要更改路径，请确保你有能力解决此行为的后果，是否继续?", "确认");
                if (iscontinue == MessageBoxResult.Cancel)
                {
                    return;
                }
                isselectfolder = true;
            }
            string selectedFolder = deploy_env_data.Install_path;

            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = deploy_env_data.Install_path;
                DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    selectedFolder = dialog.SelectedPath;
                }
            }
            deploy_env_data.Install_path = selectedFolder;
            Show_path.Text = selectedFolder;
        }
        //开始部署，BackgroundWorker异步调用方法
        private void StartDeployClicked(object sender, RoutedEventArgs e)
        {
            stopDeploy.IsEnabled = true;
            Deploy_Progress.Value = 0;
            Deploy_Progress.Style = FindResource("ProgressBarInfo") as Style;
            installcpp = install_gpp.IsChecked.Value;
            cppversion = gpp_versions.SelectedValue.ToString();
            worker.RunWorkerAsync(Deploy_Progress);
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
        //部署工作
        private async void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            //Deploy 
            if (installcpp)
            {
                string gpp_download_url = (deploy_env_data.Download_Link[cppversion])["url"].ToString();
                Trace.WriteLine(gpp_download_url);
                string downloadfolder = workDictionary;
                var downloader = new DownloadService(GetDownloadConfiguration());
                await downloader.DownloadFileTaskAsync(gpp_download_url, downloadfolder + "\\\\cpp.7z");
            }
        }
        //更新进度函数
        private void Worker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Deploy_Progress.Value = e.ProgressPercentage;
            Deploy_Step.Text = deploy_env_data.Message;
        }

        //工作结束函数
        private void Worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            stopDeploy.IsEnabled = false;
            if (e.Cancelled)
            {
                MessageBox.Warning("部署工作已被用户取消", "已取消");
                Deploy_Progress.Style = FindResource("ProgressBarDanger") as Style;
                return;
            }
            MessageBox.Success("部署工作已完成", "已完成");
            Deploy_Progress.Style = FindResource("ProgressBarSuccess") as Style;
        }
        //停止部署
        private void Stop_Deploy(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }
    }
}
