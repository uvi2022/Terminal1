// Decompiled with JetBrains decompiler
// Type: UIPSCore.Hardware.CardPayment.ICardPaymentDevice
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using UIPSCore.Interior.Businesses;

namespace UIPSCore.Hardware.CardPayment
{
  public interface ICardPaymentDevice
  {
    bool IsActive { get; }

    void CloseDay(out string errorText);

    string GetReportErrorText(int reportCode);

    string GetStatistics(out string errorText);

    string GetTerminalID(out string errorText);

    uint GetVer(out string errorText);

    void SetUpdateUi(Action<uint, string, bool> updateUi);

    void StartCardProcessing(
      IBusiness business,
      Decimal expectedMoney,
      Func<bool> isStoppedFunc,
      Action<string> processCompleteFunc);

    void StopCardProcessing_1();

    void StopCardProcessing_2();

    void StopCardProcessing_EjectCard();
  }
}
