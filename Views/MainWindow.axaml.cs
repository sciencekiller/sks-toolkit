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
        Deploy_ENV_Data deploy_env_data = new Deploy_ENV_Data();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        public void RunCmd(string str)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(str + "&exit");

            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            //StreamReader reader = p.StandardOutput;
            //string line=reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    str += line + "  ";
            //    line = reader.ReadLine();
            //}

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
        }
        public void Init()//初始化变量
        {
            //开启FastGithub以便获取Github信息
            bool isfg = true;
            foreach(var fgp in Process.GetProcessesByName("fastgithub"))
            {
                isfg = false;
            }
            string WorkDic = Environment.CurrentDirectory;
            string runfg = "\""+WorkDic + "\\Assets\\FastGithub\\fastgithub.exe\" start";
            string stopfg ="\""+ WorkDic + "\\Assets\\FastGithub\\fastgithub.exe\" stop";
            if (isfg==true)
            {
                RunCmd(runfg);
            }
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
            JObject LatestRelease = WebService.getJsonFromServer("https://api.github.com/repos/sciencekiller/sks-toolkit/releases/latest");
            if(LatestRelease != null)
            {
                data.latestVersion = LatestRelease["name"].ToString();
            }
            else
            {
                data.latestVersion = "NetWorkConnectError";
                data.latestOrNot = "连接到GitHub失败，请开启代理";
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
            deploy_env_data.Gpp_download_links = gpp_version_list;
            //关闭Fastgithub
            if (isfg == true)
            {
                RunCmd(stopfg);
            }
            //设置绑定源
            MainTab.DataContext = data;
            DeployEnvTab.DataContext = deploy_env_data;
        }
    }
}