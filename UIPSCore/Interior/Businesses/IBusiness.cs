// Decompiled with JetBrains decompiler
// Type: UIPSCore.Interior.Businesses.IBusiness
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace UIPSCore.Interior.Businesses
{
  public interface IBusiness
  {
    string CheckPrinterBusinessDoneText { get; }

    string CheckPrinterBusinessFailedText { get; }

    string CheckPrinterText { get; set; }

    string CheckPrinterText2 { get; set; }

    string Date { get; }

    string PayingTotalMoneyMessage { get; }

    string Price { get; }

    string SystemID { get; }

    uint TicketCount { get; }

    int TypeClose { get; }
  }
}
