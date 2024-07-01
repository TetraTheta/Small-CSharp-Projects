using System.IO;
using System;
using static MyConsole.MyConsole;

namespace Lowercase {
  internal class Program {
    private static void Main(string[] args) {
      string targetDir;
      if (args.Length > 0) targetDir = args[0];
      else targetDir = Directory.GetCurrentDirectory();

      if (!Directory.Exists(targetDir)) {
        Error("Provided path is not a directory.");
        Environment.Exit(1);
      }

      while (true) {
        Info(targetDir.Replace("\\", "/"), "TARGET");
        Console.Write("Continue? (y/n): ");
        string res = Console.ReadLine().ToLower();

        if (res == "y") break;
        else if (res == "n") return;
        else Error("Invalid input. Enter 'y' to continue or 'n' to abort.");
      }

      DirectoryInfo di = new DirectoryInfo(targetDir);
      Rename(di, true, true);
      Rename(di, true, false);
      Info("Rename complete");
    }

    static void Rename(DirectoryInfo di, bool excludeSelf = false, bool isTemp = false) {
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
