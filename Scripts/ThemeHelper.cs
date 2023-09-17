using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sks_toolkit.Scripts
{
    internal class ThemeHelper
    {
        internal static bool isUsingDarkMode()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            object colorModeObject = registry.OpenSubKey("AppsUseLightTheme");
            if (colorModeObject is null) return false;
            return (int)colorModeObject > 0 ? false : true;
        }
    }
}
