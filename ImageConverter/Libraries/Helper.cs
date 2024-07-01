using ConsoleProgressBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static SortLib.Natural;

namespace ImageConverter.Libraries {
  public static class Helper {
    // Extend DirectoryInfo
    public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dirInfo, params string[] extensions) {
      if (extensions == null) throw new ArgumentNullException("extensions");
      var allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);
      return dirInfo.EnumerateFiles().Where(f => allowedExtensions.Contains(f.Extension));
    }
    // Extend ProgressBar
    public static void SetProcessingText(this ProgressBar progressBar, string text) {
      progressBar.Text.Body.Processing.SetValue(text);
    }
    // Helper methods
    public static T[] ConcatArrays<T>(params T[][] p) {
      var pos = 0;
      var result = new T[p.Sum(a => a.Length)];
      foreach (var c in p) {
        Array.Copy(c, 0, result, pos, c.Length);
        pos += c.Length;
      }
      return result;
    }
    public static FileInfo[] GetImageFiles(string target) {
      return GetImageFiles(target, new[] { ".jpg", ".png", ".webp" });
    }
    public static FileInfo[] GetImageFiles(string target, string[] extensions) {
      DirectoryInfo targetInfo = new DirectoryInfo(target);
      if (!targetInfo.Exists) return new FileInfo[0];

      return targetInfo.GetFilesByExtensions(extensions).OrderBy(i => i.Name, new NaturalComparer()).ToArray();
    }
    public static FileInfo[] GetSubdirImageFiles(string target, string subdir) {
      return GetSubdirImageFiles(target, subdir, new[] { ".jpg", ".png", ".webp" });
    }
    public static FileInfo[] GetSubdirImageFiles(string target, string subdir, string[] extensions) {
      string newPath = Path.Combine(target, subdir);
      return GetImageFiles(newPath, extensions);
    }
    public static ProgressBar NewProgressBar(int max) {
      ProgressBar pB = new ProgressBar { Maximum = max, FixedInBottom = true };
      pB.Text.Description.Clear();
      pB.Text.Body.Done.SetValue(pb => $"Processed {pb.Maximum} files in {pb.TimeProcessing.TotalSeconds}s.");
      pB.Text.Body.Processing.SetValue(pb => $"{pb.ElementName}");

      return pB;
    }
  }
}
