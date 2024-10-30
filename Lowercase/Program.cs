using MyConsole;
using System;
using System.IO;

namespace Lowercase {
  internal class Program {
    private static void Main(string[] args) {
      string targetDir;
      if (args.Length > 0) targetDir = args[0];
      else targetDir = Directory.GetCurrentDirectory();

      if (!Directory.Exists(targetDir)) {
        MCS.Error("Provided path is not a directory.");
        Environment.Exit(1);
      }

      while (true) {
        MCS.Info(targetDir.Replace("\\", "/"), "TARGET");
        Console.Write("Continue? (y/n): ");
        string res = Console.ReadLine().ToLower();

        if (res == "y") break;
        else if (res == "n") return;
        else MCS.Error("Invalid input. Enter 'y' to continue or 'n' to abort.");
      }

      DirectoryInfo di = new DirectoryInfo(targetDir);
      Rename(di, true, true);
      Rename(di, true, false);
      MCS.Info("Rename complete");
    }

    private static void Rename(DirectoryInfo di, bool excludeSelf = false, bool isTemp = false) {
      foreach (var subdir in di.GetDirectories()) {
        Rename(subdir, false, isTemp);
      }
      foreach (var file in di.GetFiles()) {
        string newFileName = isTemp ? file.Name + "-t11emp" : file.Name.Replace("-t11emp", "").ToLower();
        string newFilePath = Path.Combine(file.DirectoryName, newFileName);
        if (file.FullName != newFilePath) {
          File.Move(file.FullName, newFilePath);
        }
      }
      if (!excludeSelf) {
        string newDirName = isTemp ? di.Name + "-t11emp" : di.Name.Replace("-t11emp", "").ToLower();
        string newDirPath = Path.Combine(di.Parent.FullName, newDirName);
        if (di.FullName != newDirPath) {
          Directory.Move(di.FullName, newDirPath);
        }
      }
    }
  }
}
