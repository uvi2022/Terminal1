﻿// Decompiled with JetBrains decompiler
// Type: Terminal.NV200.CHelpers
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.NV200
{
  public class CHelpers
  {
    public static int ConvertBytesToInt32(byte[] b, int index) => BitConverter.ToInt32(b, index);

    public static int ConvertBytesToInt16(byte[] b, int index) => (int) BitConverter.ToInt16(b, index);

    public static byte[] ConvertIntToBytes(int n) => BitConverter.GetBytes(n);

    public static byte[] ConvertIntToBytes(short n) => BitConverter.GetBytes(n);

    public static byte[] ConvertIntToBytes(char n) => BitConverter.GetBytes(n);

    public static string FormatToCurrency(int unformattedNumber) => ((float) unformattedNumber / 100f).ToString("0.00");

    public static string ConvertByteToName(byte b)
    {
      switch (b)
      {
        case 1:
          return "RESET COMMAND";
        case 2:
          return "SET INHIBITS COMMAND";
        case 3:
          return "DISPLAY ON COMMAND";
        case 4:
          return "DISPLAY OFF COMMAND";
        case 5:
          return "SETUP REQUEST COMMAND";
        case 7:
          return "POLL COMMAND";
        case 9:
          return "DISABLE COMMAND";
        case 10:
          return "ENABLE COMMAND";
        case 17:
          return "SYNC COMMAND";
        case 59:
          return "SET ROUTING COMMAND";
        case 63:
          return "EMPTY COMMAND";
        case 65:
          return "GET NOTE POSITIONS COMMAND";
        case 66:
          return "PAYOUT LAST NOTE COMMAND";
        case 67:
          return "STACK LAST NOTE COMMAND";
        case 69:
          return "SET VALUE REPORTING TYPE COMMAND";
        case 74:
          return "SET GENERATOR COMMAND";
        case 75:
          return "SET MODULUS COMMAND";
        case 76:
          return "KEY EXCHANGE COMMAND";
        case 91:
          return "DISABLE PAYOUT COMMAND";
        case 92:
          return "ENABLE PAYOUT COMMAND";
        case 201:
          return "NOTE TRANSFERRED TO STACKER RESPONSE";
        case 204:
          return "STACKING RESPONSE";
        case 210:
          return "NOTE DISPENSED RESPONSE";
        case 218:
          return "NOTE DISPENSING RESPONSE";
        case 219:
          return "NOTE STORED RESPONSE";
        case 225:
          return "NOTE CLEARED FROM FRONT RESPONSE";
        case 226:
          return "NOTE CLEARED TO CASHBOX RESPONSE";
        case 227:
          return "CASHBOX REMOVED RESPONSE";
        case 228:
          return "CASHBOX REPLACED RESPONSE";
        case 230:
          return "FRAUD ATTEMPT RESPONSE";
        case 231:
          return "STACKER FULL RESPONSE";
        case 232:
          return "DISABLED RESPONSE";
        case 233:
          return "UNSAFE JAM RESPONSE";
        case 234:
          return "SAFE JAM RESPONSE";
        case 235:
          return "STACKED RESPONSE";
        case 236:
          return "REJECTED RESPONSE";
        case 237:
          return "REJECTING RESPONSE";
        case 238:
          return "CREDIT RESPONSE";
        case 239:
          return "NOTE READ RESPONSE";
        case 240:
          return "OK RESPONSE";
        case 241:
          return "RESET RESPONSE";
        case 242:
          return "UNKNOWN RESPONSE";
        case 243:
          return "WRONG PARAMS RESPONSE";
        case 244:
          return "PARAM OUT OF RANGE RESPONSE";
        case 245:
          return "CANNOT PROCESS RESPONSE";
        case 246:
          return "SOFTWARE ERROR RESPONSE";
        case 248:
          return "FAIL RESPONSE";
        case 250:
          return "KEY NOT SET RESPONSE";
        default:
          return "Byte command name unsupported";
      }
    }
  }
}
