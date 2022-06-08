// Decompiled with JetBrains decompiler
// Type: Terminal.Card.CardPayment.CardAuthorizeResult
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Runtime.InteropServices;
using UIPSCore.Hardware.CardPayment;

namespace Terminal.Card.CardPayment
{
  public static class CardAuthorizeResult
  {
    public static int ResultId;
    public static byte[] ResultTrack2;
    public static int ResultTType;
    public static ulong ResultAmount;
    public static string ResultRCode;
    public static string ResultAMessage;
    public static int ResultCType;
    public static IntPtr ResultCheck;
    public static string ResultAuthCode;
    public static string ResultCardID;
    public static int ResultSberOwnCard;

    public static string CardTypeHumanText() => ((PilotNtCardTypes) CardAuthorizeResult.ResultCType).PilotNtCardTypesHumanText();

    public static void Clear()
    {
      CardAuthorizeResult.ResultId = -1;
      if (CardAuthorizeResult.ResultTrack2 != null)
        Array.Clear((Array) CardAuthorizeResult.ResultTrack2, 0, CardAuthorizeResult.ResultTrack2.Length);
      CardAuthorizeResult.ResultTType = -1;
      CardAuthorizeResult.ResultAmount = 0UL;
      if (CardAuthorizeResult.ResultRCode != null)
        CardAuthorizeResult.ResultRCode = "";
      if (CardAuthorizeResult.ResultAMessage != null)
        CardAuthorizeResult.ResultAMessage = "";
      CardAuthorizeResult.ResultCType = -1;
    }

    public static string GetAMessage()
    {
      if (CardAuthorizeResult.ResultAMessage == null)
        return (string) null;
      string amessage;
      try
      {
        string resultAmessage = CardAuthorizeResult.ResultAMessage;
        amessage = resultAmessage.Substring(0, resultAmessage.IndexOf(char.MinValue));
      }
      catch (Exception ex)
      {
        return (string) null;
      }
      return amessage;
    }

    public static string GetCardID()
    {
      if (CardAuthorizeResult.ResultCardID == null)
        return string.Empty;
      string cardId;
      try
      {
        string resultCardId = CardAuthorizeResult.ResultCardID;
        cardId = resultCardId.Substring(0, resultCardId.IndexOf(char.MinValue));
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
      return cardId;
    }

    public static string GetCheck()
    {
      if (CardAuthorizeResult.ResultCheck == IntPtr.Zero)
        return (string) null;
      string stringAnsi;
      try
      {
        stringAnsi = Marshal.PtrToStringAnsi(CardAuthorizeResult.ResultCheck);
      }
      catch (Exception ex)
      {
        return (string) null;
      }
      return stringAnsi;
    }

    public static string GetRCode()
    {
      if (CardAuthorizeResult.ResultRCode == null)
        return string.Empty;
      string rcode;
      try
      {
        string resultRcode = CardAuthorizeResult.ResultRCode;
        rcode = resultRcode.Substring(0, resultRcode.IndexOf(char.MinValue));
      }
      catch (Exception ex)
      {
        return (string) null;
      }
      return rcode;
    }
  }
}
