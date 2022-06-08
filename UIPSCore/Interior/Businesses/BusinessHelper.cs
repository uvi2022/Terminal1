// Decompiled with JetBrains decompiler
// Type: UIPSCore.Interior.Businesses.BusinessHelper
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace UIPSCore.Interior.Businesses
{
  public static class BusinessHelper
  {
    public static string GetNotDetailedErrorText(this IBusiness business, string agentAddress) => "Произошла ошибка, " + business.CheckPrinterBusinessFailedText.ToLower() + ", ваши данные сохранены, возьмите все чеки и подойдите в офис по адресу " + agentAddress;

    public static string GetNotEnoughDispenseErrorText(this IBusiness business, string agentAddress) => "В терминале недостаточно сдачи, ваши данные сохранены, возьмите все чеки и подойдите в офис по адресу " + agentAddress;
  }
}
