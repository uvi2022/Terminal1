// Decompiled with JetBrains decompiler
// Type: Terminal.NV200.CPayout
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using ITLlib;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Terminal.Classes;

namespace Terminal.NV200
{
  public class CPayout
  {
    private SSPComms _mESsp;
    private SSP_COMMAND _mCmd;
    private SSP_KEYS _keys;
    private SSP_FULL_KEY _sspKey;
    private SSP_COMMAND_INFO _info;
    private int _mProtocolVersion;
    private int _mNumberOfChannels;
    private char _mUnitType;
    private int _mValueMultiplier;
    private int _mHoldNumber;
    private int _mHoldCount;
    private bool _mNoteHeld;
    private List<ChannelData> _mUnitDataList;

    public event ChangedEventHandler OnNoteReading;

    private void NoteReading(EventArgs e)
    {
      ChangedEventHandler onNoteReading = this.OnNoteReading;
      if (onNoteReading == null)
        return;
      onNoteReading((object) this, e);
    }

    public event ChangedEventHandler OnNoteInserted;

    private void NoteInserted(EventArgs e)
    {
      ChangedEventHandler onNoteInserted = this.OnNoteInserted;
      if (onNoteInserted == null)
        return;
      onNoteInserted((object) this, e);
    }

    public event ChangedEventHandler OnNoteProcessed;

    private void NoteProcessed(EventArgs e)
    {
      ChangedEventHandler onNoteProcessed = this.OnNoteProcessed;
      if (onNoteProcessed == null)
        return;
      onNoteProcessed((object) this, e);
    }

    public event ChangedEventHandler OnNoteStacked;

    private void NoteStacked(EventArgs e)
    {
      ChangedEventHandler onNoteStacked = this.OnNoteStacked;
      if (onNoteStacked == null)
        return;
      onNoteStacked((object) this, e);
    }

    public event ChangedEventHandler OnNoteDispensed;

    private void NoteDispensed(EventArgs e)
    {
      ChangedEventHandler onNoteDispensed = this.OnNoteDispensed;
      if (onNoteDispensed == null)
        return;
      onNoteDispensed((object) this, e);
    }

    public event ChangedEventHandler OnAllNotesDispensed;

    private void AllNotesDispensed(EventArgs e)
    {
      ChangedEventHandler allNotesDispensed = this.OnAllNotesDispensed;
      if (allNotesDispensed == null)
        return;
      allNotesDispensed((object) this, e);
    }

    public event ChangedEventHandler OnErrorDuringPayout;

    private void ErrorDuringPayout(EventArgs e)
    {
      ChangedEventHandler errorDuringPayout = this.OnErrorDuringPayout;
      if (errorDuringPayout == null)
        return;
      errorDuringPayout((object) this, e);
    }

    public CPayout()
    {
      this._mESsp = new SSPComms();
      this._mCmd = new SSP_COMMAND();
      this._keys = new SSP_KEYS();
      this._sspKey = new SSP_FULL_KEY();
      this._info = new SSP_COMMAND_INFO();
      this._mProtocolVersion = 0;
      this._mNumberOfChannels = 0;
      this._mValueMultiplier = 1;
      this._mUnitDataList = new List<ChannelData>();
      this._mHoldCount = 0;
      this._mHoldNumber = 0;
    }

    public SSPComms SSPComms
    {
      get => this._mESsp;
      set => this._mESsp = value;
    }

    public SSP_COMMAND CommandStructure
    {
      get => this._mCmd;
      set => this._mCmd = value;
    }

    public SSP_COMMAND_INFO InfoStructure
    {
      get => this._info;
      set => this._info = value;
    }

    public int NumberOfChannels
    {
      get => this._mNumberOfChannels;
      set => this._mNumberOfChannels = value;
    }

    public int Multiplier
    {
      get => this._mValueMultiplier;
      set => this._mValueMultiplier = value;
    }

    public int HoldNumber
    {
      get => this._mHoldNumber;
      set => this._mHoldNumber = value;
    }

    public bool NoteHeld => this._mNoteHeld;

    public List<ChannelData> UnitDataList => this._mUnitDataList;

    public char UnitType => this._mUnitType;

    public int Credit { get; set; }

    public bool OpenComPort()
    {
      Logger.Log.Info((object) "Opening com port Validator");
      LoggerNV200.Log.Info((object) "Opening com port Validator");
      return this._mESsp.OpenSSPComPort(this._mCmd);
    }

    public void EnableValidator()
    {
      this._mCmd.CommandData[0] = (byte) 10;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Unit enabled");
      LoggerNV200.Log.Info((object) "Unit enabled");
    }

    public void DisableValidator()
    {
      this._mCmd.CommandData[0] = (byte) 9;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Unit disabled");
      LoggerNV200.Log.Info((object) "Unit disabled");
    }

    public void EnablePayout()
    {
      this._mCmd.CommandData[0] = (byte) 92;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Payout enabled");
      LoggerNV200.Log.Info((object) "Payout enabled");
    }

    public void DisablePayout()
    {
      this._mCmd.CommandData[0] = (byte) 91;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Payout disabled");
      LoggerNV200.Log.Info((object) "Payout disabled");
    }

    public void EmptyPayoutDevice()
    {
      this._mCmd.CommandData[0] = (byte) 63;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Emptying payout device");
      LoggerNV200.Log.Info((object) "Emptying payout device");
    }

    public int CheckNoteLevel(int note, char[] currency)
    {
      this._mCmd.CommandData[0] = (byte) 53;
      byte[] bytes = CHelpers.ConvertIntToBytes(note);
      this._mCmd.CommandData[1] = bytes[0];
      this._mCmd.CommandData[2] = bytes[1];
      this._mCmd.CommandData[3] = bytes[2];
      this._mCmd.CommandData[4] = bytes[3];
      this._mCmd.CommandData[5] = (byte) currency[0];
      this._mCmd.CommandData[6] = (byte) currency[1];
      this._mCmd.CommandData[7] = (byte) currency[2];
      this._mCmd.CommandDataLength = (byte) 8;
      return !this.SendCommand() || !this.CheckGenericResponses() ? 0 : (int) this._mCmd.ResponseData[1];
    }

