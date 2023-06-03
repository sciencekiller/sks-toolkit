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
        public void Init()//初始化变量
        {
            BindingData data = new BindingData();//创建binding类
            data.CurrentUser = System.Environment.UserName;//获取用户名
            int TimeNow = System.DateTime.Now.Hour;
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
                data.GreetingWord = "傍晚好";
                data.GreetingSentence = "累了就看看窗外，休息一下~";
            }
            else
            {
                data.GreetingWord = "晚上好";
                data.GreetingSentence = "你在熬夜吗?";
            }
            MainTab.DataContext = data;//设置绑定源
        }
    }
}