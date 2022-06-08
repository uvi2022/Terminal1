// Decompiled with JetBrains decompiler
// Type: Terminal.LoggerNV200
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using log4net;
using log4net.Config;

namespace Terminal
{
  public static class LoggerNV200
  {
    public static ILog Log { get; } = LogManager.GetLogger("LOGGERNV200");

    public static void InitLogger() => XmlConfigurator.Configure();
  }
}
