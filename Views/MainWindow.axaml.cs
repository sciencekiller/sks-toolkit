using Avalonia.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using sks_toolkit.Views;
using sks_toolkit;
using System.Runtime.CompilerServices;
using MessageBox;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace sks_toolkit.Views
{
    public partial class MainWindow : Window

    {
        BindingData data = new BindingData();//创建binding类
        Deploy_ENV_Data deploy_env_data= new Deploy_ENV_Data();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        public void Init()//初始化变量
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
            else if(TimeNow >= 8 && TimeNow < 11)
            {
                data.GreetingWord = "上午好,";
                data.GreetingSentence = "忙碌的一天~";
            }
            else if(TimeNow>=11 && TimeNow < 13)
            {
                data.GreetingWord = "中午好,";
                data.GreetingSentence = "可以休息一下了~";
            }
            else if(TimeNow>=13 && TimeNow < 17)
            {
                data.GreetingWord = "下午好,";
                data.GreetingSentence = "精神不好就去喝杯咖啡吧!";
            }
            else if(TimeNow>=17 && TimeNow < 19)
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
            string workDictionary = System.AppDomain.CurrentDomain.BaseDirectory;
            string configPath=workDictionary+"Assets\\\\Config.json";
            JObject Config;
            using(System.IO.StreamReader configFile = System.IO.File.OpenText(configPath))
            {
                using(JsonTextReader jsonReader = new JsonTextReader(configFile))
                {
                    Config =(JObject)JToken.ReadFrom(jsonReader);
                }
            }
            data.Version = Config["version"].ToString();
            data.Channel = Config["channel"].ToString();
            data.Build = Config["build"].ToString();
            //获取最新版本
            JObject LatestRelease = WebService.getJsonFromServer("https://gitee.com/sciencekiller/sks-toolkit/raw/main/Assets/Config.json");
            if(LatestRelease != null)
            {
                data.latestVersion = LatestRelease["version"].ToString();
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
            else if(data.Version != data.latestVersion&&LatestRelease!=null)
            {
                data.latestOrNot = "有新版本可用，请尽快查看";
            }
            List<string> gpp_version_list = new List<string>();
            string latest_gpp_version_list_url = "https://gist.githubusercontent.com/sciencekiller/39c1136117ca2d65fb14dfb022170321/raw/4cda05e096edc5c45de9c585ccec7cfc9c714c09/gpp_download.json";
            JObject latest_gpp_version_list = WebService.getJsonFromServer(latest_gpp_version_list_url);
            foreach (var gpp_version in latest_gpp_version_list)
            {
                gpp_version_list.Add(gpp_version.Key);
            }
            deploy_env_data.Gpp_version_list= gpp_version_list;
            deploy_env_data.Download_Link = latest_gpp_version_list;
            //设置绑定源
            MainTab.DataContext = data;
            DeployEnvTab.DataContext = deploy_env_data;
        }
    }
}