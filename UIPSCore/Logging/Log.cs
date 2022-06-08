// Decompiled with JetBrains decompiler
// Type: UIPSCore.Logging.Log
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using log4net;
using System;
using System.Diagnostics;
using System.Reflection;

namespace UIPSCore.Logging
{
  public class Log : ILog
  {
    private readonly log4net.ILog log;

    public Log(string loggerName) => this.log = LogManager.GetLogger(loggerName);

    public void Debug(string format, params object[] args)
    {
      if (args.Length == 0)
        this.log.Debug((object) (Log.InfoFromStack() + format));
      else
        this.log.DebugFormat(Log.InfoFromStack() + format, args);
    }

    public void Error(string format, params object[] args)
    {
      if (args.Length == 0)
        this.log.Error((object) (Log.InfoFromStack() + format));
      else
        this.log.ErrorFormat(Log.InfoFromStack() + format, args);
    }

    public void Fatal(string format, params object[] args)
    {
      if (args.Length == 0)
        this.log.Fatal((object) (Log.InfoFromStack() + format));
      else
        this.log.FatalFormat(Log.InfoFromStack() + format, args);
    }

    public void Info(string format, params object[] args)
    {
      if (args.Length == 0)
        this.log.Info((object) (Log.InfoFromStack() + format));
      else
        this.log.InfoFormat(Log.InfoFromStack() + format, args);
    }

    private static string InfoFromStack()
    {
      MethodBase method = new StackFrame(2).GetMethod();
      Type declaringType = method.DeclaringType;
      return "[" + (declaringType == (Type) null ? method.Name : declaringType.Name + "." + method.Name) + "] ";
    }
  }
}
