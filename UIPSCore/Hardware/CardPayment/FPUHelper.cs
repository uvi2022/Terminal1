// Decompiled with JetBrains decompiler
// Type: UIPSCore.Hardware.CardPayment.FPUHelper
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System.Runtime.InteropServices;

namespace UIPSCore.Hardware.CardPayment
{
  public static class FPUHelper
  {
    private const uint _MCW_EM = 524319;
    private const uint _EM_INVALID = 16;

    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern uint _controlfp(uint newcw, uint mask);

    public static void FixFPU()
    {
      int num = (int) FPUHelper._controlfp(524319U, 16U);
    }
  }
}
