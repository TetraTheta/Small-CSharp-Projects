using CLIParser.Attributes;
using System;
using System.Reflection;

namespace CLIParser {
  public static class Parser {
    public static T Parse<T>(string[] args) where T : new() {
      T instance = new T();
      var properties = typeof(T).GetProperties();

      for (int i = 0; i < args.Length; i++) {
        string arg = args[i];

        foreach (var prop in properties) {
          var flagAttr = prop.GetCustomAttribute<FlagAttribute>();
          if (flagAttr != null && (arg == $"-{flagAttr.ShortName}" || arg == $"--{flagAttr.LongName}")) {
            if (prop.PropertyType == typeof(bool)) {
              prop.SetValue(instance, true);
            } else if (i + 1 < args.Length) {
              prop.SetValue(instance, Convert.ChangeType(args[i + 1], prop.PropertyType));
              i++;
            }
          }

          var switchAttr = prop.GetCustomAttribute<SwitchAttribute>();
          if (switchAttr != null && (arg == $"-{switchAttr.ShortName}" || arg == $"--{switchAttr.LongName}")) {
            prop.SetValue(instance, true);
          }

          var posAttr = prop.GetCustomAttribute<PositionAttribute>();
          if (posAttr != null && int.TryParse(arg, out int pos) && pos == posAttr.Index) {
            prop.SetValue(instance, Convert.ChangeType(args[i + 1], prop.PropertyType));
            i++;
          }
        }
      }

      foreach (var prop in properties) {
        var flagAttr = prop.GetCustomAttribute<FlagAttribute>();
        if (flagAttr != null && flagAttr.Mandatory && prop.GetValue(instance) == null) {
          throw new ArgumentException($"The argument --{flagAttr.LongName} is mandatory.");
        }
      }

      return instance;
    }
  }
}
