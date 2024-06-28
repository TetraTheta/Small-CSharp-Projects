using System;

namespace MyConsole {
  public static class MyConsole {
    public static void Error(string text, string header = "ERROR", ConsoleColor color = ConsoleColor.Red) {
      Write(header + " ", color);
      WriteLine(text);
    }

    public static void Info(string text, string header = "INFO", ConsoleColor color = ConsoleColor.Green) {
      Write(header + " ", color);
      WriteLine(text);
    }

    public static void Write(string text) {
      Write(text, Console.ForegroundColor);
    }

    public static void Write(string text, ConsoleColor color) {
      ConsoleColor originalColor = Console.ForegroundColor;
      Console.ForegroundColor = color;
      Console.Write(text);
      Console.ForegroundColor = originalColor;
    }

    public static void WriteLine(string text) {
      WriteLine(text, Console.ForegroundColor);
    }

    public static void WriteLine(string text, ConsoleColor color) {
      ConsoleColor originalColor = Console.ForegroundColor;
      Console.ForegroundColor = color;
      Console.WriteLine(text);
      Console.ForegroundColor = originalColor;
    }

    public static void WriteError(string text) {
      WriteError(text, ConsoleColor.Red);
    }

    public static void WriteError(string text, ConsoleColor color) {
      ConsoleColor originalColor = Console.ForegroundColor;
      Console.ForegroundColor = color;
      Console.Error.Write(text);
      Console.ForegroundColor = originalColor;
    }

    public static void WriteLineError(string text) {
      WriteLineError(text, ConsoleColor.Red);
    }

    public static void WriteLineError(string text, ConsoleColor color) {
      ConsoleColor originalColor = Console.ForegroundColor;
      Console.ForegroundColor = color;
      Console.Error.WriteLine(text);
      Console.ForegroundColor = originalColor;
    }
  }
}
