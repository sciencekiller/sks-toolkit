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
        public void Init()
        {
            Data data = new Data();
            data.CurrentUser = System.Environment.UserName;
            data.GreetingWord= "aaa";
            MainTab.DataContext = data;
        }
    }
}