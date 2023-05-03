using System;

namespace sks_toolkit.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static string Version => "1.0.0";
        public static string Build => "10000";
        public static string Channel => "Developing";
        public static string Current_User=> Environment.UserName;
        public static string a;
        static void init_variable(string[] args)
        {
            a = "aaa";
        }
        public static string b => a;
    }
}