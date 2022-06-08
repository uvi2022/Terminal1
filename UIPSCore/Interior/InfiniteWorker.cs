// Decompiled with JetBrains decompiler
// Type: UIPSCore.Interior.InfiniteWorker
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.IO;
using System.Threading;
using UIPSCore.Interior.Exceptions;

namespace UIPSCore.Interior
{
  public class InfiniteWorker
  {
    private readonly IExceptionHandler exceptionHandler;
    private readonly EventWaitHandle quitEvent;
    private bool needStop;

    public InfiniteWorker(IExceptionHandler exceptionHandler)
    {
      this.exceptionHandler = exceptionHandler;
      this.quitEvent = InfiniteWorker.GetEvent();
      this.needStop = false;
    }

    public static void ApplicationStarted() => InfiniteWorker.GetEvent().Reset();

    public void DoInfiniteTask(IInfiniteWorkerTask task, int latency)
    {
      try
      {
        do
        {
          try
          {
            task.RunMe();
          }
          catch (ThreadAbortException ex)
          {
            Thread.ResetAbort();
            return;
          }
          catch (IOException ex)
          {
            if (ex.InnerException is ThreadAbortException)
            {
              Thread.ResetAbort();
              return;
            }
          }
          catch (Exception ex)
          {
            this.exceptionHandler.HandleException((object) ex);
            if (!this.exceptionHandler.CanWork((object) ex))
              return;
          }
          if (this.needStop)
            goto label_6;
        }
        while (!this.quitEvent.WaitOne(latency));
        goto label_11;
label_6:
        return;
label_11:
        task.Stop();
      }
      catch (ThreadAbortException ex)
      {
        Thread.ResetAbort();
      }
      catch (IOException ex)
      {
        if (!(ex.InnerException is ThreadAbortException))
          return;
        Thread.ResetAbort();
      }
    }

    public static EventWaitHandle GetEvent() => new EventWaitHandle(false, EventResetMode.ManualReset, "TeremQuitEvent");

    public static void SignalQuit() => InfiniteWorker.GetEvent().Set();

    public void Stop() => this.needStop = true;
  }
}
