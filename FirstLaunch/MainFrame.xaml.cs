using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Win32;
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
        private bool currentColorMode;
        private int currentSet = 0;
        private JsonObject settings=new JsonObject();
        public MainFrame()
        {
            InitializeComponent();
            currentColorMode = Scripts.ThemeHelper.isUsingDarkMode();
            SetTheme();
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnThemeChanged);
            settingFrame.Navigate(typeof(Pages.SetLanguage));
        }
        private void OnThemeChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (currentColorMode != Scripts.ThemeHelper.isUsingDarkMode()) currentColorMode = !currentColorMode;
            else return;
            if (currentColorMode) SetThemeToDark();
            else SetThemeToLight();
            return;
        }
        private void SetTheme()
        {
            if (currentColorMode) SetThemeToDark();
            else SetThemeToLight();
            return;
        }
        private void SetThemeToDark()
        {
            AcrylicBrush settingBoxBrush = (AcrylicBrush)Application.Current.Resources["FirstLaunchSettingBoxBrushDark"];
            SettingBox.Background = settingBoxBrush;
            SolidColorBrush nextButtonBrush = (SolidColorBrush)Application.Current.Resources["FirstLaunchNextButtonBrushDark"];
            NextStepButton.Background = nextButtonBrush;
        }
        private void SetThemeToLight()
        {
            AcrylicBrush settingBoxBrush = (AcrylicBrush)Application.Current.Resources["FirstLaunchSettingBoxBrushLight"];
            SettingBox.Background = settingBoxBrush;
            SolidColorBrush nextButtonBrush = (SolidColorBrush)Application.Current.Resources["FirstLaunchNextButtonBrushLight"];
            NextStepButton.Background = nextButtonBrush;
        }
        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            GetValue(currentSet);
            NextPage(currentSet);
        }
        private void GetValue(int n)
        {
            switch (n)
            {
                case 1:
                    string language = Pages.SetLanguage.getValue();
                    settings.Add("Language", language);
                    break;
            }
        }
        private void NextPage(int n)
        {
            n++;
            switch (n)
            {
                case 2:
                    break;
            }
        }
    }
}
