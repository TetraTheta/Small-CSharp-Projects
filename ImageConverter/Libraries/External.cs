using System;
using System.Diagnostics;
using System.IO;
using MyConsole;

namespace ImageConverter.Libraries {
  public static class External {
    public static void ProcessImage(FileInfo fi, GameDefinition gd) {
      int width = GetImageWidth(fi.FullName);

      string convertedDir = Path.Combine(Path.GetDirectoryName(fi.FullName), "converted");
      Directory.CreateDirectory(convertedDir);
      string convertedImagePath = Path.Combine(convertedDir, Path.GetFileNameWithoutExtension(fi.FullName) + ".webp");

      if (width == 1920) {
        string preWebPDir = Path.Combine(Path.GetDirectoryName(fi.FullName), "_pre_webp");
        Directory.CreateDirectory(preWebPDir);
        string preWebPImagePath = Path.Combine(preWebPDir, Path.GetFileName(fi.FullName));

        RunFFmpeg(fi.FullName, preWebPImagePath, gd);
        RunCwebp(preWebPImagePath, convertedImagePath);
      } else {
        RunCwebp(fi.FullName, convertedImagePath);
      }
    }
    public static void RunFFmpeg(string input, string output, GameDefinition gd) {
      string filter = $"[0:v]crop={gd.UID_AREA}:{gd.UID_POS}[c];[c]boxblur=3:15[bc];[0:v][bc]overlay={gd.UID_POS}[h];[h]";

      if (gd.CROP_POS == CropPosition.Bottom) {
        filter += $"crop=in_w:{gd.CROP_HEIGHT}:0:in_h-{gd.CROP_HEIGHT},scale=1280:-1[out]";
      } else if (gd.CROP_POS == CropPosition.Center) {
        filter += $"crop=in_w:{gd.CROP_HEIGHT}:0:(in_h-{gd.CROP_HEIGHT})/2,scale=1280:-1[out]";
      } else if (gd.CROP_POS == CropPosition.Full) {
        filter += $"scale=1280:-1[out]";
      }

      string arg = $"-i \"{input}\" -filter_complex \"{filter}\" -map \"[out]\" -y \"{output}\"";
      Run("ffmpeg.exe", arg);
    }
    public static void RunCwebp(string input, string output) {
      string arg = $"-o \"{output}\" -q 85 -m 6 -pass 10 -af -mt -- \"{input}\"";
      Run("cwebp.exe", arg);
    }

    public static bool Exists(string executable) {
      string fullPath = null;
      if (File.Exists(executable)) fullPath = Path.GetFullPath(executable);
      string env = Environment.GetEnvironmentVariable("PATH");
      foreach (string path in env.Split(Path.PathSeparator)) {
        string pathPath = Path.Combine(path, executable);
        if (File.Exists(pathPath)) fullPath = pathPath;
      }
      return fullPath != null;
    }
    public static int GetImageWidth(string imagePath) {
      string result = Run("magick.exe", $"identify -ping -format %w \"{imagePath}\"");
      return int.Parse(result);
    }

    public static string Run(string fileName, string arguments) {
      Process proc = new Process();
      proc.StartInfo.FileName = fileName;
      proc.StartInfo.Arguments = arguments;
      proc.StartInfo.RedirectStandardOutput = true;
      proc.StartInfo.RedirectStandardError = true;
      proc.StartInfo.UseShellExecute = false;
      proc.StartInfo.CreateNoWindow = true;

      proc.Start();
      string result = proc.StandardOutput.ReadToEnd();
      string error = proc.StandardError.ReadToEnd();
      proc.WaitForExit();

      if (proc.ExitCode != 0) {
        MCS.Error(error);
      }

      return result.Trim();
    }
  }
}
