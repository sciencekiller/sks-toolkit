using Microsoft.UI.Xaml;
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
        private int currentSet = 1;
        private const int totalSet = 2;
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
            FinishWord.Text = lh.GetString("WelcomePage_Finish");
            BackWord.Text = lh.GetString("WelcomePage_Back");
            FirstGreeting.Text = lh.GetString("SetLanguage_FirstGreeting");
            ToStartSet.Text = lh.GetString("SetLanguage_ToStartSet");
            HowToStart.Text = lh.GetString("SetLanguage_HowToStart");
            IsRightLanguage.Text = lh.GetString("SetLanguage_IsRightLanguage");
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            CloseSetting(currentSet);//隐藏已经设置
            currentSet++;//正在设置自加
            StartSetting(currentSet);//显示下一个设置
            if(currentSet == totalSet)//最后一个设置
            {
                NextStepButton.Visibility = Visibility.Collapsed;
                FinishButton.Visibility = Visibility.Visible;
            }
            if (currentSet > 1)
            {
                BackButton.Visibility = Visibility.Visible;
            }

        }
        private void CloseSetting(int n)
        {
            switch (n)
            {
                case 1:
                    StartSet.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void StartSetting(int n)
        {
            switch (n)
            {
                case 2:
                    LanguageSetting.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
