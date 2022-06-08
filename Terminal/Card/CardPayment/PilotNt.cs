// Decompiled with JetBrains decompiler
// Type: Terminal.Card.CardPayment.PilotNt
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Runtime.InteropServices;
using System.Text;
using UIPSCore.Hardware.CardPayment;

namespace Terminal.Card.CardPayment
{
  public class PilotNt : IDisposable
  {
    public const int OP_PURCHASE = 1;
    public const int OP_RETURN = 3;
    public const int OP_FUNDS = 6;
    public const int OP_CLOSEDAY = 7;
    public const int OP_SHIFT = 9;
    public const int OP_PREAUTH = 51;
    public const int OP_COMPLETION = 52;
    public const int OP_CASHIN = 53;
    public const int OP_CASHIN_COMP = 54;
    public const int MAX_ENCR_DATA = 32;
    public const int ICMD_DISP_STRING_1 = 1;
    public const int ICMD_DISP_STRING_2 = 2;
    public const int ICMD_DISP_CLEAR = 3;
    public const int ICMD_SHOW_INPUT = 4;
    public const int ICMD_GET_KEY = 5;
    public PilotNt.HalDispFuncDelegate Callback;
    private Action<uint, string, bool> _updateUi;
    private const string DllPath = "E:\\SBER\\pilot_nt1.dll";

    public PilotNt()
    {
    }

