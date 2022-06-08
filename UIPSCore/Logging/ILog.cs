// Decompiled with JetBrains decompiler
// Type: UIPSCore.Logging.ILog
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace UIPSCore.Logging
{
  public interface ILog
  {
    void Debug(string format, params object[] args);

    void Error(string format, params object[] args);

    void Fatal(string format, params object[] args);

    void Info(string format, params object[] args);
  }
}
