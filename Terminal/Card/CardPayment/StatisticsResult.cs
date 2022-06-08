// Decompiled with JetBrains decompiler
// Type: Terminal.Card.CardPayment.StatisticsResult
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Runtime.InteropServices;

namespace Terminal.Card.CardPayment
{
  public static class StatisticsResult
  {
    public static int ResultId;
    public static byte[] ResultTrack2;
    public static int ResultTType;
    public static ulong ResultAmount;
    public static string ResultRCode;
    public static string ResultAMessage;
    public static int ResultCType;
    public static IntPtr ResultCheck;

    public static void Clear()
    {
      StatisticsResult.ResultId = -1;
      if (StatisticsResult.ResultTrack2 != null)
        Array.Clear((Array) StatisticsResult.ResultTrack2, 0, StatisticsResult.ResultTrack2.Length);
      StatisticsResult.ResultTType = -1;
      StatisticsResult.ResultAmount = 0UL;
      if (StatisticsResult.ResultRCode != null)
        StatisticsResult.ResultRCode = "";
      if (StatisticsResult.ResultAMessage != null)
        StatisticsResult.ResultAMessage = "";
      StatisticsResult.ResultCType = -1;
    }

    public static string GetAMessage()
    {
      if (StatisticsResult.ResultAMessage == null)
        return (string) null;
      string amessage;
      try
      {
        string resultAmessage = StatisticsResult.ResultAMessage;
        amessage = resultAmessage.Substring(0, resultAmessage.IndexOf(char.MinValue));
      }
      catch (Exception ex)
      {
        return (string) null;
      }
      return amessage;
    }

    public static string GetCheck()
    {
      if (!(StatisticsResult.ResultCheck != IntPtr.Zero))
        return (string) null;
      string stringAnsi;
      try
      {
        stringAnsi = Marshal.PtrToStringAnsi(StatisticsResult.ResultCheck);
      }
      catch (Exception ex)
      {
        return (string) null;
      }
      return stringAnsi;
    }
  }
}
