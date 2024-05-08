using ConvertWebP.Properties;
using DarkModeForms;
using System;
using System.Media;

namespace ConvertWebP.Libraries {

  public class Dialogs {
    public static void ShowError(Exception ex, bool doExit = true) {
      SystemSounds.Exclamation.Play();
      Messenger.MessageBox(ex);
      if (doExit) Environment.Exit(1);
    }

    public static void ShowError(string content, bool doExit = true) {
      SystemSounds.Exclamation.Play();
      Messenger.MessageBox(content, Resources.ErrTitle, MsgIcon.Error, System.Windows.Forms.MessageBoxButtons.OK);
      if (doExit) Environment.Exit(1);
    }

    public static void ShowInfo(string content, string title, bool doExit = true, int exitCode = 1) {
      if (string.IsNullOrEmpty(title)) title = Resources.InfoTitle;
      SystemSounds.Exclamation.Play();
      Messenger.MessageBox(content, title, MsgIcon.Info, System.Windows.Forms.MessageBoxButtons.OK);
      if (doExit) Environment.Exit(exitCode);
    }
  }
}
