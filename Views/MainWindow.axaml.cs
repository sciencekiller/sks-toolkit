using Avalonia.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
namespace sks_toolkit.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        public void Init()//��ʼ������
        {
            BindingData data = new BindingData();//����binding��
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
                data.GreetingWord = "�����";
                data.GreetingSentence = "���˾Ϳ������⣬��Ϣһ��~";
            }
            else
            {
                data.GreetingWord = "���Ϻ�";
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
            MainTab.DataContext = data;//���ð�Դ
        }
    }
}