using Microsoft.UI.Xaml.Controls;
using sks_toolkit.Scripts;
using System.Text.Json.Nodes;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace sks_toolkit.FirstLaunch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainFrame : Page
    {
        private JsonObject settings=new JsonObject();
        private LanguageHelper lh = new LanguageHelper();
        public MainFrame()
        {
            InitializeComponent();
            SetLanguage();
        }
        private void SetLanguage()
        {
            var language = System.Globalization.CultureInfo.InstalledUICulture.Name;
            SetLanguage(language);
        }
        private void SetLanguage(string language)
        {
            lh.SetLanguage(language);
            WelcomeWord.Text = lh.GetString("WelcomePage_Header");
            NextStepWord.Text = lh.GetString("WelcomePage_Next");
            FirstGreeting.Text = lh.GetString("SetLanguage_FirstGreeting");
            ToStartSet.Text = lh.GetString("SetLanguage_ToStartSet");
            HowToStart.Text = lh.GetString("SetLanguage_HowToStart");
        }
    }
}
