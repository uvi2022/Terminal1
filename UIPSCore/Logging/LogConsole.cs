// Decompiled with JetBrains decompiler
// Type: UIPSCore.Logging.LogConsole
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace UIPSCore.Logging
{
  public class LogConsole : ILog
  {
    public static readonly ILog Instance = (ILog) new LogConsole();

    public void Debug(string format, params object[] args) => Console.WriteLine(format, args);

    public void Error(string format, params object[] args) => Console.WriteLine(format, args);

    public void Fatal(string format, params object[] args) => Console.WriteLine(format, args);

    public void Info(string format, params object[] args) => Console.WriteLine(format, args);
  }
}
