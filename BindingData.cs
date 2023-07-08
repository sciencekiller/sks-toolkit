using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using sks_toolkit;
using ReactiveUI;
using sks_toolkit.Views;
using sks_toolkit;
using Avalonia.Controls;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Avalonia.Controls.Chrome;

namespace sks_toolkit
{
    internal class BindingData:MainWindow
    {
        private string currentuser = "ERROR?";
        public string CurrentUser
        {
            set { currentuser = value; }
            get { return currentuser; }
        }
        private string greetingword = "ERROR?";
        public string GreetingWord
        {
            set { greetingword = value; }
            get { return greetingword; }
        }
        private string greetingsentence = "ERROR?";
        public string GreetingSentence
        {
            set { greetingsentence = value; }
            get { return greetingsentence; }
        }
        private string version = "ERROR?";
        public string Version
        {
            set { version = value; }
            get { return version; }
        }
        private string channel = "ERROR?";
        public string Channel
        {
            set { channel = value; }
            get { return channel; }
        }
        private string build = "ERROR?";
        public string Build
        {
            set { build = value; }
            get { return build; }
        }
        private string latestversion = "ERROR?";
        public string latestVersion
        {
            set { latestversion = value; }
            get { return latestversion; }
        }
        private string latestornot = "ERROR?";
        public string latestOrNot
        {
            set { latestornot = value; }
            get { return latestornot; }
        }
    }
    internal class Deploy_ENV_Data
    {
        private JObject download_link = new JObject();
        public JObject Download_Link
        {
            set { download_link = value; }
            get { return download_link; }
        }
        private List<string> gpp_version_list = new List<string>();
        public List<string> Gpp_version_list
        {
            get { return gpp_version_list; }
            set { gpp_version_list = value; }
        }
        private string install_gpp = "true";
        public string Install_gpp
        {
            set { install_gpp = value; }
            get { return install_gpp; }
        }
        private string install_gpp_version = "8.1.0-seh";
        public string Install_gpp_version
        {
            set { install_gpp_version = value; }
            get { return install_gpp_version; }
        }
        private string install_path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        public string Install_path
        {
            set { 
                install_path = value;
            }
            get { return install_path;}
        }
        public void StartDeployClicked()
        {

        }
        public async void SelectInstallFolder()
        {
            OpenFolderDialog installpath = new OpenFolderDialog();
            installpath.Title = "浏览安装目录";
            installpath.Directory = install_path;
            Window parent = new Window();
            var result= await installpath.ShowAsync(parent);
            Install_path=result;
            
        }
    }
}