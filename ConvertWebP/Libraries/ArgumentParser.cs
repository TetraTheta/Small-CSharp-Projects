using ConvertWebP.Properties;
using System;
using System.IO;

namespace ConvertWebP.Libraries {
  public class ArgumentParser {
    public static Options Parse(string[] args) {
      int width = 1280;
      string path = Directory.GetCurrentDirectory();

      if (args.Length > 4) {
        Dialogs.ShowError(Resources.ErrTooManyArgs);
        return null;
      }

      for (int i = 0; i < args.Length; i++) {
        string arg = args[i].ToLower();
        if (arg == "-h" || arg == "--help") {
          // Show help message
          Dialogs.ShowInfo(Resources.HelpContent, Resources.HelpTitle, true, 0);
          return null;
        } else if (arg == "-v" || arg == "--version") {
          // Show version message
          Dialogs.ShowInfo(Resources.VersionContent, Resources.VersionTitle, true, 0);
          return null;
        } else if (arg == "-w" || arg == "--width") {
          try {
            width = int.Parse(args[i + 1]);
            if (width <= 0) {
              width = 1280;
            }
          } catch (ArgumentException) {
            // next argument is null
            Dialogs.ShowError(Resources.ErrArgNoValue + "\n" + args[i]);
            return null;
          } catch (OverflowException) {
            // next argument over/underflows or is not integer
            Dialogs.ShowError(Resources.ErrInvalidValue + "\n" + args[i + 1]);
            return null;
          } catch (IndexOutOfRangeException) {
            // next argument doesn't exist
            Dialogs.ShowError(Resources.ErrArgNoValue + "\n" + args[i]);
            return null;
          } catch (FormatException) {
            // next argument isn't number
            Dialogs.ShowError(Resources.ErrNotPositiveInt + "\n" + args[i + 1]);
            return null;
          } catch (Exception e) {
            // other exceptions
            Dialogs.ShowError(e);
            return null;
          }
        } else if (arg == "-p" || arg == "--path") {
          try {
            if (File.Exists(args[i + 1]) || Directory.Exists(args[i + 1])) {
              path = args[i + 1];
            } else {
              Dialogs.ShowError(Resources.ErrInvalidPath + "\n" + args[i + 1]);
              return null;
            }
          } catch (IndexOutOfRangeException) {
            // next argument doesn't exist
            Dialogs.ShowError(Resources.ErrArgNoValue + "\n" + args[i]);
            return null;
          } catch (Exception e) {
            // other exception
            Dialogs.ShowError(e);
            return null;
          }
        }
      }
      return new Options(path, width);
    }
  }
}
