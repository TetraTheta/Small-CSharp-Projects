using ConvertWebP.Libraries;
using ConvertWebP.Properties;
using DarkModeForms;
using System.Globalization;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertWebP {
  public partial class ProgressForm : Form {
    private int completedTasks = 0;
    private readonly int totalTasks;

    public ProgressForm(string[] files, int targetWidth, string inputPath, string outputPath) {
      InitializeComponent();
      Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
      Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
      _ = new DarkModeCS(this);
      Show();

      totalTasks = files.Length;

      labelPath.Text = Resources.ProgressFormLabelPathPrefix + inputPath;

      Task.Run(async () => {
        for (int idx = 0; idx < files.Length; idx++) {
          string file = files[idx];
          await ProcessFileAsync(file, idx, files.Length, targetWidth, outputPath);
        }

        if (completedTasks == totalTasks) {
          SystemSounds.Asterisk.Play();
          Close();
        }
      });
    }

    private async Task ProcessFileAsync(string file, int idx, int totalFiles, int targetWidth, string outputPath) {
      Text = $"[{idx + 1:D3}/{totalFiles:D3}] {Path.GetFileName(file)}";
      progressBar.Value = (int)((double)(idx + 1) / totalFiles * 100);
      progressBar.Refresh();
      labelCurrent.Text = $"[{idx + 1:D3}/{totalFiles:D3}] {Path.GetFileName(file)}";
      listBoxHistory.Items.Add(Path.GetFileName(file));
      listBoxHistory.SelectedIndex = listBoxHistory.Items.Count - 1;

      if (await Task.Run(() => RunPrograms.RunCwebp(file, targetWidth, outputPath)) != 0) {
        Dialogs.ShowError(Resources.ErrFileProcessFailed + "\n" + file);
      }

      Interlocked.Increment(ref completedTasks);
    }
  }
}
