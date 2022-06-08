// Decompiled with JetBrains decompiler
// Type: UIPSCore.Logging.LogToFunc
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace UIPSCore.Logging
{
  public class LogToFunc : ILog
  {
    private readonly Action<string> action;

    public LogToFunc(Action<string> action) => this.action = action;

    public void Debug(string format, params object[] args) => this.action(args.Length == 0 ? format : string.Format(format, args) + Environment.NewLine);

    public void Error(string format, params object[] args) => this.action(args.Length == 0 ? format : string.Format(format, args) + Environment.NewLine);

    public void Fatal(string format, params object[] args) => this.action(args.Length == 0 ? format : string.Format(format, args) + Environment.NewLine);

    public void Info(string format, params object[] args) => this.action(args.Length == 0 ? format : string.Format(format, args) + Environment.NewLine);
  }
}
