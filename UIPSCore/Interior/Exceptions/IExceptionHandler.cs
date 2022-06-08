// Decompiled with JetBrains decompiler
// Type: UIPSCore.Interior.Exceptions.IExceptionHandler
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace UIPSCore.Interior.Exceptions
{
  public interface IExceptionHandler
  {
    bool CanWork(object e);

    void HandleException(object e);

    void MyExit(string reason);
  }
}
