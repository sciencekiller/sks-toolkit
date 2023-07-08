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
        BindingData data = new BindingData();//����binding��
        Deploy_ENV_Data deploy_env_data= new Deploy_ENV_Data();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        public void Init()//��ʼ������
        {
            data.CurrentUser = System.Environment.UserName;//��ȡ�û���
            int TimeNow = System.DateTime.Now.Hour;//��ȡʱ��
            //����ʱ��ָ���ʺ�
            if (TimeNow >= 1 && TimeNow < 5)
            {
                data.GreetingWord = "�賿��,";
                data.GreetingSentence = "��ô����ô��?";
            }
            else if (TimeNow >= 5 && TimeNow < 8)
            {
                data.GreetingWord = "���Ϻ�,";
                data.GreetingSentence = "���õ�һ���ֿ�ʼ��!";
            }
            else if(TimeNow >= 8 && TimeNow < 11)
            {
                data.GreetingWord = "�����,";
                data.GreetingSentence = "æµ��һ��~";
            }
            else if(TimeNow>=11 && TimeNow < 13)
            {
                data.GreetingWord = "�����,";
                data.GreetingSentence = "������Ϣһ����~";
            }
            else if(TimeNow>=13 && TimeNow < 17)
            {
                data.GreetingWord = "�����,";
                data.GreetingSentence = "���񲻺þ�ȥ�ȱ����Ȱ�!";
            }
            else if(TimeNow>=17 && TimeNow < 19)
            {
                data.GreetingWord = "�����,";
                data.GreetingSentence = "���˾Ϳ������⣬��Ϣһ��~";
            }
            else
            {
                data.GreetingWord = "���Ϻ�,";
                data.GreetingSentence = "���ڰ�ҹ��?";
            }
            //��ȡ�汾��
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
            //��ȡ���°汾
            JObject LatestRelease = WebService.getJsonFromServer("https://gitee.com/sciencekiller/sks-toolkit/raw/main/Assets/Config.json");
            if(LatestRelease != null)
            {
                data.latestVersion = LatestRelease["version"].ToString();
            }
            else
            {
                data.latestVersion = "NetWorkConnectError";
                data.latestOrNot = "���ӵ�Giteeʧ�ܣ��뿪������";
            }
            if (data.Version == data.latestVersion)
            {
                data.latestOrNot = "������ʹ�����µ�Sciencekill's Toolkit";
            }
            else if(data.Version != data.latestVersion&&LatestRelease!=null)
            {
                data.latestOrNot = "���°汾���ã��뾡��鿴";
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
            //���ð�Դ
            MainTab.DataContext = data;
            DeployEnvTab.DataContext = deploy_env_data;
        }
    }
}