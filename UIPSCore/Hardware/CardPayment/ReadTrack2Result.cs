// Decompiled with JetBrains decompiler
// Type: UIPSCore.Hardware.CardPayment.ReadTrack2Result
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Text;

namespace UIPSCore.Hardware.CardPayment
{
  public static class ReadTrack2Result
  {
    public static int ResultId;
    public static byte[] ResultTrack2;

    public static void Clear()
    {
      ReadTrack2Result.ResultId = -1;
      if (ReadTrack2Result.ResultTrack2 == null)
        return;
      Array.Clear((Array) ReadTrack2Result.ResultTrack2, 0, ReadTrack2Result.ResultTrack2.Length);
    }

    public static string GetAsString()
    {
      if (ReadTrack2Result.ResultTrack2 == null)
        return string.Empty;
      string str = Encoding.GetEncoding("windows-1251").GetString(ReadTrack2Result.ResultTrack2);
      return str.Substring(0, str.IndexOf(char.MinValue));
    }
  }
}
