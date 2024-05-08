using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace pathed.Libraries {
  public class EnvPath {
    private readonly string key;
    private readonly string target;
    private string[] paths;

    public EnvPath(string key, EnvironmentVariableTarget target) {
      this.key = key;
      this.target = target.ToString();
      paths = Environment.GetEnvironmentVariable(key, target).Split(';');
    }

    public void Append(string value) {
      string realValue;
      try {
        realValue = GetFinalPathName(value);
      } catch (Win32Exception) {
        realValue = value;
      }
      if (paths.Contains(realValue, StringComparer.OrdinalIgnoreCase)) paths = RemoveElementFromArray(paths, realValue, StringComparison.OrdinalIgnoreCase);
      paths = paths.Append(realValue).ToArray();
      Show();
    }

    public void Prepend(string value) {
      string realValue;
      try {
        realValue = GetFinalPathName(value);
      } catch (Win32Exception) {
        realValue = value;
      }
      if (paths.Contains(realValue, StringComparer.OrdinalIgnoreCase)) paths = RemoveElementFromArray(paths, realValue, StringComparison.OrdinalIgnoreCase);
      paths = new string[] { realValue }.Concat(paths).ToArray();
      Show();
    }

    public void Remove(string value) {
      paths = paths.Where(x => !string.Equals(x, value, StringComparison.OrdinalIgnoreCase)).ToArray();
      Show();
    }

    public void Show() {
      MyConsole.WriteLine($"Environment Variable: {key}\nVariable Target: {target}");
      MyConsole.WriteLine($"========================================");
      int pathsLength = Math.Max(2, (int)Math.Ceiling(Math.Log10(paths.Length + 1)));
      for (int i = 0; i < paths.Length; i++) {
        if (string.IsNullOrEmpty(paths[i])) continue;
        string index = (i + 1).ToString().PadLeft(pathsLength, '0');
        if (DoesExist(paths[i])) MyConsole.WriteLine($"{index} {paths[i]}");
        else MyConsole.WriteLine($"{index} {paths[i]} [INVALID]", ConsoleColor.Red);
      }
    }

    public void Slim() {
      HashSet<string> uniquePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      for (int i = 0; i < paths.Length; i++) {
        if (string.IsNullOrEmpty(paths[i])) continue;
        string expanded;
        try {
          expanded = GetFinalPathName(Environment.ExpandEnvironmentVariables(paths[i]));
          if (!DoesExist(expanded)) continue;
          uniquePaths.Add(expanded);
        } catch (Win32Exception) { continue; }
      }
      paths = uniquePaths.ToArray();
      Show();
    }

    public void Sort() {
      Array.Sort(paths, StringComparer.OrdinalIgnoreCase);
      Show();
    }

    public override string ToString() {
      return string.Join(";", paths);
    }

    private bool DoesExist(string path) {
      bool result;
      string realPath = Environment.ExpandEnvironmentVariables(path);
      result = File.Exists(realPath) || Directory.Exists(realPath);
      if (!result) {
        // Check again with File System Redirection
        IntPtr wow64Value = IntPtr.Zero;
        Wow64DisableWow64FsRedirection(ref wow64Value);
        result = File.Exists(realPath) || Directory.Exists(realPath);
        Wow64RevertWow64FsRedirection(wow64Value);
      }
      return result;
    }

    private string[] RemoveElementFromArray(string[] arr, string elem, StringComparison comparisonType) {
      return arr.Where(e => !e.Equals(elem, comparisonType)).ToArray();
    }

    // Some dirty jobs for disabling File System Redirection
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

    // Some dirty jobs for getting actual path
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern uint GetFinalPathNameByHandle(SafeFileHandle hFile, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszFilePath, uint cchFilePath, uint dwFlags);

    private const uint FILE_NAME_NORMALIZED = 0x0;

    private static string GetFinalPathNameByHandle(SafeFileHandle fileHandle) {
      StringBuilder outPath = new StringBuilder(1024);
      var size = GetFinalPathNameByHandle(fileHandle, outPath, (uint)outPath.Capacity, FILE_NAME_NORMALIZED);
      if (size == 0 || size > outPath.Capacity) throw new Win32Exception(Marshal.GetLastWin32Error());
      if (outPath[0] == '\\' && outPath[1] == '\\' && outPath[2] == '?' && outPath[3] == '\\') return outPath.ToString(4, outPath.Length - 4);
      return outPath.ToString();
    }

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern SafeFileHandle CreateFile([MarshalAs(UnmanagedType.LPTStr)] string filename, [MarshalAs(UnmanagedType.U4)] FileAccess access, [MarshalAs(UnmanagedType.U4)] FileShare share, IntPtr securityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition, [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes, IntPtr templateFile);

    private const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;

    private static string GetFinalPathName(string dirtyPath) {
      using (var directoryHandle = CreateFile(dirtyPath, 0, FileShare.ReadWrite | FileShare.Delete, IntPtr.Zero, FileMode.Open, (FileAttributes)FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero)) {
        if (directoryHandle.IsInvalid) throw new Win32Exception(Marshal.GetLastWin32Error());
        return GetFinalPathNameByHandle(directoryHandle);
      }
    }
  }
}