    public void ReturnNote()
    {
      this._mCmd.CommandData[0] = (byte) 8;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Returning note");
      LoggerNV200.Log.Info((object) "Returning note");
      this._mHoldCount = 0;
    }

    public void GetAllLevels()
    {
      this._mCmd.CommandData[0] = (byte) 34;
      this._mCmd.CommandDataLength = (byte) 1;
      StringBuilder stringBuilder = new StringBuilder(500);
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      stringBuilder.Append("Number of Denominations = ");
      stringBuilder.Append(this._mCmd.ResponseData[1]);
      stringBuilder.AppendLine();
      for (int index = 1; index <= (int) this._mCmd.ResponseData[1]; ++index)
      {
        int int16 = (int) BitConverter.ToInt16(this._mCmd.ResponseData, 9 * index - 7);
        int num = BitConverter.ToInt32(this._mCmd.ResponseData, 9 * index - 5) / this._mValueMultiplier;
        stringBuilder.Append(int16);
        stringBuilder.Append(" x ");
        stringBuilder.Append(num);
        stringBuilder.Append(" ");
        stringBuilder.Append((char) this._mCmd.ResponseData[9 * index - 1]);
        stringBuilder.Append((char) this._mCmd.ResponseData[9 * index]);
        stringBuilder.Append((char) this._mCmd.ResponseData[9 * index + 1]);
        stringBuilder.Append(" = ");
        stringBuilder.Append(int16 * num);
        stringBuilder.AppendLine();
      }
      stringBuilder.AppendLine();
      Logger.Log.Info((object) stringBuilder.ToString());
      LoggerNV200.Log.Info((object) stringBuilder.ToString());
    }

    public void ChangeNoteRoute(int note, char[] currency, bool stack)
    {
      this._mCmd.CommandData[0] = (byte) 59;
      this._mCmd.CommandData[1] = !stack ? (byte) 0 : (byte) 1;
      byte[] bytes = BitConverter.GetBytes(note);
      this._mCmd.CommandData[2] = bytes[0];
      this._mCmd.CommandData[3] = bytes[1];
      this._mCmd.CommandData[4] = bytes[2];
      this._mCmd.CommandData[5] = bytes[3];
      this._mCmd.CommandData[6] = (byte) currency[0];
      this._mCmd.CommandData[7] = (byte) currency[1];
      this._mCmd.CommandData[8] = (byte) currency[2];
      this._mCmd.CommandDataLength = (byte) 9;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      string str1 = new string(currency);
      string str2 = !stack ? str1 + " to storage)" : str1 + " to cashbox)";
      Logger.Log.Info((object) ("Note routing successful (" + CHelpers.FormatToCurrency(note) + " " + str2));
      LoggerNV200.Log.Info((object) ("Note routing successful (" + CHelpers.FormatToCurrency(note) + " " + str2));
    }

    public void Reset()
    {
      this._mCmd.CommandData[0] = (byte) 1;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand())
        return;
      this.CheckGenericResponses();
    }

    public bool SendSync()
    {
      this._mCmd.CommandData[0] = (byte) 17;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return false;
      Logger.Log.Info((object) "Sent sync");
      LoggerNV200.Log.Info((object) "Sent sync");
      return true;
    }

    public void SetProtocolVersion(byte pVersion)
    {
      this._mCmd.CommandData[0] = (byte) 6;
      this._mCmd.CommandData[1] = pVersion;
      this._mCmd.CommandDataLength = (byte) 2;
      this.SendCommand();
    }

