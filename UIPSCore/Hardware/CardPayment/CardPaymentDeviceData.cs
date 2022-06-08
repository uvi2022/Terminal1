// Decompiled with JetBrains decompiler
// Type: UIPSCore.Hardware.CardPayment.CardPaymentDeviceData
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using UIPSCore.Interior.Businesses;

namespace UIPSCore.Hardware.CardPayment
{
  internal class CardPaymentDeviceData
  {
    public IBusiness Business;
    public Decimal ExpectedMoney;
    public Func<bool> IsStoppedFunc;
    public Action<string> ProcessCompleteFunc;
  }
}
