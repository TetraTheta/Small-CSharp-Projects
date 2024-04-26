using System.IO;

namespace ConvertWebP.Libraries {
  public class Options {
    public string Path;
    public int Width;

    public Options(string path, int width) {
      Path = string.IsNullOrEmpty(path) ? Directory.GetCurrentDirectory() : path;
      Width = width <= 0 ? 1280 : width;
    }
  }
}
