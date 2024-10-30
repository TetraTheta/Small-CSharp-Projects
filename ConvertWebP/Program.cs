using ConvertWebP.Libraries;
using ConvertWebP.Properties;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ConvertWebP {
  internal static class Program {
    [STAThread]
    private static void Main(string[] args) {
      // TODO: How can I localize this application?
      Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
      Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;

      Options options = ArgumentParser.Parse(args);

      // Check cwebp and magick
      if (!RunPrograms.CheckCwebp()) {
        Dialogs.ShowError(Resources.ErrNoCwebp);
      }

      if (!RunPrograms.CheckMagick()) {
        Dialogs.ShowError(Resources.ErrNoMagick);
      }
      // Check file list
      string[] files = FilePath.GetImageFileArray(options.Path);
      if (files.Length == 0) {
        Dialogs.ShowError(Resources.ErrNoFileToProcess);
      }
      // Create 'converted' subdir
      string convertedPath = Path.Combine(FilePath.GetParentDirectory(options.Path), "converted");
      if (!Directory.Exists(convertedPath)) {
        Directory.CreateDirectory(convertedPath);
      }
      // Convert image files (opens Form window)
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new ProgressForm(files, options.Width, options.Path, convertedPath));
    }
  }
}
