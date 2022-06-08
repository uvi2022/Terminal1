// Decompiled with JetBrains decompiler
// Type: UIPSCore.Interior.Exceptions.TeremWebException
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace UIPSCore.Interior.Exceptions
{
  public class TeremWebException : Exception
  {
    public TeremWebException()
    {
    }

    public TeremWebException(string message)
      : base(message)
    {
    }

    public TeremWebException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
