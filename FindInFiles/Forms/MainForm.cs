using DarkModeForms;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace FindInFiles {
  public partial class MainForm : Form {

    public MainForm() {
      InitializeComponent();
      Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
      Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
      _ = new DarkModeCS(this);
      Show();
    }
  }
}
