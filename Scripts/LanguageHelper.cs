using System.Collections.Generic;
using Windows.Web.Http;
using ResourceManager=System.Resources.ResourceManager;

namespace sks_toolkit.Scripts
{
    class LanguageHelper
    {
        private static string language="zh-CN";
        internal string Language { get; set; }
        private static Dictionary<string,ResourceManager> rms= new Dictionary<string,ResourceManager>();
        public LanguageHelper(string l)
        {
            language = l;
            GetLanguages();
        }
        public LanguageHelper()
        {
            GetLanguages();
        }
        internal void SetLanguage(string l)
        {
            language = l;
        }
        private void GetLanguages()
        {
            ResourceManager rm = new ResourceManager("sks_toolkit.Resources.AppInfo", typeof(LanguageHelper).Assembly);
            string languages = rm.GetString("LanguageSupport");
            string[] languageArray= languages.Split(';');
            foreach(string l in languageArray)
            {
                rms.Add(l, new ResourceManager("sks_toolkit.Resources.Languages." + l, typeof(LanguageHelper).Assembly));
            }
        }
        internal string GetString(string language, string key)
        {
            if (rms.ContainsKey(language))
            {
                return rms[language].GetString(key);
            }
            return "Text Not Found";
        }
        internal string GetString(string key)
        {
            return GetString(language, key);
        }
    }
}
