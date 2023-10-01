using ResourceManager = System.Resources.ResourceManager;
namespace sks_toolkit.Scripts
{
    internal class AppInfoHelper
    {
        private static ResourceManager rm = new ResourceManager("sks_toolkit.Resources.AppInfo", typeof(AppInfoHelper).Assembly);
        internal static string GetInfo(string key)
        {
            return rm.GetString(key);
        }
    }
}
