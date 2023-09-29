using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using WinRT.Interop;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace sks_toolkit
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private string launchMode;
        private Scighost.WinUILib.Helpers.SystemBackdrop systemBackDrop;
        private IntPtr hwnd;
        private AppWindow appWindow;
        public MainWindow(string lm)
        {
            launchMode = lm;
            InitializeComponent();
            if (launchMode == "FirstLaunch")
            {
                    mainFrame.Navigate(typeof(FirstLaunch.MainFrame));
            }
            else if (launchMode == "CommonLaunch")
            {
                mainFrame.Navigate(typeof(CommonLaunch.MainFrame));
            }
            systemBackDrop = new Scighost.WinUILib.Helpers.SystemBackdrop(this);
            systemBackDrop.TrySetMica(useMicaAlt: true, fallbackToAcrylic: true);
            hwnd = WindowNative.GetWindowHandle(this);
            WindowId id = Win32Interop.GetWindowIdFromWindow(hwnd);
            appWindow = AppWindow.GetFromWindowId(id);
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = appWindow.TitleBar;
                titleBar.ExtendsContentIntoTitleBar = true;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            var screenHeight=DisplayArea.Primary.WorkArea.Height;
            var screenWidth=DisplayArea.Primary.WorkArea.Width;
            appWindow.MoveAndResize(new Windows.Graphics.RectInt32((screenWidth - 1700) / 2, (screenHeight - 900) / 2, 1700, 900));
        }
    }
}
