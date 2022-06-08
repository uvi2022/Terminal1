// Decompiled with JetBrains decompiler
// Type: UIPSCore.Hardware.CardPayment.HalDispFuncResult
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace UIPSCore.Hardware.CardPayment
{
  public static class HalDispFuncResult
  {
    public static int ResultCmd;
    public static string ResultPar1;
    public static int ResultPar2;

    public static void Reset()
    {
      HalDispFuncResult.ResultCmd = -1;
      HalDispFuncResult.ResultPar1 = "";
      HalDispFuncResult.ResultPar2 = -1;
    }
  }
}