    public PilotNt(Action<uint, string, bool> updateUi)
    {
      this._updateUi = updateUi;
      this.SetGUIHandles();
    }

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _AbortTransaction();

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize10(byte[] track2, ref PilotNt.auth_answer10 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize11(byte[] track2, ref PilotNt.auth_answer11 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize2(byte[] track2, ref PilotNt.auth_answer2 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize3(byte[] track2, ref PilotNt.auth_answer3 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize4(byte[] track2, ref PilotNt.auth_answer4 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize5(byte[] track2, ref PilotNt.auth_answer5 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize6(byte[] track2, ref PilotNt.auth_answer6 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize8(byte[] track2, ref PilotNt.auth_answer8 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize9(byte[] track2, ref PilotNt.auth_answer9 a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_complete_multi_auth8(
      byte[] track2,
      ref PilotNt.auth_answer8 auth_ans,
      ref PilotNt.preauth_rec pPreAuthLista,
      ref int NumAuths);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _CommitTrx(uint dwAmount, byte[] pAuthCode);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern void _Done();

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _ReadCard(byte[] Last4Digits, byte[] Hash);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _ReadCardFull(byte[] CardNo, byte[] ValidThru);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _ReadCardSB(byte[] Last4Digits, byte[] Hash);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _ReadTrack3(byte[] Last4Digits, byte[] Hash, byte[] pTrack3);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _RollbackTrx(uint dwAmount, byte[] pAuthCode);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _ServiceMenu(ref PilotNt.auth_answer a);

    [DllImport("E:\\SBER\\pilot_nt1.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _SuspendTrx(uint dwAmount, byte[] pAuthCode);

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize(byte[] track2, ref PilotNt.auth_answer a);

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _card_authorize7(byte[] track2, ref PilotNt.auth_answer7 a);

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _close_day(ref PilotNt.auth_answer a);

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _EjectCard();

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _get_statistics(ref PilotNt.auth_answer a);

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _GetTerminalID(byte[] pTerminalID);

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern uint _GetVer();

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _ReadTrack2(byte[] track2);

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _SetGUIHandles(int hText, int hEdit);

    [DllImport("pilot_nt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _TestPinpad();

    public int TestPinpad()
    {
      int num;
      try
      {
        num = PilotNt._TestPinpad();
        Logger.Log.Info((object) string.Format("Проверка статуса Пинпада: {0}", (object) num));
      }
      catch (Exception ex)
      {
        num = -1;
        Logger.Log.Error((object) ("Ошибка Пинпада: " + ex.ToString()));
      }
      return num;
    }

    public int CardAuthorize(uint amount, byte[] track2, out PilotNt.auth_answer answer)
    {
      answer = new PilotNt.auth_answer();
      int num;
      try
      {
        PilotNt.auth_answer a = new PilotNt.auth_answer()
        {
          TType = 1,
          Amount = amount
        };
        num = PilotNt._card_authorize(track2, ref a);
        answer = a;
        CardAuthorizeResult.ResultId = num;
        CardAuthorizeResult.ResultTrack2 = track2 != null && track2.Length != 0 ? track2 : (byte[]) null;
        CardAuthorizeResult.ResultTType = a.TType;
        CardAuthorizeResult.ResultAmount = (ulong) a.Amount;
        CardAuthorizeResult.ResultRCode = a.RCode;
        CardAuthorizeResult.ResultAMessage = a.AMessage;
        CardAuthorizeResult.ResultCType = a.CType;
        CardAuthorizeResult.ResultCheck = a.Check;
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
        num = -1;
        CardAuthorizeResult.Clear();
      }
      return num;
    }

    public int CardAuthorize7(
      uint amount,
      byte[] track2,
      out PilotNt.auth_answer7 answer,
      out string check)
    {
      answer = new PilotNt.auth_answer7();
      int num;
      try
      {
        PilotNt.auth_answer7 a = new PilotNt.auth_answer7()
        {
          ans = new PilotNt.auth_answer()
          {
            TType = 1,
            Amount = amount,
            CType = 0
          }
        };
        num = PilotNt._card_authorize7(track2, ref a);
        answer = a;
        CardAuthorizeResult.ResultId = num;
        CardAuthorizeResult.ResultTrack2 = track2 != null && track2.Length != 0 ? track2 : (byte[]) null;
        CardAuthorizeResult.ResultTType = a.ans.TType;
        CardAuthorizeResult.ResultAmount = (ulong) a.ans.Amount;
        CardAuthorizeResult.ResultRCode = a.ans.RCode;
        CardAuthorizeResult.ResultAMessage = a.ans.AMessage;
        CardAuthorizeResult.ResultCType = a.ans.CType;
        CardAuthorizeResult.ResultCheck = a.ans.Check;
        CardAuthorizeResult.ResultAuthCode = a.AuthCode;
        CardAuthorizeResult.ResultCardID = a.CardID;
        CardAuthorizeResult.ResultSberOwnCard = a.SberOwnCard;
        check = CardAuthorizeResult.GetCheck();
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
        num = -1;
        CardAuthorizeResult.Clear();
        check = "";
      }
      return num;
    }

    public int CloseDay()
    {
      int num;
      try
      {
        PilotNt.auth_answer authAnswer = new PilotNt.auth_answer();
        PilotNt.auth_answer a = authAnswer;
        num = PilotNt._close_day(ref a);
        CloseDayResult.ResultId = num;
        CloseDayResult.ResultTType = a.TType;
        CloseDayResult.ResultAmount = (ulong) a.Amount;
        CloseDayResult.ResultRCode = a.RCode;
        CloseDayResult.ResultAMessage = a.AMessage;
        CloseDayResult.ResultCType = a.CType;
        if (authAnswer.Check != IntPtr.Zero)
          MemoryHelper.GlobalFree(a.Check);
      }
      catch (Exception ex)
      {
        num = -1;
        Logger.Log.Error((object) ex.ToString());
      }
      return num;
    }

    public int EjectCard()
    {
      int num;
      try
      {
        num = PilotNt._EjectCard();
      }
      catch (Exception ex)
      {
        num = -1;
        Logger.Log.Error((object) ex.ToString());
      }
      return num;
    }

    public string GetCardAuthorizeResultRCode() => CardAuthorizeResult.ResultRCode;

    public string GetHalDispFuncResultCmd() => HalDispFuncResult.ResultCmd.ToString();

    public string GetHalDispFuncResultPar1() => HalDispFuncResult.ResultPar1.ToString();

    public string GetHalDispFuncResultPar2() => HalDispFuncResult.ResultPar2.ToString();

    public string GetReadTrack2ResultCardNo()
    {
      string str = Encoding.ASCII.GetString(ReadTrack2Result.ResultTrack2);
      return string.IsNullOrEmpty(str) ? string.Empty : str.Substring(0, str.IndexOf("="));
    }

    public byte[] GetReadTrack2ResultTrack2() => ReadTrack2Result.ResultTrack2 != null ? ReadTrack2Result.ResultTrack2 : (byte[]) null;

    public int GetStatistics()
    {
      int statistics;
      try
      {
        PilotNt.auth_answer a = new PilotNt.auth_answer()
        {
          TType = 1
        };
        statistics = PilotNt._get_statistics(ref a);
        StatisticsResult.ResultId = statistics;
        StatisticsResult.ResultTType = a.TType;
        StatisticsResult.ResultAmount = (ulong) a.Amount;
        StatisticsResult.ResultRCode = a.RCode;
        StatisticsResult.ResultAMessage = a.AMessage;
        StatisticsResult.ResultCType = a.CType;
        StatisticsResult.ResultCheck = a.Check;
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
        statistics = -1;
      }
      return statistics;
    }

    public int GetTerminalID(out byte[] pTerminalIDOut)
    {
      int terminalId = 0;
      byte[] pTerminalID = new byte[128];
      pTerminalIDOut = (byte[]) null;
      try
      {
        terminalId = PilotNt._GetTerminalID(pTerminalID);
        if (terminalId == 0)
          pTerminalIDOut = pTerminalID;
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
      }
      return terminalId;
    }

    public uint GetVer()
    {
      uint ver;
      try
      {
        ver = PilotNt._GetVer();
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
        ver = 0U;
      }
      return ver;
    }

    public int HalDispFunc(int cmd, string par1, int par2)
    {
      HalDispFuncResult.ResultCmd = cmd;
      HalDispFuncResult.ResultPar1 = par1;
      HalDispFuncResult.ResultPar2 = par2;
      if (this._updateUi != null)
      {
        switch (cmd)
        {
          case 1:
            if (par2 > 5)
            {
              this._updateUi(3U, par1, true);
              break;
            }
            this._updateUi(1U, par1, true);
            break;
          case 2:
            this._updateUi(2U, par1, true);
            break;
          case 3:
            this._updateUi(4U, string.Empty, true);
            break;
          case 4:
            if (par2 != 0)
            {
              this._updateUi(2U, string.Empty, true);
              break;
            }
            this._updateUi(2U, string.Empty, false);
            break;
        }
      }
      return 0;
    }

    public int ReadTrack2(out byte[] track2out)
    {
      int num = -1;
      byte[] track2 = new byte[128];
      track2out = (byte[]) null;
      try
      {
        num = PilotNt._ReadTrack2(track2);
        ReadTrack2Result.ResultId = num;
        if (num == 0)
        {
          track2out = track2;
          ReadTrack2Result.ResultTrack2 = track2;
        }
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
      }
      return num;
    }

    private int SetGUIHandles()
    {
      byte[] numArray = new byte[40];
      int num;
      try
      {
        this.Callback = new PilotNt.HalDispFuncDelegate(this.HalDispFunc);
        int hEdit = 0;
        num = PilotNt._SetGUIHandles((int) Marshal.GetFunctionPointerForDelegate<PilotNt.HalDispFuncDelegate>(this.Callback), hEdit);
      }
      catch (Exception ex)
      {
        num = 0;
        Logger.Log.Error((object) ex);
      }
      return num;
    }

    public void Dispose() => GC.SuppressFinalize((object) this);

    public struct auth_answer
    {
      public int TType;
      public uint Amount;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
      public string RCode;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
      public string AMessage;
      public int CType;
      public IntPtr Check;
    }

    public struct auth_answer10
    {
      public PilotNt.auth_answer ans;
      public byte[] AuthCode;
      public byte[] CardID;
      public int ErrorCode;
      public byte[] TransDate;
      public int TransNumber;
      public int SberOwnCard;
      public byte[] Hash;
    }

    public struct auth_answer11
    {
      public PilotNt.auth_answer ans;
      public byte[] AuthCode;
      public byte[] CardID;
      public int ErrorCode;
      public byte[] TransDate;
      public int TransNumber;
      public int SberOwnCard;
      public byte[] Hash;
      public byte[] Track3;
    }

    public struct auth_answer2
    {
      public PilotNt.auth_answer ans;
      public byte[] AuthCode;
    }

    public struct auth_answer3
    {
      public PilotNt.auth_answer ans;
      public byte[] AuthCode;
      public byte[] CardID;
    }

    public struct auth_answer4
    {
      public PilotNt.auth_answer ans;
      public byte[] AuthCode;
      public byte[] CardID;
      public int ErrorCode;
      public byte[] TransDate;
      public int TransNumber;
    }

    public struct auth_answer5
    {
      public PilotNt.auth_answer ans;
      public byte[] RRN;
      public byte[] AuthCode;
    }

    public struct auth_answer6
    {
      public PilotNt.auth_answer ans;
      public byte[] AuthCode;
      public byte[] CardID;
      public int ErrorCode;
      public byte[] TransDate;
      public int TransNumber;
      public byte[] RRN;
    }

    public struct auth_answer7
    {
      public PilotNt.auth_answer ans;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
      public string AuthCode;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
      public string CardID;
      public int SberOwnCard;
    }

    public struct auth_answer8
    {
      public PilotNt.auth_answer ans;
      public byte[] AuthCode;
      public byte[] CardID;
      public int ErrorCode;
      public byte[] TransDate;
      public int TransNumber;
      public byte[] RRN;
      public byte[] EncryptedData;
    }

    public struct auth_answer9
    {
      public PilotNt.auth_answer ans;
      public byte[] AuthCode;
      public byte[] CardID;
      public int SberOwnCard;
      public byte[] Hash;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int HalDispFuncDelegate(int cmd, string par1, int par2);

    public struct preauth_rec
    {
      public uint Amount;
      public byte[] RRN;
      public byte[] Last4Digits;
      public int ErrorCode;
    }
  }
}
