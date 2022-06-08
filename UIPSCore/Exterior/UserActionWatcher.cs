// Decompiled with JetBrains decompiler
// Type: UIPSCore.Exterior.UserActionWatcher
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using UIPSCore.Interior;

namespace UIPSCore.Exterior
{
  public class UserActionWatcher : IUserActionWatcher, IInfiniteWorkerTask
  {
    private readonly double userTimeoutInMinutes;
    private DateTime timeForTimeout;

    public UserActionWatcher()
      : this(10.0)
    {
    }

    public UserActionWatcher(double userTimeoutInMinutes)
    {
      this.userTimeoutInMinutes = userTimeoutInMinutes;
      this.GotUserAction();
    }

    public void GotUserAction() => this.timeForTimeout = DateTime.Now.AddMinutes(this.userTimeoutInMinutes);

    public void RunMe()
    {
      if (!(DateTime.Now > this.timeForTimeout) || this.UserActionTimeout == null)
        return;
      this.UserActionTimeout();
      this.GotUserAction();
    }

    public void Stop()
    {
    }

    public event Action UserActionTimeout;
  }
}
