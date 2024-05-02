using CommandLine;
using System;
using System.Text;

namespace pathed.Libraries {

  [Verb("append", HelpText = "Append variable to environment variable")]
  public class AppendOptions {

    [Value(0, MetaName = "VAR", HelpText = "New variable to add to the end of the environment variable", Required = true)]
    public string Value { get; set; }

    [Value(1, MetaName = "ENV", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option('t', "target", Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    // This is for named pipe
    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Value: {Value}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }
  }

  [Verb("prepend", HelpText = "Prepend variable to environment variable")]
  public class PrependOptions {

    [Value(0, MetaName = "VAR", HelpText = "New variable to add to the front of the environment variable", Required = true)]
    public string Value { get; set; }

    [Value(1, MetaName = "ENV", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option(Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Value: {Value}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }
  }

  [Verb("remove", HelpText = "Remove variable from environment variable")]
  public class RemoveOptions {

    [Value(0, MetaName = "VAR", HelpText = "Variable to remove from the environment variable", Required = true)]
    public string Value { get; set; }

    [Value(1, MetaName = "ENV", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option(Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Value: {Value}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }
  }

  [Verb("show", isDefault: true, HelpText = "Print environment variable as a list")]
  public class ShowOptions {

    [Value(0, MetaName = "ENV", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option(HelpText = "", Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }
  }

  [Verb("slim", HelpText = "Remove duplicated or non-exeistent variable from environment variable")]
  public class SlimOptions {

    [Value(0, MetaName = "ENV", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option(Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }
  }

  [Verb("sort", HelpText = "Sort environment variable alphabetically")]
  public class SortOptions {

    [Value(0, MetaName = "ENV", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option(Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }
  }
}
