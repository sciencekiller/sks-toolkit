using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sks_toolkit
{
    public class BindingData
    {
        private string currentuser;
        public string CurrentUser
        {
            set { this.currentuser = value; }
            get { return this.currentuser; }
        }
        private string greetingword;
        public string GreetingWord
        {
            set { this.greetingword = value; }
            get { return this.greetingword; }
        }
    }
}
