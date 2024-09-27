using System;

namespace CLIParser.Attributes {
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class FlagAttribute : Attribute {
    public char ShortName { get; }
    public string LongName { get; }
    public string HelpText { get; set; }
    public string MetaValue { get; set; }
    public bool Mandatory { get; set; }

    public FlagAttribute(char shortName, string longName) {
      ShortName = shortName;
      LongName = longName;
    }
  }

  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class SwitchAttribute : Attribute {
    public char ShortName { get; }
    public string LongName { get; }
    public string HelpText { get; set; }
    public string MetaValue { get; set; }
    public bool Mandatory { get; set; }

    public SwitchAttribute(char shortName, string longName) {
      ShortName = shortName;
      LongName = longName;
    }
  }

  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class PositionAttribute : Attribute {
    public int Index { get; }
    public string HelpText { get; set; }
    public string MetaValue { get; set; }
    public bool Mandatory { get; set; }

    public PositionAttribute(int index) {
      Index = index;
    }
  }
}
