using MyConsole;
using SortLib;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Renumber {
  internal class Program {
    private static void Main(string[] args) {
      string targetDir;
      if (args.Length > 0) targetDir = args[0];
      else targetDir = Directory.GetCurrentDirectory();

      if (!Directory.Exists(targetDir)) {
        MCS.Error("Provided path is not a directory.");
        Environment.Exit(1);
      }

      MCS.Info(targetDir.Replace("\\", "/"), "TARGET");

      string[] webpFiles = Directory.GetFiles(targetDir, "*.webp").Where(f => Regex.IsMatch(Path.GetFileNameWithoutExtension(f), @"^\d+$")).ToArray();

      if (webpFiles.Length == 0) {
        MCS.Error("There is no WebP file to rename.");
        Environment.Exit(0);
      }

      Array.Sort(webpFiles, new Natural.NaturalComparer());

      for (int i = 0; i < webpFiles.Length; i++) {
        string tempFileName = Path.Combine(targetDir, Path.GetFileNameWithoutExtension(webpFiles[i]) + "-temp.webp");
        File.Move(webpFiles[i], tempFileName);
        webpFiles[i] = tempFileName;
      }

      for (int i = 0; i < webpFiles.Length; i++) {
        string newFileName = Path.Combine(targetDir, (i + 1).ToString("D3") + ".webp");
        File.Move(webpFiles[i], newFileName);
      }
      MCS.Info("Rename complete");
    }
  }
}
