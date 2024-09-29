using System;
using System.IO;
using System.Linq;
using MyConsole;

namespace RemoveEmptyDirectories {
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

      RemoveEmptyDirectories(targetDir);
    }

    private static void RemoveEmptyDirectories(string target) {
      string[] importants = new string[] { "System Volume Information", "RECYCLER", "Recycled", "NtUninstall", "$RECYCLE.BIN", "GAC_MSIL", "GAC_32", "WinSxS", "System32" };
      string[] emptyFiles = new string[] { "desktop.ini", "Thumbs.db", ".DS_Store" };
      string[] emptyFilesEnds = new string[] { ".log" };
      string[] emptyFilesStarts = new string[] { "._" };
      if (importants.Any(dir => string.Equals(Path.GetFileName(target), dir, StringComparison.OrdinalIgnoreCase))) return;
      foreach (string dir in Directory.GetDirectories(target)) {
        RemoveEmptyDirectories(dir);
      }

      string[] files = Directory.GetFiles(target);
      string[] dirs = Directory.GetDirectories(target);

      bool isEmpty = files.All(f => emptyFiles.Contains(Path.GetFileName(f), StringComparer.OrdinalIgnoreCase) ||
                                    emptyFilesEnds.Any(end => f.EndsWith(end, StringComparison.OrdinalIgnoreCase)) ||
                                    emptyFilesStarts.Any(start => Path.GetFileName(f).StartsWith(start, StringComparison.OrdinalIgnoreCase))) &&
                     !dirs.Any();

      if (isEmpty) {
        try {
          Directory.Delete(target, true);
          MCS.Info(target.Replace("\\", "/"), "DELETED");
        } catch (Exception ex) {
          MCS.Error($"Failed to delete '{target}': {ex.Message}");
        }
      }
    }
  }
}
