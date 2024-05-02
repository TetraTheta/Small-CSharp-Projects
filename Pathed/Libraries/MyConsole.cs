using System;

namespace pathed.Libraries {
  public static class MyConsole {
    // TODO: Implement sending string to named pipe for later use

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
