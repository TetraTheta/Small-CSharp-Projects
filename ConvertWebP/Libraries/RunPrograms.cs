using ConvertWebP.Properties;
using System;
using System.Diagnostics;
using System.IO;

namespace ConvertWebP.Libraries {

  public class RunPrograms {

    public static bool CheckCwebp() {
      string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');
      foreach (string path in paths) {
        string fullPath = Path.Combine(path, "cwebp.exe");
        if (File.Exists(fullPath)) return true;
      }
      return false;
    }

    public static bool CheckMagick() {
      string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');
      foreach (string path in paths) {
        string fullPath = Path.Combine(path, "magick.exe");
        if (File.Exists(fullPath)) return true;
      }
      return false;
    }

    public static int GetImageWidth(string file) {
      ProcessStartInfo startInfo = new ProcessStartInfo {
        FileName = "magick.exe",
        Arguments = $"identify -ping -format \"%w\" \"{file}\"",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
      };
      Process process = Process.Start(startInfo);
      process.Start();
      string output = process.StandardOutput.ReadLine();
      if (process.ExitCode != 0 || string.IsNullOrEmpty(output)) {
        Dialogs.ShowError(Resources.ErrInvalidPath + "\n" + file);
        return 0;
      }
      try {
        return int.Parse(output);
      } catch (Exception e) {
        Dialogs.ShowError(e);
        return 0;
      }
    }

    public static int RunCwebp(string file, int targetWidth, string outputPath) {
      string[] argument = GetImageWidth(file) <= targetWidth
        ? (new[] {
          "-preset default",
          "-q 85",
          "-m 6",
          "-pass 10",
          "-mt",
          "-quiet",
          $"-o \"{outputPath}\\{Path.GetFileNameWithoutExtension(file)}.webp\"",
          $"-- \"{file}\""
        })
        : (new[] {
          "-preset default",
          "-q 85",
          "-m 6",
          "-pass 10",
          $"-resize {targetWidth} 0",
          "-mt",
          "-quiet",
          $"-o \"{outputPath}\\{Path.GetFileNameWithoutExtension(file)}.webp\"",
          $"-- \"{file}\""
        });
      ProcessStartInfo startInfo = new ProcessStartInfo {
        FileName = "cwebp.exe",
        Arguments = string.Join(" ", argument),
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
      };
      Process process = Process.Start(startInfo);
      process.Start();
      process.WaitForExit();
      return process.ExitCode;
    }
  }
}
