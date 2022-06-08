// Decompiled with JetBrains decompiler
// Type: UIPSCore.Logging.ITerminalEventLog
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using UIPSCore.Interior.Businesses;

namespace UIPSCore.Logging
{
  public interface ITerminalEventLog
  {
    void InsertBillsDispensed(
      int dispensedBills,
      int rejectedBills,
      bool isNearEnd,
      string pnr,
      Decimal balanceBefore);

    void InsertDispenserDecassation(
      Decimal moneyInserted,
      string techLogin,
      int checkNumber,
      Decimal balanceBefore);

    void InsertError(string errorText);

    void InsertIncassation(
      Decimal dispenserMoneyTaken,
      Decimal cashCodeMoneyTaken,
      string techLogin,
      int checkNumber,
      Decimal balanceBefore);

    void InsertInfo(string text);

    void InsertInternetState(bool isNowOnline);

    void InsertMoneyEated(
      IBusiness business,
      Decimal eated,
      Decimal expected,
      string pnr,
      Decimal balanceBefore);

    void InsertMoneyEated(IBusiness business, Decimal eated, string pnr, Decimal balanceBefore);

    void InsertNeedService(NeedServiceReason reason, string reasonText);

    void InsertZOrder(bool silently, string answer);
  }
}
