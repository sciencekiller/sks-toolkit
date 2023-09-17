using Microsoft.UI.Xaml;
using System.Diagnostics;
using System.IO;
using Windows.Security.Cryptography.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace sks_toolkit
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private string launchMode;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            if (!File.Exists(Path.Combine(Path.GetTempPath(), "sks_toolkit", "config.json")))
            {
                launchMode = "FirstLaunch";
            }
            else
            {
                launchMode = "CommonLaunch";
            }
            m_window = new MainWindow(launchMode);
            m_window.Activate();
        }

        private Window m_window;
    }
}
