using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SortPATH {

  internal class Program {

    private static string Normalized(string path) {
      return Path.HasExtension(path) || (path.Length > 1 && path[path.Length - 1] == '\\') ? Path.GetFullPath(path.TrimEnd('\\')) : path;
    }

    private static void Main() {
      // Get current user's %PATH% environment variable
      string pathEnv = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
      if (pathEnv == null) {
        Console.WriteLine("%PATH% environment variable is not set.");
        return;
      }

      // This is special path
      string cce = Normalized(@"C:\CustomExecutables");

      // Prepare 'uniquePaths' for storing distinct paths
      HashSet<string> uniquePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

      // Process paths
      foreach (string path in pathEnv.Split(';')) {
        string expanded = Normalized(Environment.ExpandEnvironmentVariables(path));
        if (expanded.Equals(cce, StringComparison.OrdinalIgnoreCase)) continue;
        if (Directory.Exists(expanded)) {
          uniquePaths.Add(expanded);
        }
      }

      // Sort paths
      var sortedPaths = uniquePaths.OrderBy(p => p).ToList();

      // Insert 'C:\CustomExecutables' at the front of PATH
      sortedPaths.Insert(0, cce);

      // Set %PATH%
      string newPathEnv = string.Join(";", sortedPaths);
      try {
        Environment.SetEnvironmentVariable("PATH", newPathEnv, EnvironmentVariableTarget.User);
        Console.WriteLine("%PATH% is now sorted.");
      } catch (Exception e) {
        Console.WriteLine($"Failed to set %PATH%.\nError: {e}");
      }
    }
  }
}
