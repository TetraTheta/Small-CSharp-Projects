using CommandLine;
using System;
using System.IO;
using System.Text;
using static MyConsole.MyConsole;

namespace ImageConverter.Libraries {
  public class BaseOptions {

    [Value(0, MetaName = "PATH", HelpText = Res.Help_Value_Target)]
    public string Target { get; set; }

    // I'm doing this because CommandLineParser won't let me use this as default value in attribute.
    public BaseOptions() {
      Target = Directory.GetCurrentDirectory();
    }

    internal string _type = string.Empty;

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }

  public class GameOptions : BaseOptions {

    [Option('g', "game", MetaValue = "GAME", Required = true, HelpText = Res.Help_Option_Game)]
    public Game Game { get; set; }

    public GameOptions() : base() {
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Game: {Game}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }

  [Verb("all", HelpText = Res.Help_Verb_All)]
  public class AllOptions : GameOptions {
    internal new string _type = "All";
    public AllOptions() : base() {
    }
  }

  [Verb("background", HelpText = Res.Help_Verb_Background)]
  public class BackgroundOptions : GameOptions {
    internal new string _type = "Background";
    public BackgroundOptions() : base() {
    }
  }

  [Verb("center", HelpText = Res.Help_Verb_Center)]
  public class CenterOptions : GameOptions {
    internal new string _type = "Center";
    public CenterOptions() : base() {
    }
  }

  [Verb("create-directory", HelpText = Res.Help_Verb_CreateDirectory)]
  public class CreateDirectoryOptions : BaseOptions {
    internal new string _type = "CreateDirectory";
    public CreateDirectoryOptions() : base() {
    }
  }

  [Verb("foreground", HelpText = Res.Help_Verb_Foreground)]
  public class ForegroundOptions : GameOptions {

    [Option('c', "chat", MetaValue = "ChatOptionCount", Required = true, HelpText = "")]
    public ushort ChatCount { get; set; }

    internal new string _type = "Foreground";

    public ForegroundOptions(GameOptions go, ushort chatCount) : base() {
      Target = go.Target;
      Game = go.Game;
      if (chatCount >= 0 && chatCount <= 4) {
        ChatCount = chatCount;
      } else {
        Error(Res.Error_Fore_Over);
        Environment.Exit(1);
      }
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Game: {Game}");
      sb.AppendLine($"ChatCount: {ChatCount}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }

  [Verb("full", HelpText = Res.Help_Verb_Full)]
  public class FullOptions : GameOptions {
    internal new string _type = "Full";
    public FullOptions() : base() {
    }
  }
}