    public bool PayoutAmount(int amount, char[] currency = null)
    {
      Logger.Log.Info((object) ("Сдача " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
      LoggerNV200.Log.Info((object) ("Сдача " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
      if (currency == null)
        currency = new char[3]{ 'R', 'U', 'B' };
      this._mCmd.CommandData[0] = (byte) 51;
      byte[] bytes = CHelpers.ConvertIntToBytes(amount);
      this._mCmd.CommandData[1] = bytes[0];
      this._mCmd.CommandData[2] = bytes[1];
      this._mCmd.CommandData[3] = bytes[2];
      this._mCmd.CommandData[4] = bytes[3];
      this._mCmd.CommandData[5] = (byte) currency[0];
      this._mCmd.CommandData[6] = (byte) currency[1];
      this._mCmd.CommandData[7] = (byte) currency[2];
      this._mCmd.CommandData[8] = (byte) 88;
      this._mCmd.CommandDataLength = (byte) 9;
      Logger.Log.Info((object) ("Отправляем запрос на выплату " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
      LoggerNV200.Log.Info((object) ("Отправляем запрос на выплату " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
      int num = 1;
      while (true)
      {
        Thread.Sleep(1000);
        Logger.Log.Info((object) ("Отправляем команду. Попытка " + num.ToString()));
        LoggerNV200.Log.Info((object) ("Отправляем команду. Попытка " + num.ToString()));
        if (!this.SendCommand())
        {
          LoggerNV200.Log.Error((object) ("Не удалось отправить команду. Попытка " + num.ToString()));
        }
        else
        {
          Thread.Sleep(1000);
          if (!this.CheckGenericResponses())
          {
            Logger.Log.Info((object) ("Ошибка при выплате " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
            LoggerNV200.Log.Info((object) ("Ошибка при выплате " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
            if (num >= 5)
              goto label_8;
          }
          else
            break;
        }
        ++num;
      }
      Logger.Log.Info((object) ("Отправлен запрос на выплату " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
      LoggerNV200.Log.Info((object) ("Отправлен запрос на выплату " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
      Logger.Log.Info((object) ("Завершение метода " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
      LoggerNV200.Log.Info((object) ("Завершение метода " + CHelpers.FormatToCurrency(amount) + " " + new string(currency)));
      return true;
label_8:
      Logger.Log.Info((object) "Не удалось отправить команду");
      LoggerNV200.Log.Info((object) "Не удалось отправить команду");
      return false;
    }

    public void PayoutByDenomination(byte numDenoms, byte[] data, byte dataLength)
    {
      this._mCmd.CommandData[0] = (byte) 70;
      this._mCmd.CommandData[1] = numDenoms;
      int num1 = 2;
      for (int index = 0; index < (int) dataLength; ++index)
        this._mCmd.CommandData[num1++] = data[index];
      byte[] commandData = this._mCmd.CommandData;
      int index1 = num1;
      int num2 = index1 + 1;
      commandData[index1] = (byte) 88;
      dataLength += (byte) 3;
      this._mCmd.CommandDataLength = dataLength;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Paying out by denomination...");
      LoggerNV200.Log.Info((object) "Paying out by denomination...");
    }

    public bool NegotiateKeys()
    {
      this._mCmd.EncryptionStatus = false;
      Logger.Log.Info((object) "Syncing... ");
      LoggerNV200.Log.Info((object) "Syncing... ");
      this._mCmd.CommandData[0] = (byte) 17;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand())
        return false;
      Logger.Log.Info((object) "Success");
      LoggerNV200.Log.Info((object) "Success");
      this._mESsp.InitiateSSPHostKeys(this._keys, this._mCmd);
      this._mCmd.CommandData[0] = (byte) 74;
      this._mCmd.CommandDataLength = (byte) 9;
      Logger.Log.Info((object) "Setting generator... ");
      LoggerNV200.Log.Info((object) "Setting generator... ");
      BitConverter.GetBytes(this._keys.Generator).CopyTo((Array) this._mCmd.CommandData, 1);
      if (!this.SendCommand())
        return false;
      Logger.Log.Info((object) "Success");
      LoggerNV200.Log.Info((object) "Success");
      this._mCmd.CommandData[0] = (byte) 75;
      this._mCmd.CommandDataLength = (byte) 9;
      Logger.Log.Info((object) "Sending modulus... ");
      LoggerNV200.Log.Info((object) "Sending modulus... ");
      BitConverter.GetBytes(this._keys.Modulus).CopyTo((Array) this._mCmd.CommandData, 1);
      if (!this.SendCommand())
        return false;
      Logger.Log.Info((object) "Success");
      LoggerNV200.Log.Info((object) "Success");
      this._mCmd.CommandData[0] = (byte) 76;
      this._mCmd.CommandDataLength = (byte) 9;
      Logger.Log.Info((object) "Exchanging keys... ");
      LoggerNV200.Log.Info((object) "Exchanging keys... ");
      BitConverter.GetBytes(this._keys.HostInter).CopyTo((Array) this._mCmd.CommandData, 1);
      if (!this.SendCommand())
        return false;
      Logger.Log.Info((object) "Success");
      LoggerNV200.Log.Info((object) "Success");
      this._keys.SlaveInterKey = BitConverter.ToUInt64(this._mCmd.ResponseData, 1);
      this._mESsp.CreateSSPHostEncryptionKey(this._keys);
      this._mCmd.Key.FixedKey = 81985526925837671UL;
      this._mCmd.Key.VariableKey = this._keys.KeyHost;
      Logger.Log.Info((object) "Keys successfully negotiated");
      LoggerNV200.Log.Info((object) "Keys successfully negotiated");
      return true;
    }

    public void PayoutSetupRequest()
    {
      StringBuilder stringBuilder = new StringBuilder(1000);
      this._mCmd.CommandData[0] = (byte) 5;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand())
        return;
      int num1 = 1;
      stringBuilder.Append("Unit Type: ");
      byte[] responseData1 = this._mCmd.ResponseData;
      int index1 = num1;
      int num2 = index1 + 1;
      this._mUnitType = (char) responseData1[index1];
      switch (this._mUnitType)
      {
        case char.MinValue:
          stringBuilder.Append("Validator");
          break;
        case '\u0003':
          stringBuilder.Append("SMART Hopper");
          break;
        case '\u0006':
          stringBuilder.Append("SMART Payout");
          break;
        case '\a':
          stringBuilder.Append("NV11");
          break;
        default:
          stringBuilder.Append("Unknown Type");
          break;
      }
      stringBuilder.AppendLine();
      stringBuilder.Append("Firmware: ");
      while (num2 <= 5)
      {
        stringBuilder.Append((char) this._mCmd.ResponseData[num2++]);
        if (num2 == 4)
          stringBuilder.Append(".");
      }
      stringBuilder.AppendLine();
      int num3 = num2 + 3 + 3;
      stringBuilder.AppendLine();
      stringBuilder.Append("Number of Channels: ");
      byte[] responseData2 = this._mCmd.ResponseData;
      int index2 = num3;
      int num4 = index2 + 1;
      this._mNumberOfChannels = (int) responseData2[index2];
      stringBuilder.Append(this._mNumberOfChannels);
      stringBuilder.AppendLine();
      int index3 = num4 + this._mNumberOfChannels + this._mNumberOfChannels;
      stringBuilder.Append("Real Value Multiplier: ");
      this._mValueMultiplier = (int) this._mCmd.ResponseData[index3 + 2];
      this._mValueMultiplier += (int) this._mCmd.ResponseData[index3 + 1] << 8;
      this._mValueMultiplier += (int) this._mCmd.ResponseData[index3] << 16;
      stringBuilder.Append(this._mValueMultiplier);
      stringBuilder.AppendLine();
      int num5 = index3 + 3;
      stringBuilder.Append("Protocol Version: ");
      byte[] responseData3 = this._mCmd.ResponseData;
      int index4 = num5;
      int num6 = index4 + 1;
      this._mProtocolVersion = (int) responseData3[index4];
      stringBuilder.Append(this._mProtocolVersion);
      stringBuilder.AppendLine();
      this._mUnitDataList.Clear();
      for (byte index5 = 0; (int) index5 < this._mNumberOfChannels; ++index5)
      {
        ChannelData channelData = new ChannelData();
        channelData.Channel = (byte) ((uint) index5 + 1U);
        channelData.Value = BitConverter.ToInt32(this._mCmd.ResponseData, num6 + this._mNumberOfChannels * 3 + (int) index5 * 4) * this._mValueMultiplier;
        channelData.Currency[0] = (char) this._mCmd.ResponseData[num6 + (int) index5 * 3];
        channelData.Currency[1] = (char) this._mCmd.ResponseData[num6 + 1 + (int) index5 * 3];
        channelData.Currency[2] = (char) this._mCmd.ResponseData[num6 + 2 + (int) index5 * 3];
        channelData.Level = this.CheckNoteLevel(channelData.Value, channelData.Currency);
        bool response = false;
        this.IsNoteRecycling(channelData.Value, channelData.Currency, ref response);
        channelData.Recycling = response;
        this._mUnitDataList.Add(channelData);
        stringBuilder.Append("Channel ");
        stringBuilder.Append(channelData.Channel);
        stringBuilder.Append(": ");
        stringBuilder.Append(channelData.Value / this._mValueMultiplier);
        stringBuilder.Append(" ");
        stringBuilder.Append(channelData.Currency);
        stringBuilder.AppendLine();
      }
      this._mUnitDataList.Sort((Comparison<ChannelData>) ((d1, d2) => d1.Value.CompareTo(d2.Value)));
      Logger.Log.Info((object) stringBuilder.ToString());
      LoggerNV200.Log.Info((object) stringBuilder.ToString());
    }

    public void SetInhibits()
    {
      this._mCmd.CommandData[0] = (byte) 2;
      this._mCmd.CommandData[1] = byte.MaxValue;
      this._mCmd.CommandData[2] = byte.MaxValue;
      this._mCmd.CommandDataLength = (byte) 3;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Inhibits set");
      LoggerNV200.Log.Info((object) "Inhibits set");
    }

    public void IsNoteRecycling(int noteValue, char[] currency, ref bool response)
    {
      this._mCmd.CommandData[0] = (byte) 60;
      byte[] bytes = CHelpers.ConvertIntToBytes(noteValue);
      this._mCmd.CommandData[1] = bytes[0];
      this._mCmd.CommandData[2] = bytes[1];
      this._mCmd.CommandData[3] = bytes[2];
      this._mCmd.CommandData[4] = bytes[3];
      this._mCmd.CommandData[5] = (byte) currency[0];
      this._mCmd.CommandData[6] = (byte) currency[1];
      this._mCmd.CommandData[7] = (byte) currency[2];
      this._mCmd.CommandDataLength = (byte) 8;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      if (this._mCmd.ResponseData[1] == (byte) 0)
        response = true;
      else if (this._mCmd.ResponseData[1] == (byte) 1)
        response = false;
    }

    public void SetFloat(int minPayout, int floatAmount, char[] currency)
    {
      this._mCmd.CommandData[0] = (byte) 61;
      byte[] bytes1 = CHelpers.ConvertIntToBytes(minPayout);
      this._mCmd.CommandData[1] = bytes1[0];
      this._mCmd.CommandData[2] = bytes1[1];
      this._mCmd.CommandData[3] = bytes1[2];
      this._mCmd.CommandData[4] = bytes1[3];
      byte[] bytes2 = CHelpers.ConvertIntToBytes(floatAmount);
      this._mCmd.CommandData[5] = bytes2[0];
      this._mCmd.CommandData[6] = bytes2[1];
      this._mCmd.CommandData[7] = bytes2[2];
      this._mCmd.CommandData[8] = bytes2[3];
      this._mCmd.CommandData[9] = (byte) currency[0];
      this._mCmd.CommandData[10] = (byte) currency[1];
      this._mCmd.CommandData[11] = (byte) currency[2];
      this._mCmd.CommandData[12] = (byte) 88;
      this._mCmd.CommandDataLength = (byte) 13;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "Floated amount successfully");
      LoggerNV200.Log.Info((object) "Floated amount successfully");
    }

    public void SmartEmpty()
    {
      this._mCmd.CommandData[0] = (byte) 82;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Logger.Log.Info((object) "SMART Emptying...");
      LoggerNV200.Log.Info((object) "SMART Emptying...");
    }

    public string GetCashboxPayoutOpData()
    {
      StringBuilder stringBuilder = new StringBuilder(100);
      this._mCmd.CommandData[0] = (byte) 83;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return "";
      int num = (int) this._mCmd.ResponseData[1];
      stringBuilder.Append("Total Number of Notes: ");
      stringBuilder.Append(num.ToString());
      stringBuilder.AppendLine();
      stringBuilder.AppendLine();
      int index;
      for (index = 2; index < 9 * num; index += 9)
      {
        stringBuilder.Append("Moved ");
        stringBuilder.Append(CHelpers.ConvertBytesToInt16(this._mCmd.ResponseData, index));
        stringBuilder.Append(" x ");
        stringBuilder.Append(CHelpers.FormatToCurrency(CHelpers.ConvertBytesToInt32(this._mCmd.ResponseData, index + 2)));
        stringBuilder.Append(" ");
        stringBuilder.Append((char) this._mCmd.ResponseData[index + 6]);
        stringBuilder.Append((char) this._mCmd.ResponseData[index + 7]);
        stringBuilder.Append((char) this._mCmd.ResponseData[index + 8]);
        stringBuilder.Append(" to cashbox");
        stringBuilder.AppendLine();
      }
      stringBuilder.Append(CHelpers.ConvertBytesToInt32(this._mCmd.ResponseData, index));
      stringBuilder.Append(" notes not recognised");
      stringBuilder.AppendLine();
      Logger.Log.Info((object) stringBuilder.ToString());
      LoggerNV200.Log.Info((object) stringBuilder.ToString());
      return stringBuilder.ToString();
    }

    public void ConfigureBezel(byte red, byte green, byte blue)
    {
      this._mCmd.CommandData[0] = (byte) 84;
      this._mCmd.CommandData[1] = red;
      this._mCmd.CommandData[2] = green;
      this._mCmd.CommandData[3] = blue;
      this._mCmd.CommandData[4] = (byte) 0;
      this._mCmd.CommandDataLength = (byte) 5;
      this.SendCommand();
    }

    public void QueryRejection()
    {
      this._mCmd.CommandData[0] = (byte) 23;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      switch (this._mCmd.ResponseData[1])
      {
        case 0:
          LoggerNV200.Log.Info((object) "Note accepted");
          break;
        case 1:
          LoggerNV200.Log.Info((object) "Note length incorrect");
          break;
        case 2:
          LoggerNV200.Log.Info((object) "Invalid note");
          break;
        case 3:
          LoggerNV200.Log.Info((object) "Invalid note");
          break;
        case 4:
          LoggerNV200.Log.Info((object) "Invalid note");
          break;
        case 5:
          LoggerNV200.Log.Info((object) "Invalid note");
          break;
        case 6:
          LoggerNV200.Log.Info((object) "Channel inhibited");
          break;
        case 7:
          LoggerNV200.Log.Info((object) "Second note inserted during read");
          break;
        case 8:
          LoggerNV200.Log.Info((object) "Host rejected note");
          break;
        case 9:
          LoggerNV200.Log.Info((object) "Invalid note");
          break;
        case 10:
          LoggerNV200.Log.Info((object) "Invalid note read");
          break;
        case 11:
          LoggerNV200.Log.Info((object) "Note too long");
          break;
        case 12:
          LoggerNV200.Log.Info((object) "Validator disabled");
          break;
        case 13:
          LoggerNV200.Log.Info((object) "Mechanism slow/stalled");
          break;
        case 14:
          LoggerNV200.Log.Info((object) "Strim attempt");
          break;
        case 15:
          LoggerNV200.Log.Info((object) "Fraud channel reject");
          break;
        case 16:
          LoggerNV200.Log.Info((object) "No notes inserted");
          break;
        case 17:
          LoggerNV200.Log.Info((object) "Invalid note read");
          break;
        case 18:
          LoggerNV200.Log.Info((object) "Twisted note detected");
          break;
        case 19:
          LoggerNV200.Log.Info((object) "Escrow time-out");
          break;
        case 20:
          LoggerNV200.Log.Info((object) "Bar code scan fail");
          break;
        case 21:
          LoggerNV200.Log.Info((object) "Invalid note read");
          break;
        case 22:
          LoggerNV200.Log.Info((object) "Invalid note read");
          break;
        case 23:
          LoggerNV200.Log.Info((object) "Invalid note read");
          break;
        case 24:
          LoggerNV200.Log.Info((object) "Invalid note read");
          break;
        case 25:
          LoggerNV200.Log.Info((object) "Incorrect note width");
          break;
        case 26:
          LoggerNV200.Log.Info((object) "Note too short");
          break;
      }
    }

    public void GetSerialNumber(byte device)
    {
      this._mCmd.CommandData[0] = (byte) 12;
      this._mCmd.CommandData[1] = device;
      this._mCmd.CommandDataLength = (byte) 2;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Array.Reverse((Array) this._mCmd.ResponseData, 1, 4);
      Logger.Log.Info((object) ("Serial Number Device " + device.ToString() + ": " + BitConverter.ToUInt32(this._mCmd.ResponseData, 1).ToString()));
      LoggerNV200.Log.Info((object) ("Serial Number Device " + device.ToString() + ": " + BitConverter.ToUInt32(this._mCmd.ResponseData, 1).ToString()));
    }

    public void GetSerialNumber()
    {
      this._mCmd.CommandData[0] = (byte) 12;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand() || !this.CheckGenericResponses())
        return;
      Array.Reverse((Array) this._mCmd.ResponseData, 1, 4);
      Logger.Log.Info((object) ("Serial Number:" + BitConverter.ToUInt32(this._mCmd.ResponseData, 1).ToString()));
      LoggerNV200.Log.Info((object) ("Serial Number:" + BitConverter.ToUInt32(this._mCmd.ResponseData, 1).ToString()));
    }

    public bool DoPoll()
    {
      if (this._mHoldCount > 0)
      {
        this._mNoteHeld = true;
        --this._mHoldCount;
        this._mCmd.CommandData[0] = (byte) 24;
        this._mCmd.CommandDataLength = (byte) 1;
        Logger.Log.Info((object) ("Note held in escrow: " + this._mHoldCount.ToString()));
        LoggerNV200.Log.Info((object) ("Note held in escrow: " + this._mHoldCount.ToString()));
        return this.SendCommand();
      }
      this._mCmd.CommandData[0] = (byte) 7;
      this._mCmd.CommandDataLength = (byte) 1;
      if (!this.SendCommand())
        return false;
      byte[] b = new byte[(int) byte.MaxValue];
      this._mCmd.ResponseData.CopyTo((Array) b, 0);
      byte responseDataLength = this._mCmd.ResponseDataLength;
      ChannelData d = new ChannelData();
      int num1;
      for (byte index = 1; (int) index < (int) responseDataLength; ++index)
      {
        switch (b[(int) index])
        {
          case 177:
            ILog log1 = Logger.Log;
            num1 = CHelpers.ConvertBytesToInt32(b, (int) index + 2) / 100;
            string message1 = "Detected error with payout device " + num1.ToString() + " " + b[(int) index + 9].ToString();
            log1.Error((object) message1);
            ILog log2 = LoggerNV200.Log;
            num1 = CHelpers.ConvertBytesToInt32(b, (int) index + 2) / 100;
            string message2 = "Detected error with payout device " + num1.ToString() + " " + b[(int) index + 9].ToString();
            log2.Error((object) message2);
            num1 = CHelpers.ConvertBytesToInt32(b, (int) index + 2) / 100;
            DataExchange.SendNv200Status("Detected error with payout device " + num1.ToString() + " " + b[(int) index + 9].ToString());
            this.ErrorDuringPayout((EventArgs) new ListPaymentEvents()
            {
              TotalRejected = (CHelpers.ConvertBytesToInt32(b, (int) index + 2) / 100)
            });
            index += (byte) ((int) b[(int) index + 1] * 7 + 2);
            break;
          case 179:
            Logger.Log.Info((object) "SMART Emptying...");
            LoggerNV200.Log.Info((object) "SMART Emptying...");
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 180:
            Logger.Log.Info((object) "SMART Emptied, getting info...");
            LoggerNV200.Log.Info((object) "SMART Emptied, getting info...");
            DataExchange.SendNv200Status("SMART Emptied, getting info...");
            this.UpdateData();
            this.GetCashboxPayoutOpData();
            this.EnableValidator();
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 194:
            Logger.Log.Info((object) "Emptying");
            LoggerNV200.Log.Info((object) "Emptying");
            DataExchange.SendNv200Status("Emptying");
            break;
          case 195:
            Logger.Log.Info((object) "Emptied");
            LoggerNV200.Log.Info((object) "Emptied");
            DataExchange.SendNv200Status("Emptied");
            this.UpdateData();
            this.EnableValidator();
            break;
          case 198:
            Logger.Log.Info((object) "Payout out of service...");
            LoggerNV200.Log.Info((object) "Payout out of service...");
            break;
          case 201:
            Logger.Log.Info((object) "Note transferred to stacker");
            int num2 = CHelpers.ConvertBytesToInt32(b, (int) index + 1) / 100;
            LoggerNV200.Log.Info((object) ("Note transferred to stacker " + num2.ToString()));
            DataExchange.SendNv200Status("Note transferred to stacker " + num2.ToString());
            this.NoteStacked((EventArgs) new ListPaymentEvents()
            {
              TotalAccepted = num2
            });
            index += (byte) 7;
            break;
          case 202:
            Logger.Log.Info((object) "Note paid into cashbox on startup");
            LoggerNV200.Log.Info((object) "Note paid into cashbox on startup");
            int num3 = CHelpers.ConvertBytesToInt32(b, (int) index + 1) / 100;
            LoggerNV200.Log.Info((object) ("Note paid into cashbox on startup: " + num3.ToString()));
            DataExchange.SendNv200Status("Note paid into cashbox on startup: " + num3.ToString());
            this.NoteStacked((EventArgs) new ListPaymentEvents()
            {
              TotalAccepted = num3
            });
            index += (byte) 7;
            break;
          case 203:
            Logger.Log.Info((object) "Note paid into payout device on startup");
            LoggerNV200.Log.Info((object) "Note paid into payout device on startup");
            index += (byte) 7;
            break;
          case 204:
            Logger.Log.Info((object) "Stacking note");
            LoggerNV200.Log.Info((object) "Stacking note");
            break;
          case 206:
            Logger.Log.Info((object) "Note in bezel...");
            LoggerNV200.Log.Info((object) "Note in bezel...");
            index += (byte) 7;
            break;
          case 210:
            Logger.Log.Info((object) ("Dispensed note(s) - Деньги выданы полностью и забраны пользователем:" + CHelpers.FormatToCurrency(CHelpers.ConvertBytesToInt32(b, (int) index + 2))));
            LoggerNV200.Log.Info((object) ("Dispensed note(s) - Деньги выданы полностью и забраны пользователем:" + CHelpers.FormatToCurrency(CHelpers.ConvertBytesToInt32(b, (int) index + 2))));
            this.UpdateData();
            this.EnableValidator();
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            this.AllNotesDispensed((EventArgs) new ListPaymentEvents()
            {
              TotalRejected = (CHelpers.ConvertBytesToInt32(b, (int) index + 2) / 100)
            });
            break;
          case 213:
            Logger.Log.Info((object) "Unit jammed...");
            LoggerNV200.Log.Info((object) "Unit jammed...");
            DataExchange.SendNv200Status("Unit jammed...");
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 214:
            Logger.Log.Info((object) "Halted...");
            LoggerNV200.Log.Info((object) "Halted...");
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 215:
            Logger.Log.Info((object) "Floating notes");
            LoggerNV200.Log.Info((object) "Floating notes");
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 216:
            Logger.Log.Info((object) "Completed floating");
            LoggerNV200.Log.Info((object) "Completed floating");
            this.GetCashboxPayoutOpData();
            this.UpdateData();
            this.EnableValidator();
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 217:
            Logger.Log.Info((object) "Timed out searching for a note");
            LoggerNV200.Log.Info((object) "Timed out searching for a note");
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 218:
            Logger.Log.Info((object) ("Dispensing note(s):" + CHelpers.FormatToCurrency(CHelpers.ConvertBytesToInt32(b, (int) index + 2))));
            LoggerNV200.Log.Info((object) ("Dispensing note(s):" + CHelpers.FormatToCurrency(CHelpers.ConvertBytesToInt32(b, (int) index + 2))));
            this.NoteDispensed((EventArgs) new ListPaymentEvents()
            {
              TotalRejected = (CHelpers.ConvertBytesToInt32(b, (int) index + 2) / 100)
            });
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 219:
            ILog log3 = Logger.Log;
            num1 = this.Credit;
            string message3 = "Note stored in Payout " + num1.ToString();
            log3.Info((object) message3);
            ILog log4 = LoggerNV200.Log;
            num1 = this.Credit;
            string message4 = "Note stored in Payout " + num1.ToString();
            log4.Info((object) message4);
            this.UpdateData();
            this.NoteProcessed((EventArgs) new PaymentEvent());
            break;
          case 220:
            Logger.Log.Info((object) "Incomplete payout");
            LoggerNV200.Log.Info((object) "Incomplete payout");
            index += (byte) ((int) b[(int) index + 1] * 11 + 1);
            break;
          case 221:
            Logger.Log.Info((object) "Incomplete float");
            LoggerNV200.Log.Info((object) "Incomplete float");
            index += (byte) ((int) b[(int) index + 1] * 11 + 1);
            break;
          case 225:
            Logger.Log.Info((object) "Note cleared from front of validator");
            LoggerNV200.Log.Info((object) "Note cleared from front of validator");
            ++index;
            break;
          case 226:
            Logger.Log.Info((object) "Note cleared to cashbox");
            LoggerNV200.Log.Info((object) "Note cleared to cashbox");
            ++index;
            break;
          case 227:
            Logger.Log.Info((object) "Cashbox removed");
            LoggerNV200.Log.Info((object) "Cashbox removed");
            DataExchange.SendNv200Status("Cashbox removed");
            break;
          case 228:
            Logger.Log.Info((object) "Cashbox replaced");
            LoggerNV200.Log.Info((object) "Cashbox replaced");
            DataExchange.SendNv200Status("Cashbox replaced");
            break;
          case 230:
            Logger.Log.Info((object) "Fraud attempt");
            LoggerNV200.Log.Info((object) "Fraud attempt");
            index += (byte) ((int) b[(int) index + 1] * 7 + 1);
            break;
          case 231:
            Logger.Log.Info((object) "Stacker full");
            LoggerNV200.Log.Info((object) "Stacker full");
            DataExchange.SendNv200Status("Stacker full");
            break;
          case 232:
            Logger.Log.Info((object) "Unit disabled...");
            LoggerNV200.Log.Info((object) "Unit disabled...");
            break;
          case 233:
            Logger.Log.Info((object) "Unsafe jam");
            LoggerNV200.Log.Info((object) "Unsafe jam");
            DataExchange.SendNv200Status("Unsafe jam");
            break;
          case 234:
            Logger.Log.Info((object) "Safe jam");
            LoggerNV200.Log.Info((object) "Safe jam");
            DataExchange.SendNv200Status("Safe jam");
            break;
          case 235:
            ILog log5 = Logger.Log;
            num1 = this.Credit;
            string message5 = "Note stacked " + num1.ToString();
            log5.Info((object) message5);
            ILog log6 = LoggerNV200.Log;
            num1 = this.Credit;
            string message6 = "Note stacked " + num1.ToString();
            log6.Info((object) message6);
            this.NoteStacked((EventArgs) new ListPaymentEvents()
            {
              TotalAccepted = this.Credit
            });
            this.NoteProcessed((EventArgs) new PaymentEvent());
            break;
          case 236:
            Logger.Log.Info((object) "Note rejected");
            LoggerNV200.Log.Info((object) "Note rejected");
            this.QueryRejection();
            this.NoteProcessed((EventArgs) new PaymentEvent());
            break;
          case 237:
            Logger.Log.Info((object) "Rejecting note");
            LoggerNV200.Log.Info((object) "Rejecting note");
            break;
          case 238:
            this.GetDataByChannel((int) b[(int) index + 1], ref d);
            Logger.Log.Info((object) ("Credit " + CHelpers.FormatToCurrency(d.Value)));
            LoggerNV200.Log.Info((object) ("Credit " + CHelpers.FormatToCurrency(d.Value)));
            this.UpdateData();
            this.NoteInserted((EventArgs) new ListPaymentEvents()
            {
              TotalAccepted = (d.Value / 100)
            });
            ++index;
            break;
          case 239:
            if (this._mCmd.ResponseData[(int) index + 1] > (byte) 0)
            {
              this.GetDataByChannel((int) b[(int) index + 1], ref d);
              Logger.Log.Info((object) ("Note in escrow, amount: " + CHelpers.FormatToCurrency(d.Value)));
              LoggerNV200.Log.Info((object) ("Note in escrow, amount: " + CHelpers.FormatToCurrency(d.Value)));
              if (d.Value == 500000)
              {
                Logger.Log.Info((object) ("5000 не принимаем" + CHelpers.FormatToCurrency(d.Value)));
                LoggerNV200.Log.Info((object) ("5000 не принимаем" + CHelpers.FormatToCurrency(d.Value)));
                this._mCmd.CommandData[0] = (byte) 8;
                this._mCmd.CommandDataLength = (byte) 1;
                this.SendCommand();
              }
              else if (d.Value == 1000)
              {
                Logger.Log.Info((object) ("10 не принимаем" + CHelpers.FormatToCurrency(d.Value)));
                LoggerNV200.Log.Info((object) ("10 не принимаем" + CHelpers.FormatToCurrency(d.Value)));
                this._mCmd.CommandData[0] = (byte) 8;
                this._mCmd.CommandDataLength = (byte) 1;
                this.SendCommand();
              }
              else
              {
                this.Credit = d.Value / 100;
                this._mHoldCount = this._mHoldNumber;
              }
            }
            else
            {
              Logger.Log.Info((object) "Reading note");
              LoggerNV200.Log.Info((object) "Reading note");
            }
            this.NoteReading((EventArgs) new PaymentEvent());
            ++index;
            break;
          case 241:
            Logger.Log.Info((object) "Unit reset");
            LoggerNV200.Log.Info((object) "Unit reset");
            this.UpdateData();
            break;
          default:
            ILog log7 = Logger.Log;
            num1 = (int) this._mCmd.ResponseData[(int) index];
            string message7 = "Unsupported poll response received: " + num1.ToString();
            log7.Info((object) message7);
            ILog log8 = LoggerNV200.Log;
            num1 = (int) this._mCmd.ResponseData[(int) index];
            string message8 = "Unsupported poll response received: " + num1.ToString();
            log8.Info((object) message8);
            break;
        }
      }
      return true;
    }

    public string GetChannelLevelInfo()
    {
      string channelLevelInfo = "";
      foreach (ChannelData mUnitData in this._mUnitDataList)
      {
        channelLevelInfo = channelLevelInfo + ((float) mUnitData.Value / 100f).ToString() + " " + mUnitData.Currency[0].ToString() + mUnitData.Currency[1].ToString() + mUnitData.Currency[2].ToString();
        channelLevelInfo = channelLevelInfo + " [" + mUnitData.Level.ToString() + "] = " + ((float) (mUnitData.Level * mUnitData.Value) / 100f).ToString();
        channelLevelInfo = channelLevelInfo + " " + mUnitData.Currency[0].ToString() + mUnitData.Currency[1].ToString() + mUnitData.Currency[2].ToString();
      }
      return channelLevelInfo;
    }

    public List<Tuple<int, int>> GetChannelLevelInfo2()
    {
      this.UpdateData();
      return this._mUnitDataList.Select<ChannelData, Tuple<int, int>>((Func<ChannelData, Tuple<int, int>>) (d => new Tuple<int, int>(d.Value / 100, d.Level))).ToList<Tuple<int, int>>();
    }

    public List<ChannelLevelInfo> GetChannelLevelInfo3()
    {
      this.UpdateData();
      return this._mUnitDataList.Select<ChannelData, ChannelLevelInfo>((Func<ChannelData, ChannelLevelInfo>) (d => new ChannelLevelInfo()
      {
        Note = d.Value / 100,
        Count = (short) (sbyte) d.Level
      })).ToList<ChannelLevelInfo>();
    }

    public void UpdateData()
    {
      foreach (ChannelData mUnitData in this._mUnitDataList)
      {
        mUnitData.Level = this.CheckNoteLevel(mUnitData.Value, mUnitData.Currency);
        this.IsNoteRecycling(mUnitData.Value, mUnitData.Currency, ref mUnitData.Recycling);
      }
    }

    public void GetDataByChannel(int channel, ref ChannelData d)
    {
      foreach (ChannelData mUnitData in this._mUnitDataList)
      {
        if ((int) mUnitData.Channel == channel)
        {
          d = mUnitData;
          break;
        }
      }
    }

    private bool CheckGenericResponses()
    {
      if (this._mCmd.ResponseData[0] == (byte) 240)
        return true;
      switch (this._mCmd.ResponseData[0])
      {
        case 242:
          Logger.Log.Info((object) "Command response is NOT KNOWN");
          LoggerNV200.Log.Info((object) "Command response is NOT KNOWN");
          return false;
        case 243:
          Logger.Log.Info((object) "Command response is WRONG NUMBER OF PARAMETERS");
          LoggerNV200.Log.Info((object) "Command response is WRONG NUMBER OF PARAMETERS");
          return false;
        case 244:
          Logger.Log.Info((object) "Command response is PARAM OUT OF RANGE");
          LoggerNV200.Log.Info((object) "Command response is PARAM OUT OF RANGE");
          return false;
        case 245:
          if (this._mCmd.ResponseData[1] == (byte) 3)
          {
            Logger.Log.Info((object) "Unit responded with a \"Busy\" response, command cannot be processed at this time");
            LoggerNV200.Log.Info((object) "Unit responded with a \"Busy\" response, command cannot be processed at this time");
          }
          else
          {
            Logger.Log.Info((object) ("Command response is CANNOT PROCESS COMMAND, error code - 0x" + BitConverter.ToString(this._mCmd.ResponseData, 1, 1)));
            LoggerNV200.Log.Info((object) ("Command response is CANNOT PROCESS COMMAND, error code - 0x" + BitConverter.ToString(this._mCmd.ResponseData, 1, 1)));
          }
          return false;
        case 246:
          Logger.Log.Info((object) "Command response is SOFTWARE ERROR");
          LoggerNV200.Log.Info((object) "Command response is SOFTWARE ERROR");
          return false;
        case 248:
          Logger.Log.Info((object) "Command response is FAIL");
          LoggerNV200.Log.Info((object) "Command response is FAIL");
          return false;
        case 250:
          Logger.Log.Info((object) "Command response is KEY NOT SET, renegotiate keys");
          LoggerNV200.Log.Info((object) "Command response is KEY NOT SET, renegotiate keys");
          return false;
        default:
          return false;
      }
    }

    public bool SendCommand()
    {
      this._mCmd.CommandData.CopyTo((Array) new byte[(int) byte.MaxValue], 0);
      if (this._mESsp.SSPSendCommand(this._mCmd, this._info) || this._mESsp.SSPSendCommand(this._mCmd, this._info))
        return true;
      this._mESsp.CloseComPort();
      Logger.Log.Info((object) ("Sending command failedPort status: " + this._mCmd.ResponseStatus.ToString()));
      LoggerNV200.Log.Info((object) ("Sending command failedPort status: " + this._mCmd.ResponseStatus.ToString()));
      return false;
    }

    public void SetRoutes()
    {
      this.EnablePayout();
      List<ChannelLevelInfo> channelLevelInfo3 = this.GetChannelLevelInfo3();
      short num1 = channelLevelInfo3.Where<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == 10)).Select<ChannelLevelInfo, short>((Func<ChannelLevelInfo, short>) (c => c.Count)).FirstOrDefault<short>();
      short num2 = channelLevelInfo3.Where<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == 50)).Select<ChannelLevelInfo, short>((Func<ChannelLevelInfo, short>) (c => c.Count)).FirstOrDefault<short>();
      short num3 = channelLevelInfo3.Where<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == 100)).Select<ChannelLevelInfo, short>((Func<ChannelLevelInfo, short>) (c => c.Count)).FirstOrDefault<short>();
      channelLevelInfo3.Where<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == 200)).Select<ChannelLevelInfo, short>((Func<ChannelLevelInfo, short>) (c => c.Count)).FirstOrDefault<short>();
      short num4 = channelLevelInfo3.Where<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == 500)).Select<ChannelLevelInfo, short>((Func<ChannelLevelInfo, short>) (c => c.Count)).FirstOrDefault<short>();
      short num5 = channelLevelInfo3.Where<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == 1000)).Select<ChannelLevelInfo, short>((Func<ChannelLevelInfo, short>) (c => c.Count)).FirstOrDefault<short>();
      channelLevelInfo3.Where<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == 2000)).Select<ChannelLevelInfo, short>((Func<ChannelLevelInfo, short>) (c => c.Count)).FirstOrDefault<short>();
      short num6 = channelLevelInfo3.Where<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == 5000)).Select<ChannelLevelInfo, short>((Func<ChannelLevelInfo, short>) (c => c.Count)).FirstOrDefault<short>();
      this.ChangeNoteRoute(1000, "RUB".ToCharArray(), num1 >= (short) 0);
      this.ChangeNoteRoute(5000, "RUB".ToCharArray(), num2 >= (short) 25);
      this.ChangeNoteRoute(10000, "RUB".ToCharArray(), num3 >= (short) 25);
      this.ChangeNoteRoute(50000, "RUB".ToCharArray(), num4 >= (short) 19);
      this.ChangeNoteRoute(100000, "RUB".ToCharArray(), num5 >= (short) 0);
      this.ChangeNoteRoute(500000, "RUB".ToCharArray(), num6 >= (short) 0);
    }
  }
}
