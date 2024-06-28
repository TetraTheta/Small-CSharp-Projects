namespace ImageConverter.Libraries {
  /// <summary>
  /// I'm doing this because CommandLineParser won't let me use Properties.
  /// </summary>
  public static class Res {

    // Verb
    public const string Help_Verb_All = "Process 'background', 'center', 'foreground'(0~4), 'full' jobs at once.\r\nThis requires these corresponding directories:\r\n- IC-Background\r\n- IC-Center\r\n- IC-Foreground-0\r\n- IC-Foreground-1\r\n- IC-Foreground-2\r\n- IC-Foreground-3\r\n- IC-Foreground-4\r\n- IC-Full";

    public const string Help_Verb_Background = "Process images as 'background'.";
    public const string Help_Verb_Center = "Process images as 'center'.";
    public const string Help_Verb_CreateDirectory = "Create directories for 'all'.\r\nThis will create these subdirectories:\r\n- IC-Background\r\n- IC-Center\r\n- IC-Foreground-0\r\n- IC-Foreground-1\r\n- IC-Foreground-2\r\n- IC-Foreground-3\r\n- IC-Foreground-4\r\n- IC-Full";
    public const string Help_Verb_Foreground = "Process images as 'foreground'.";
    public const string Help_Verb_Full = "Process images as 'full'.";

    // Value
    public const string Help_Value_Target = "Target directory";

    // Option
    public const string Help_Option_ChatCount = "Number of chat options in the screenshot. (0 ~ 4)";

    public const string Help_Option_Game = "Game that the screenshot is taken from.\r\nMust be either 'tof' or 'ww'.";

    // Validity Check
    public const string Error_All_NoDir = "'all' subcommand requires at least one of these directories:\r\n- IC-Background\r\n- IC-Center\r\n- IC-Foreground-0\r\n- IC-Foreground-1\r\n- IC-Foreground-2\r\n- IC-Foreground-3\r\n- IC-Foreground-4\r\n- IC-Full";

    public const string Error_Fore_Over = "'chat' option's value must be between 0 to 4 (inclusive).";
    public const string Error_NoDep_Cwebp = "'cwebp.exe' is missing. It is required for converting image to WebP format.";
    public const string Error_NoDep_FFmpeg = "'ffmpeg.exe' is missing. It is required for manipulating image.";
    public const string Error_NoDep_Magick = "'magick.exe' is missing. It is required for getting width of the image.";
    public const string Error_Target_NoFile = "There is no file to process.";
    public const string Error_Target_NonDir = "Given 'target' is not a directory: ";
  }
}
