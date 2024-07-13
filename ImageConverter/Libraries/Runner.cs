using ConsoleProgressBar;
using System;
using System.Globalization;
using System.IO;
using System.Media;
using static MyConsole.MyConsole;

namespace ImageConverter.Libraries {
  public class Runner {
    // All
    public Runner(AllOptions opt) {
      FileInfo[] fiBG = Helper.GetImageFiles(Dirs.BG(opt.Target));
      FileInfo[] fiC = Helper.GetImageFiles(Dirs.C(opt.Target));
      FileInfo[] fiFG0 = Helper.GetImageFiles(Dirs.FG0(opt.Target));
      FileInfo[] fiFG1 = Helper.GetImageFiles(Dirs.FG1(opt.Target));
      FileInfo[] fiFG2 = Helper.GetImageFiles(Dirs.FG2(opt.Target));
      FileInfo[] fiFG3 = Helper.GetImageFiles(Dirs.FG3(opt.Target));
      FileInfo[] fiFG4 = Helper.GetImageFiles(Dirs.FG4(opt.Target));
      FileInfo[] fiF = Helper.GetImageFiles(Dirs.F(opt.Target));

      fiBG = CheckLength(fiBG);
      fiC = CheckLength(fiC);
      fiFG0 = CheckLength(fiFG0);
      fiFG1 = CheckLength(fiFG1);
      fiFG2 = CheckLength(fiFG2);
      fiFG3 = CheckLength(fiFG3);
      fiFG4 = CheckLength(fiFG4);
      fiF = CheckLength(fiF);

      FileInfo[] fiAll = CheckLength(new FileInfo[][] { fiBG, fiC, fiFG0, fiFG1, fiFG2, fiFG3, fiFG4, fiF });

      ProgressBar pb = Helper.NewProgressBar(fiAll.Length);

      if (fiBG != null) Progress(new GameDefinition(opt.Game, Op.Background), fiBG, "BG", pb);
      if (fiC != null) Progress(new GameDefinition(opt.Game, Op.Center), fiC, "C", pb);
      if (fiFG0 != null) Progress(new GameDefinition(opt.Game, Op.Foreground0), fiFG0, "FG 0", pb);
      if (fiFG1 != null) Progress(new GameDefinition(opt.Game, Op.Foreground1), fiFG1, "FG 1", pb);
      if (fiFG2 != null) Progress(new GameDefinition(opt.Game, Op.Foreground2), fiFG2, "FG 2", pb);
      if (fiFG3 != null) Progress(new GameDefinition(opt.Game, Op.Foreground3), fiFG3, "FG 3", pb);
      if (fiFG4 != null) Progress(new GameDefinition(opt.Game, Op.Foreground4), fiFG4, "FG 4", pb);
      if (fiF != null) Progress(new GameDefinition(opt.Game, Op.Full), fiF, "F", pb);

      FileInfo[] fiBGC = Helper.GetImageFiles(Dirs.BGC(opt.Target));
      FileInfo[] fiCC = Helper.GetImageFiles(Dirs.CC(opt.Target));
      FileInfo[] fiFG0C = Helper.GetImageFiles(Dirs.FG0C(opt.Target));
      FileInfo[] fiFG1C = Helper.GetImageFiles(Dirs.FG1C(opt.Target));
      FileInfo[] fiFG2C = Helper.GetImageFiles(Dirs.FG2C(opt.Target));
      FileInfo[] fiFG3C = Helper.GetImageFiles(Dirs.FG3C(opt.Target));
      FileInfo[] fiFG4C = Helper.GetImageFiles(Dirs.FG4C(opt.Target));
      FileInfo[] fiFC = Helper.GetImageFiles(Dirs.FC(opt.Target));

      fiBGC = CheckLength(fiBGC);
      fiCC = CheckLength(fiCC);
      fiFG0C = CheckLength(fiFG0C);
      fiFG1C = CheckLength(fiFG1C);
      fiFG2C = CheckLength(fiFG2C);
      fiFG3C = CheckLength(fiFG3C);
      fiFG4C = CheckLength(fiFG4C);
      fiFC = CheckLength(fiFC);

      FileInfo[] fiAllC = CheckLength(new FileInfo[][] { fiBGC, fiCC, fiFG0C, fiFG1C, fiFG2C, fiFG3C, fiFG4C, fiFC });

      foreach (FileInfo fi in fiAllC) {
        if (fi.Extension.Equals(".webp", StringComparison.OrdinalIgnoreCase)) {
          try {
            string destPath = Path.Combine(opt.Target, fi.Name);
            if (File.Exists(destPath)) {
              Error($"File already exists: {destPath}");
              continue;
            }
            fi.MoveTo(destPath);
          } catch (IOException e) {
            Console.WriteLine($"IOException: {e.Message} while moving file: {fi.FullName}");
          } catch (Exception e) {
            Console.WriteLine($"Exception: {e.Message} while moving file: {fi.FullName}");
          }
        }
      }

      pb.SetValue(fiAll.Length);
      SystemSounds.Asterisk.Play();
      Info("All jobs complete");
    }
    // Background
    public Runner(BackgroundOptions opt) {
      FileInfo[] fiBG = Helper.GetImageFiles(opt.Target);
      if (fiBG.Length == 0) {
        Error(Res.Error_Target_NoFile);
        return;
      } else {
        Progress(new GameDefinition(opt.Game, Op.Background), fiBG, "BG");
        SystemSounds.Asterisk.Play();
        Info("Background job complete");
      }
    }
    // Center
    public Runner(CenterOptions opt) {
      FileInfo[] fiC = Helper.GetImageFiles(opt.Target);
      if (fiC.Length == 0) {
        Error(Res.Error_Target_NoFile);
        return;
      } else {
        Progress(new GameDefinition(opt.Game, Op.Center), fiC, "C");
        SystemSounds.Asterisk.Play();
        Info("Center job complete");
      }
    }
    // CreateDirectory
    public Runner(CreateDirectoryOptions opt) {
      DirectoryInfo diBG = new DirectoryInfo(Dirs.BG(opt.Target));
      DirectoryInfo diC = new DirectoryInfo(Dirs.C(opt.Target));
      DirectoryInfo diFG0 = new DirectoryInfo(Dirs.FG0(opt.Target));
      DirectoryInfo diFG1 = new DirectoryInfo(Dirs.FG1(opt.Target));
      DirectoryInfo diFG2 = new DirectoryInfo(Dirs.FG2(opt.Target));
      DirectoryInfo diFG3 = new DirectoryInfo(Dirs.FG3(opt.Target));
      DirectoryInfo diFG4 = new DirectoryInfo(Dirs.FG4(opt.Target));
      DirectoryInfo diF = new DirectoryInfo(Dirs.F(opt.Target));

      try {
        diBG.Create();
        diC.Create();
        diFG0.Create();
        diFG1.Create();
        diFG2.Create();
        diFG3.Create();
        diFG3.Create();
        diFG4.Create();
        diF.Create();
        SystemSounds.Asterisk.Play();
        Info("CreateDirectory job complete");
      } catch (IOException e) {
        Error(e.Message);
        Console.WriteLine(e.StackTrace);
      }
    }
    // Foreground
    public Runner(ForegroundOptions opt) {
      FileInfo[] fiFG = Helper.GetImageFiles(opt.Target);
      if (fiFG.Length == 0) {
        Error(Res.Error_Target_NoFile);
        return;
      } else {
        if (opt.ChatCount == 0) Progress(new GameDefinition(opt.Game, Op.Foreground0), fiFG, "FG 0");
        else if (opt.ChatCount == 1) Progress(new GameDefinition(opt.Game, Op.Foreground1), fiFG, "FG 1");
        else if (opt.ChatCount == 2) Progress(new GameDefinition(opt.Game, Op.Foreground2), fiFG, "FG 2");
        else if (opt.ChatCount == 3) Progress(new GameDefinition(opt.Game, Op.Foreground3), fiFG, "FG 3");
        else if (opt.ChatCount == 4) Progress(new GameDefinition(opt.Game, Op.Foreground4), fiFG, "FG 4");
        Info("Foreground job complete");
      }
    }
    // Full
    public Runner(FullOptions opt) {
      FileInfo[] fiF = Helper.GetImageFiles(opt.Target);
      if (fiF.Length == 0) {
        Error(Res.Error_Target_NoFile);
        return;
      } else {
        Progress(new GameDefinition(opt.Game, Op.Full), fiF, "F");
        SystemSounds.Asterisk.Play();
        Info("Full job complete");
      }
    }
    // Runner
    public void Progress(GameDefinition gd, FileInfo[] targets, string header, ProgressBar pb = null) {
      bool showParent = false;
      if (pb != null) showParent = true;
      pb = pb ?? Helper.NewProgressBar(targets.Length);
      foreach (FileInfo fi in targets) {
        string name = showParent ? (fi.Directory.Name + "/" + fi.Name).Trim() : fi.Name.Trim();
        string title = showParent ? $"ImageConverter [{Shrink(fi.Directory.Parent.Name)}] [{pb.Value + 1}/{pb.Maximum}] {name}" : $"ImageConverter [{Shrink(fi.Directory.Name)}] [{pb.Value + 1}/{pb.Maximum}] {name}";
        string progress = $"[{pb.Value + 1}/{pb.Maximum}] [{header}] {name}";
        string log = showParent ? $"[{pb.Value + 1}/{pb.Maximum}] [{Shrink(fi.Directory.Parent.Name)}] [{header}] {name}" : $"[{pb.Value + 1}/{pb.Maximum}] [{Shrink(fi.Directory.Name)}] [{header}] {name}";

        Console.Title = title;
        pb.WriteLine(log);
        pb.SetProcessingText(progress);
        External.ProcessImage(fi, gd);
        pb.PerformStep();
      }
      if (!showParent) pb.SetValue(targets.Length);
      Directory.Delete(Path.Combine(targets[0].Directory.FullName, "_pre_webp"), true);
      Console.Title = Program.OriginalTitle;
    }
    // Helper
    private FileInfo[] CheckLength(FileInfo[] fi) {
      return fi.Length > 0 ? fi : null;
    }
    private FileInfo[] CheckLength(FileInfo[][] fis) {
      FileInfo[] fia = new FileInfo[0];
      foreach (FileInfo[] fi in fis) {
        if (fi != null && fi.Length > 0) fia = Helper.ConcatArrays(fia, fi);
      }
      return fia.Length > 0 ? fia : null;
    }
    private string Shrink(string text) {
      StringInfo info = new StringInfo(text);
      int length = info.LengthInTextElements;

      if (length > 15) {
        string first = info.SubstringByTextElements(0, 10);
        string last = info.SubstringByTextElements(length - 4, 4);
        return first + "…" + last;
      } else {
        return text;
      }
    }
  }
}
