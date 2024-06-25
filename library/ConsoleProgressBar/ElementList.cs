using System.Collections.Generic;

namespace ConsoleProgressBar {
  /// <summary>
  /// A list of Elements of a ProgressBar
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ElementList<T> {

    /// <summary>
    /// The List of Elements of a Progressbar
    /// </summary>
    public List<Element<T>> List { get; } = new List<Element<T>>();

    /// <summary>
    /// Clears the List of elements of a ProgressBar
    /// </summary>
    /// <returns></returns>
    public ElementList<T> Clear() {
      List.Clear();
      return this;
    }

    /// <summary>
    /// Adds new Element to the List
    /// </summary>
    /// <returns></returns>
    public Element<T> AddNew() {
      var line = new Element<T>();
      List.Add(line);
      return line;
    }
  }
}
