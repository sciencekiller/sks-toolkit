using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sks_toolkit
{
    public class BindingData
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
        private string greetingsentence="ERROR?";
        public string GreetingSentence
        {
            set { this.greetingsentence = value; }
            get { return this.greetingsentence; }
        }
        private string version="ERROR?";
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
    }
}
