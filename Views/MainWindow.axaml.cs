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
            }
            else if (TimeNow >= 5 && TimeNow < 8)
            {
                data.GreetingWord = "早上好,";
            }
            else if(TimeNow >= 8 && TimeNow < 11)
            {
                data.GreetingWord = "上午好,";
            }
            else if(TimeNow>=11 && TimeNow < 13)
            {
                data.GreetingWord = "中午好,";
            }
            else if(TimeNow>=13 && TimeNow < 17)
            {
                data.GreetingWord = "下午好,";
            }
            else if(TimeNow>=17 && TimeNow < 19)
            {
                data.GreetingWord = "傍晚好";
            }
            else
            {
                data.GreetingWord = "晚上好";
            }
            MainTab.DataContext = data;//设置绑定源
        }
    }
}