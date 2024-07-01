using System.IO;

namespace ImageConverter.Libraries {
  public static class Dirs {
    public static string BG(string parent) {
      return Path.Combine(parent, "IC-Background");
    }
    public static string BGC(string parent) {
      return Path.Combine(parent, "IC-Background", "converted");
    }
    public static string C(string parent) {
      return Path.Combine(parent, "IC-Center");
    }
    public static string CC(string parent) {
      return Path.Combine(parent, "IC-Center", "converted");
    }
    public static string FG0(string parent) {
      return Path.Combine(parent, "IC-Foreground-0");
    }
    public static string FG0C(string parent) {
      return Path.Combine(parent, "IC-Foreground-0", "converted");
    }
    public static string FG1(string parent) {
      return Path.Combine(parent, "IC-Foreground-1");
    }
    public static string FG1C(string parent) {
      return Path.Combine(parent, "IC-Foreground-1", "converted");
    }
    public static string FG2(string parent) {
      return Path.Combine(parent, "IC-Foreground-2");
    }
    public static string FG2C(string parent) {
      return Path.Combine(parent, "IC-Foreground-2", "converted");
    }
    public static string FG3(string parent) {
      return Path.Combine(parent, "IC-Foreground-3");
    }
    public static string FG3C(string parent) {
      return Path.Combine(parent, "IC-Foreground-3", "converted");
    }
    public static string FG4(string parent) {
      return Path.Combine(parent, "IC-Foreground-4");
    }
    public static string FG4C(string parent) {
      return Path.Combine(parent, "IC-Foreground-4", "converted");
    }
    public static string F(string parent) {
      return Path.Combine(parent, "IC-Full");
    }
    public static string FC(string parent) {
      return Path.Combine(parent, "IC-Full", "converted");
    }
  }
}
