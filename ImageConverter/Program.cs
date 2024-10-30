using CommandLine;
using CommandLine.Text;
using ImageConverter.Libraries;
using MyConsole;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImageConverter {
  public static class Program {
    public static string OriginalTitle = null;

    private static void Main(string[] args) {
      OriginalTitle = Console.Title;

      Type[] _types = {
        typeof(AllOptions),
        typeof(BackgroundOptions),
        typeof(CenterOptions),
        typeof(CreateDirectoryOptions),
        typeof(ForegroundOptions),
        typeof(FullOptions)
      };

      var result = new Parser(c => {
        c.HelpWriter = null;
        c.CaseSensitive = false;
        c.CaseInsensitiveEnumValues = true;
      }).ParseArguments(args, _types);
      result.WithParsed<AllOptions>(opt => {
        CheckValidity(opt);
        new Runner(opt);
      });
      result.WithParsed<BackgroundOptions>(opt => {
        CheckValidity(opt);
        new Runner(opt);
      });
      result.WithParsed<CenterOptions>(opt => {
        CheckValidity(opt);
        new Runner(opt);
      });
      result.WithParsed<CreateDirectoryOptions>(opt => {
        CheckValidity(opt);
        new Runner(opt);
      });
      result.WithParsed<ForegroundOptions>(opt => {
        CheckValidity(opt);
        new Runner(opt);
      });
      result.WithParsed<FullOptions>(opt => {
        CheckValidity(opt);
        new Runner(opt);
      });
      result.WithNotParsed(errs => DisplayHelp(result, errs));
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

    private static void CheckValidity(IOptions opt) {
      List<string> msgs = new List<string>();
      bool isOK = true;
      // 1. Dependency check
      if (!External.Exists("cwebp.exe")) {
        isOK = false;
        msgs.Add(Res.Error_NoDep_Cwebp);
      }
      if (!External.Exists("ffmpeg.exe")) {
        isOK = false;
        msgs.Add(Res.Error_NoDep_FFmpeg);
      }
      if (!External.Exists("magick.exe")) {
        isOK = false;
        msgs.Add(Res.Error_NoDep_Magick);
      }
      // 2. Option check
      if ((File.GetAttributes(opt.Target) & FileAttributes.Directory) != FileAttributes.Directory) {
        isOK = false;
        msgs.Add(Res.Error_Target_NonDir + opt.Target);
      }
      if (opt is AllOptions aopt) {
        bool subdirOK = false;
        string[] subdirs = new string[] { "IC-Background", "IC-Center", "IC-Foreground-0", "IC-Foreground-1", "IC-Foreground-2", "IC-Foreground-3", "IC-Foreground-4", "IC-Full" };
        foreach (string dir in subdirs) {
          string fullPath = Path.Combine(aopt.Target, dir);
          if (Directory.Exists(fullPath)) subdirOK = true;
        }
        if (!subdirOK) msgs.Add(Res.Error_All_NoDir);
      } else if (opt is ForegroundOptions fopt) {
        if (fopt.ChatCount < 0 || fopt.ChatCount > 4) {
          isOK = false;
          msgs.Add(Res.Error_Fore_CC_Over);
        }
      }
      // Print error
      if (!isOK && msgs.Count > 0) {
        foreach (string msg in msgs) {
          MCS.Error(msg);
        }
        Environment.Exit(1);
      }
    }
  }
}
