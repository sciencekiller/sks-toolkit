using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sks_toolkit
{
    internal class BindingData
    {
        private string currentuser = "ERROR?";
        public string CurrentUser
        {
            set { this.currentuser = value; }
            get { return this.currentuser; }
        }
        private string greetingword = "ERROR?";
        public string GreetingWord
        {
            set { this.greetingword = value; }
            get { return this.greetingword; }
        }
        private string greetingsentence = "ERROR?";
        public string GreetingSentence
        {
            set { this.greetingsentence = value; }
            get { return this.greetingsentence; }
        }
        private string version = "ERROR?";
        public string Version
        {
            set { this.version = value; }
            get { return this.version; }
        }
        private string channel = "ERROR?";
        public string Channel
        {
            set { this.channel = value; }
            get { return this.channel; }
        }
        private string build = "ERROR?";
        public string Build
        {
            set { this.build = value; }
            get { return this.build; }
        }
        private string latestversion = "ERROR?";
        public string latestVersion
        {
            set { this.latestversion = value; }
            get { return this.latestversion; }
        }
        private string latestornot = "ERROR?";
        public string latestOrNot
        {
            set { this.latestornot = value; }
            get { return this.latestornot; }
        }
    }
    internal class Deploy_ENV_Data
    {
        private List<string> gpp_download_links = new List<string>();
        public List<string> Gpp_download_links
        {
            set { this.gpp_download_links = value; }
            get { return this.gpp_download_links; }
        }
    }
}
