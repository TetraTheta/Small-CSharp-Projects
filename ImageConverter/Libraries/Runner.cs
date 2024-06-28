using ConsoleProgressBar;
using System;
using System.IO;
using static MyConsole.MyConsole;

namespace ImageConverter.Libraries {
  public class Runner {
    // All
    public Runner(AllOptions opt) {
      FileInfo[] fiBackground = Helper.GetImageFiles(Path.Combine(opt.Target, "IC-Background"));
      FileInfo[] fiCenter = Helper.GetImageFiles(Path.Combine(opt.Target, "IC-Center"));
      FileInfo[] fiForeground0 = Helper.GetImageFiles(Path.Combine(opt.Target, "IC-Foreground-0"));
      FileInfo[] fiForeground1 = Helper.GetImageFiles(Path.Combine(opt.Target, "IC-Foreground-1"));
      FileInfo[] fiForeground2 = Helper.GetImageFiles(Path.Combine(opt.Target, "IC-Foreground-2"));
      FileInfo[] fiForeground3 = Helper.GetImageFiles(Path.Combine(opt.Target, "IC-Foreground-3"));
      FileInfo[] fiForeground4 = Helper.GetImageFiles(Path.Combine(opt.Target, "IC-Foreground-4"));
      FileInfo[] fiFull = Helper.GetImageFiles(Path.Combine(opt.Target, "IC-Full"));

      fiBackground = fiBackground.Length > 0 ? fiBackground : null;
      fiCenter = fiCenter.Length > 0 ? fiCenter : null;
      fiForeground0 = fiForeground0.Length > 0 ? fiForeground0 : null;
      fiForeground1 = fiForeground1.Length > 0 ? fiForeground1 : null;
      fiForeground2 = fiForeground2.Length > 0 ? fiForeground2 : null;
      fiForeground3 = fiForeground3.Length > 0 ? fiForeground3 : null;
      fiForeground4 = fiForeground4.Length > 0 ? fiForeground4 : null;
      fiFull = fiFull.Length > 0 ? fiFull : null;

      FileInfo[] fiAll = new FileInfo[0];
      foreach (FileInfo[] fi in new FileInfo[][] { fiBackground, fiCenter, fiForeground0, fiForeground1, fiForeground2, fiForeground3, fiForeground4, fiFull }) {
        if (fi != null && fi.Length > 0) fiAll = Helper.ConcatArrays(fiAll, fi);
      }

      ProgressBar pb = Helper.NewProgressBar(fiAll.Length);

      if (fiBackground != null) Progress(new GameDefinition(opt.Game, Operation.Background), fiBackground, "Background", pb);
      if (fiCenter != null) Progress(new GameDefinition(opt.Game, Operation.Center), fiCenter, "Center", pb);
      if (fiForeground0 != null) Progress(new GameDefinition(opt.Game, Operation.Foreground0), fiForeground0, "Foreground 0", pb);
      if (fiForeground1 != null) Progress(new GameDefinition(opt.Game, Operation.Foreground1), fiForeground1, "Foreground 1", pb);
      if (fiForeground2 != null) Progress(new GameDefinition(opt.Game, Operation.Foreground2), fiForeground2, "Foreground 2", pb);
      if (fiForeground3 != null) Progress(new GameDefinition(opt.Game, Operation.Foreground3), fiForeground3, "Foreground 3", pb);
      if (fiForeground4 != null) Progress(new GameDefinition(opt.Game, Operation.Foreground4), fiForeground4, "Foreground 4", pb);
      if (fiFull != null) Progress(new GameDefinition(opt.Game, Operation.Full), fiFull, "Full", pb);
    }
    // Background
    public Runner(BackgroundOptions opt) {
      FileInfo[] fiBackground = Helper.GetImageFiles(opt.Target);
      if (fiBackground.Length == 0) {
        Error(Res.Error_Target_NoFile);
        return;
      } else Progress(new GameDefinition(opt.Game, Operation.Background), fiBackground, "Background");
    }
    // Center
    public Runner(CenterOptions opt) {
      FileInfo[] fiCenter = Helper.GetImageFiles(opt.Target);
      if (fiCenter.Length == 0) {
        Error(Res.Error_Target_NoFile);
        return;
      } else Progress(new GameDefinition(opt.Game, Operation.Center), fiCenter, "Center");
    }
    // CreateDirectory
    public Runner(CreateDirectoryOptions opt) {
      DirectoryInfo diBackground = new DirectoryInfo(Path.Combine(opt.Target, "IC-Background"));
      DirectoryInfo diCenter = new DirectoryInfo(Path.Combine(opt.Target, "IC-Center"));
      DirectoryInfo diForeground0 = new DirectoryInfo(Path.Combine(opt.Target, "IC-Foreground-0"));
      DirectoryInfo diForeground1 = new DirectoryInfo(Path.Combine(opt.Target, "IC-Foreground-1"));
      DirectoryInfo diForeground2 = new DirectoryInfo(Path.Combine(opt.Target, "IC-Foreground-2"));
      DirectoryInfo diForeground3 = new DirectoryInfo(Path.Combine(opt.Target, "IC-Foreground-3"));
      DirectoryInfo diForeground4 = new DirectoryInfo(Path.Combine(opt.Target, "IC-Foreground-4"));
      DirectoryInfo diFull = new DirectoryInfo(Path.Combine(opt.Target, "IC-Full"));

      try {
        diBackground.Create();
        diCenter.Create();
        diForeground0.Create();
        diForeground1.Create();
        diForeground2.Create();
        diForeground3.Create();
        diForeground4.Create();
        diFull.Create();
      } catch (IOException e) {
        Error(e.Message);
        Console.WriteLine(e.StackTrace);
      }
    }
    // Foreground
    public Runner(ForegroundOptions opt) {
      FileInfo[] fiForeground = Helper.GetImageFiles(opt.Target);
      if (fiForeground.Length == 0) {
        Error(Res.Error_Target_NoFile);
        return;
      } else {
        if (opt.ChatCount == 0) Progress(new GameDefinition(opt.Game, Operation.Foreground0), fiForeground, "Foreground 0");
        else if (opt.ChatCount == 1) Progress(new GameDefinition(opt.Game, Operation.Foreground1), fiForeground, "Foreground 1");
        else if (opt.ChatCount == 2) Progress(new GameDefinition(opt.Game, Operation.Foreground2), fiForeground, "Foreground 2");
        else if (opt.ChatCount == 3) Progress(new GameDefinition(opt.Game, Operation.Foreground3), fiForeground, "Foreground 3");
        else if (opt.ChatCount == 4) Progress(new GameDefinition(opt.Game, Operation.Foreground4), fiForeground, "Foreground 4");
      }
    }
    // Full
    public Runner(FullOptions opt) {
      FileInfo[] fiFull = Helper.GetImageFiles(opt.Target);
      if (fiFull.Length == 0) {
        Error(Res.Error_Target_NoFile);
        return;
      } else Progress(new GameDefinition(opt.Game, Operation.Full), fiFull, "Full");
    }
    // Runner
    public void Progress(GameDefinition gd, FileInfo[] targets, string header, ProgressBar pb = null) {
      bool showParent = false;
      if (pb == null) showParent = true;
      pb = pb ?? Helper.NewProgressBar(targets.Length);
      foreach (FileInfo fi in targets) {
        string name = showParent ? fi.Directory.Name + "/" + fi.Name : fi.Name;
        pb.WriteLine($"[{header}] {name}");
        pb.SetProcessingText($"[{header}] {name}");
        External.ProcessImage(fi, gd);
        pb.PerformStep();
      }
      Directory.Delete(Path.Combine(targets[0].Directory.FullName, "_pre_webp"), true);
    }
  }
}
