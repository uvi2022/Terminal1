// Decompiled with JetBrains decompiler
// Type: UIPSCore.Hardware.CardPayment.MemoryHelper
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Runtime.InteropServices;

namespace UIPSCore.Hardware.CardPayment
{
  public static class MemoryHelper
  {
    [DllImport("kernel32.dll")]
    public static extern IntPtr GlobalFree(IntPtr hMem);
  }
}
