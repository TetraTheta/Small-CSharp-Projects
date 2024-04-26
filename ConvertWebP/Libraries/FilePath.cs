using ConvertWebP.Properties;
using System.IO;
using System.Linq;

namespace ConvertWebP.Libraries {
  public class FilePath {
    public static string[] GetImageFileArray(string path) {
      if (File.Exists(path)) {
        return new string[] { Path.GetFullPath(path) };
      } else if (Directory.Exists(path)) {
        string[] extensions = { ".bmp", ".jpg", ".jpeg", ".png", ".webp" };
        return NaturalSort.Sort(Directory.GetFiles(path).Where(file => extensions.Contains(Path.GetExtension(file).ToLower())).ToArray());
      } else {
        Dialogs.ShowError(Resources.ErrInvalidPath + "\n" + path);
        return null;
      }
    }

    public static string GetParentDirectory(string path) {
      if (File.Exists(path)) return Path.GetDirectoryName(path);
      else if (Directory.Exists(path)) return Path.GetFullPath(path);
      else {
        Dialogs.ShowError(Resources.ErrInvalidPath + "\n" + path);
        return null;
      }
    }
  }
}
