using CommandLine;
using System.IO;
using System.Text;

namespace ImageConverter.Libraries {
  public interface IOptions {
    string Target { get; set; }
    string ToString();
  }
  public interface IGameOptions : IOptions {
    Game Game { get; set; }
  }

  [Verb("all", aliases: new[] { "a" }, HelpText = Res.Help_Verb_All)]
  public class AllOptions : IGameOptions {
    internal string _type = "All";

    [Value(0, MetaName = "PATH", Required = false, HelpText = Res.Help_Value_Target)]
    public string Target { get; set; } = Directory.GetCurrentDirectory();

    [Option('g', "game", MetaValue = "GAME", Required = true, HelpText = Res.Help_Option_Game)]
    public Game Game { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Game: {Game}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }

  [Verb("background", aliases: new[] { "bg" }, HelpText = Res.Help_Verb_Background)]
  public class BackgroundOptions : IGameOptions {
    internal string _type = "Background";

    [Value(0, MetaName = "PATH", Required = false, HelpText = Res.Help_Value_Target)]
    public string Target { get; set; } = Directory.GetCurrentDirectory();

    [Option('g', "game", MetaValue = "GAME", Required = true, HelpText = Res.Help_Option_Game)]
    public Game Game { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Game: {Game}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }

  [Verb("center", aliases: new[] { "c" }, HelpText = Res.Help_Verb_Center)]
  public class CenterOptions : IGameOptions {
    internal string _type = "Center";

    [Value(0, MetaName = "PATH", Required = false, HelpText = Res.Help_Value_Target)]
    public string Target { get; set; } = Directory.GetCurrentDirectory();

    [Option('g', "game", MetaValue = "GAME", Required = true, HelpText = Res.Help_Option_Game)]
    public Game Game { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Game: {Game}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }

  [Verb("create-directory", aliases: new[] { "cd" }, HelpText = Res.Help_Verb_CreateDirectory)]
  public class CreateDirectoryOptions : IOptions {
    internal string _type = "CreateDirectory";

    [Value(0, MetaName = "PATH", Required = false, HelpText = Res.Help_Value_Target)]
    public string Target { get; set; } = Directory.GetCurrentDirectory();

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }

  [Verb("foreground", aliases: new[] { "fg" }, HelpText = Res.Help_Verb_Foreground)]
  public class ForegroundOptions : IGameOptions {
    internal string _type = "Foreground";

    [Value(0, MetaName = "PATH", Required = false, HelpText = Res.Help_Value_Target)]
    public string Target { get; set; } = Directory.GetCurrentDirectory();

    [Option('g', "game", MetaValue = "GAME", Required = true, HelpText = Res.Help_Option_Game)]
    public Game Game { get; set; }

    [Option('c', "chat", MetaValue = "ChatOptionCount", Required = true, HelpText = Res.Help_Option_ChatCount)]
    public ushort ChatCount { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Game: {Game}");
      sb.AppendLine($"ChatCount: {ChatCount}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }

  [Verb("full", aliases: new[] { "f" }, HelpText = Res.Help_Verb_Full)]
  public class FullOptions : IGameOptions {
    internal string _type = "Full";

    [Value(0, MetaName = "PATH", Required = false, HelpText = Res.Help_Value_Target)]
    public string Target { get; set; } = Directory.GetCurrentDirectory();

    [Option('g', "game", MetaValue = "GAME", Required = true, HelpText = Res.Help_Option_Game)]
    public Game Game { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Subcommand: {_type}");
      sb.AppendLine($"Target: {Target}");
      return sb.ToString();
    }
  }
}
