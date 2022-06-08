// Decompiled with JetBrains decompiler
// Type: UIPSCore.Hardware.CardPayment.PilotNtCardTypesHelpers
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace UIPSCore.Hardware.CardPayment
{
  public static class PilotNtCardTypesHelpers
  {
    public static string PilotNtCardTypesHumanText(this PilotNtCardTypes pilotNtCardType)
    {
      switch (pilotNtCardType)
      {
        case PilotNtCardTypes.CT_USER:
          return "Выбор из меню";
        case PilotNtCardTypes.CT_VISA:
          return "VISA";
        case PilotNtCardTypes.CT_EUROCARD:
          return "Eurocard/Mastercard";
        case PilotNtCardTypes.CT_CIRRUS:
          return "Cirrus/Maestro";
        case PilotNtCardTypes.CT_AMEX:
          return "American Express";
        case PilotNtCardTypes.CT_DINERS:
          return "Diners Club";
        case PilotNtCardTypes.CT_ELECTRON:
          return "VISA Electron";
        case PilotNtCardTypes.CT_PRO100:
          return "PRO100";
        case PilotNtCardTypes.CT_CASHIER:
          return "Cashier card";
        case PilotNtCardTypes.CT_SBERCARD:
          return "Sbercard";
        default:
          return "Unknown";
      }
    }
  }
}
