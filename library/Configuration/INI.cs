using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Configuration {
  public static class INI {
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    public static string GetINIPathPortable(string appName) {
      string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      return Path.Combine(exePath, $"{appName}.ini");
    }

    public static string GetINIPathAppData(string appName) {
      string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      return Path.Combine(Path.Combine(appDataPath, appName), $"{appName}.ini");
    }
  }
}
