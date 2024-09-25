using System;
using System.Windows.Shell;
using System.Windows;
using System.Diagnostics;

namespace CustomJumpList {

  internal static class Program {
    [STAThread]
    private static void Main() {
      ProcessStartInfo processStartInfo = new ProcessStartInfo() {
        FileName = Environment.ExpandEnvironmentVariables(@"%WinDir%\explorer.exe")
      };
      Process.Start(processStartInfo);

      Application app = new Application();

      JumpList jl = new JumpList();

      JumpTask taskWebNewWindow = new JumpTask {
        ApplicationPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",
        Arguments = @"--window-size=1385,936",
        CustomCategory = @"웹 브라우저",
        Description = @"웹 브라우저의 새 창을 엽니다",
        IconResourceIndex = 0,
        IconResourcePath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",
        Title = @"새 창",
      };

      JumpTask taskWebNewIncognitoWindow = new JumpTask {
        ApplicationPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",
        Arguments = @"--window-size=1385,936 --incognito",
        CustomCategory = @"웹 브라우저",
        Description = @"웹 브라우저의 새 시크릿 창을 엽니다",
        IconResourceIndex = 1,
        IconResourcePath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",
        Title = @"새 시크릿 창",
      };

      JumpTask taskFileExplorer = new JumpTask {
        ApplicationPath = Environment.ExpandEnvironmentVariables(@"%WinDir%\explorer.exe"),
        CustomCategory = @"파일 탐색기",
        Description = @"Windows 탐색기의 새 창을 엽니다",
        IconResourceIndex = 0,
        IconResourcePath = Environment.ExpandEnvironmentVariables(@"%WinDir%\explorer.exe"),
        Title = @"파일 탐색기",
      };

      JumpTask taskGameWWMM = new JumpTask {
        ApplicationPath = @"D:\WuWaMod\JASM - Just Another Skin Manager.exe",
        CustomCategory = @"게임",
        Description = @"Wuthering Waves 모드 매니저를 실행합니다",
        IconResourceIndex = 0,
        IconResourcePath = @"D:\WuWaMod\JASM - Just Another Skin Manager.exe",
        Title = @"JASM",
      };

      jl.JumpItems.Add(taskGameWWMM);
      jl.JumpItems.Add(taskFileExplorer);
      jl.JumpItems.Add(taskWebNewWindow);
      jl.JumpItems.Add(taskWebNewIncognitoWindow);

      JumpList.SetJumpList(Application.Current, jl);
      jl.Apply();

      app.Shutdown();
    }
  }
}
