using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace FindInFiles {
  internal static class Program {

    [STAThread]
    private static void Main() {
      Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
      Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
