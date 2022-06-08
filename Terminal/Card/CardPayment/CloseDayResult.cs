// Decompiled with JetBrains decompiler
// Type: Terminal.Card.CardPayment.CloseDayResult
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Card.CardPayment
{
  public static class CloseDayResult
  {
    public static int ResultId;
    public static byte[] ResultTrack2;
    public static int ResultTType;
    public static ulong ResultAmount;
    public static string ResultRCode;
    public static string ResultAMessage;
    public static int ResultCType;
    public static string ResultCheck;

    public static void Clear()
    {
      CloseDayResult.ResultId = -1;
      if (CloseDayResult.ResultTrack2 != null)
        Array.Clear((Array) CloseDayResult.ResultTrack2, 0, CloseDayResult.ResultTrack2.Length);
      CloseDayResult.ResultTType = 0;
      CloseDayResult.ResultAmount = 0UL;
      if (CloseDayResult.ResultRCode != null)
        CloseDayResult.ResultRCode = "";
      if (CloseDayResult.ResultAMessage != null)
        CloseDayResult.ResultAMessage = "";
      CloseDayResult.ResultCType = -1;
    }

    public static string GetAMessage()
    {
      if (CloseDayResult.ResultAMessage == null)
        return string.Empty;
      string amessage;
      try
      {
        string resultAmessage = CloseDayResult.ResultAMessage;
        amessage = resultAmessage.Substring(0, resultAmessage.IndexOf(char.MinValue));
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
      return amessage;
    }

    public static string GetCheck() => CloseDayResult.ResultCheck;

    public static int GetRCode()
    {
      string rcodeString = CloseDayResult.GetRCodeString();
      if (string.IsNullOrEmpty(rcodeString))
        return -1;
      int result;
      int.TryParse(rcodeString, out result);
      return result;
    }

    public static string GetRCodeString()
    {
      if (CloseDayResult.ResultRCode == null)
        return string.Empty;
      string rcodeString;
      try
      {
        string resultRcode = CloseDayResult.ResultRCode;
        rcodeString = resultRcode.Substring(0, resultRcode.IndexOf(char.MinValue)).TrimEnd();
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
      return rcodeString;
    }
  }
}
