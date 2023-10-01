using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using sks_toolkit.Scripts;
using System.IO;
using System.Text.Json;
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
        private LanguageHelper lh = new LanguageHelper();
        private string language = System.Globalization.CultureInfo.InstalledUICulture.Name;
        public MainFrame()
        {
            InitializeComponent();
            SetLanguage();
            ListLanguages();
        }
        private void ListLanguages()
        {
            var ls = lh.GetSupportLanguages();
            SelectLanguage.ItemsSource = ls;
            SelectLanguage.SelectedValue=lh.GetLanguageNameFromCode(language);
        }
        private void SetLanguage()
        {
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
            AskLanguage.Text = lh.GetString("SetLanguage_AskLanguage");
        }
        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            CloseSetting(currentSet);//隐藏已经设置
            currentSet++;//正在设置自加
            StartSetting(currentSet);//显示下一个设置
            if (currentSet == totalSet)//最后一个设置
            {
                NextStepButton.Visibility = Visibility.Collapsed;
                FinishButton.Visibility = Visibility.Visible;
            }
            if (currentSet > 1)
            {
                BackButton.Visibility = Visibility.Visible;
            }

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSet == totalSet)
            {
                NextStepButton.Visibility = Visibility.Visible;
                FinishButton.Visibility = Visibility.Collapsed;
            }
            if (currentSet == 2)
            {
                BackButton.Visibility = Visibility.Collapsed;
            }
            CloseSetting(currentSet);
            currentSet--;
            StartSetting(currentSet);
        }
        private void CloseSetting(int n)
        {
            switch (n)
            {
                case 1:
                    StartSet.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    LanguageSetting.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void StartSetting(int n)
        {
            switch (n)
            {
                case 1:
                    StartSet.Visibility = Visibility.Visible;
                    break;
                case 2:
                    LanguageSetting.Visibility = Visibility.Visible;
                    break;
            }
        }
        private JsonObject GetSettings()
        {
            JsonObject settings = new JsonObject();
            //settings.Add("Language",)
            return settings;
        }
        private async void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            JsonObject jo = GetSettings();
            string j = JsonSerializer.Serialize(jo);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Path.GetTempPath(), "sks_toolkit", "config.json")))
            {
                await outputFile.WriteAsync(j);
            }
        }

        private void SelectLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            language=lh.GetLanguageCodeFromName(SelectLanguage.SelectedValue.ToString());
            SetLanguage(language);
        }
    }
}
