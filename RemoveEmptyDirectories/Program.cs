using CommandLine;
using CommandLine.Text;
using MyConsole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RemoveEmptyDirectories {
  internal class Program {
    private class Options {
      [Option('y', "yes", Default = false, HelpText = "Skip user confirmation")]
      public bool IsYes { get; set; }

      [Value(0, Required = false, HelpText = "Target directory")]
      public string Target { get; set; } = Directory.GetCurrentDirectory();
    }

    private static int DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs) {
      HelpText helpText = null;
      if (errs.IsVersion()) {
        helpText = HelpText.AutoBuild(result, maxDisplayWidth: Console.WindowWidth);
      } else {
        helpText = HelpText.AutoBuild(result, h => HelpText.DefaultParsingErrorsHandler(result, h), e => e, true, maxDisplayWidth: Console.WindowWidth);
      }
      Console.WriteLine(helpText);
      return 1;
    }

    private static void Main(string[] args) {
      var result = new Parser(c => {
        c.HelpWriter = null;
        c.CaseSensitive = false;
        c.CaseInsensitiveEnumValues = true;
      }).ParseArguments(args, typeof(Options));
      result.WithParsed<Options>(opt => {
        if (!Directory.Exists(opt.Target)) {
          MCS.Error("Provided path is not a directory.");
          Environment.Exit(1);
        }

        while (true) {
          string p = opt.Target.Replace("\\", "/");
          MCS.Info(p, "TARGET");
          Console.Write("Continue? (y/n): ");
          string res = Console.ReadLine().ToLower();

          if (res == "y") break;
          else if (res == "n") return;
          else MCS.Error("Invalid input. Enter 'y' to continue or 'n' to abort.");
        }

        RemoveEmptyDirectories(opt.Target);
      });
      result.WithNotParsed(errs => DisplayHelp(result, errs));
    }

    private static void RemoveEmptyDirectories(string target) {
      string[] importants = new string[] { "System Volume Information", "RECYCLER", "Recycled", "NtUninstall", "$RECYCLE.BIN", "GAC_MSIL", "GAC_32", "WinSxS", "Start Menu", "System32" };
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
