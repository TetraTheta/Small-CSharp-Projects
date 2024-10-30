using System;
using System.Collections.Generic;

namespace ConsoleProgressBar.Extensions {
  /// <summary>
  /// Extensions for Element
  /// </summary>
  public static class ElementExtensions {
    /// <summary>
    /// Returns a list of Actions to write the element in Console
    /// </summary>
    /// <param name="element"></param>
    /// <param name="progressBar"></param>
    /// <param name="valueTransformer">Function to Transform the value before write</param>
    /// <returns></returns>
    public static List<Action> GetRenderActions(this Element<string> element, ProgressBar progressBar, Func<string, string> valueTransformer = null) {
      var list = new List<Action>();

      if (progressBar == null || element == null || !element.GetVisible(progressBar)) return list;

      var foregroundColor = element.GetForegroundColor(progressBar);
      if (foregroundColor.HasValue) list.Add(() => Console.ForegroundColor = foregroundColor.Value);

      var backgroundColor = element.GetBackgroundColor(progressBar);
      if (backgroundColor.HasValue) list.Add(() => Console.BackgroundColor = backgroundColor.Value);

      string value = element.GetValue(progressBar);
      if (valueTransformer != null) value = valueTransformer.Invoke(value);

      list.Add(() => Console.Write(value));
      list.Add(() => Console.ResetColor());

      return list;
    }

    /// <summary>
    /// Returns a list of Actions to write the element in Console
    /// </summary>
    /// <param name="element"></param>
    /// <param name="progressBar"></param>
    /// <param name="repetition"></param>
    /// <returns></returns>
    public static List<Action> GetRenderActions(this Element<char> element, ProgressBar progressBar, int repetition = 1) {
      var list = new List<Action>();

      if (progressBar == null || repetition < 1 || element == null || !element.GetVisible(progressBar)) return list;

      var foregroundColor = element.GetForegroundColor(progressBar);
      if (foregroundColor.HasValue) list.Add(() => Console.ForegroundColor = foregroundColor.Value);

      var backgroundColor = element.GetBackgroundColor(progressBar);
      if (backgroundColor.HasValue) list.Add(() => Console.BackgroundColor = backgroundColor.Value);

      char value = element.GetValue(progressBar);
      list.Add(() => Console.Write(new string(value, repetition)));
      list.Add(() => Console.ResetColor());

      return list;
    }
  }
}
