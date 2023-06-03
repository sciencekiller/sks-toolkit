using Avalonia.Controls;
using sks_toolkit;
using System;
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
            int TimeNow = System.DateTime.Now.Hour;
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
            MainTab.DataContext = data;//���ð�Դ
        }
    }
}