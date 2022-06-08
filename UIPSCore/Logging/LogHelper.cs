// Decompiled with JetBrains decompiler
// Type: UIPSCore.Logging.LogHelper
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace UIPSCore.Logging
{
  public static class LogHelper
  {
    public static void Debug(this ILog log, object o) => log.Debug(o == null ? "<null>" : o.ToString());

    public static void Error(this ILog log, object o) => log.Error(o == null ? "<null>" : o.ToString());

    public static void Fatal(this ILog log, object o) => log.Fatal(o == null ? "<null>" : o.ToString());

    public static void Info(this ILog log, object o) => log.Info(o == null ? "<null>" : o.ToString());
  }
}
