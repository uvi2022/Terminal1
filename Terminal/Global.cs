// Decompiled with JetBrains decompiler
// Type: Terminal.Global
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Web.Configuration;

namespace Terminal
{
  public static class Global
  {
    public static string Nv200ComPort = WebConfigurationManager.AppSettings[nameof (Nv200ComPort)];
    public static string VkpComPort = WebConfigurationManager.AppSettings[nameof (VkpComPort)];
    public static int VkpBaudRate = int.Parse(WebConfigurationManager.AppSettings[nameof (VkpBaudRate)]);
    public static string DllPath = WebConfigurationManager.AppSettings[nameof (DllPath)];
    public static byte Nv200SSPAddress = 0;
    public static uint Nv200Timeout = 1500;
    public static int BaudRate = 9600;
    public static int Nv200ReconnectionAttempts = 15;
    public static string WebApiUrl = WebConfigurationManager.AppSettings[nameof (WebApiUrl)];
    public static int ServicesCheckerInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (ServicesCheckerInterval)]);
    public static int DateCheckerInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (DateCheckerInterval)]);
    public static int PrintCheckerInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (PrintCheckerInterval)]);
    public static int CheckServerInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (CheckServerInterval)]);
    public static int CheckDatabaseInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (CheckDatabaseInterval)]);
    public static int CheckPayoutInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (CheckPayoutInterval)]);
    public static int PaymentSendInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (PaymentSendInterval)]);
    public static int CheckPinpadInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (CheckPinpadInterval)]);
    public static int ClearCartInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (ClearCartInterval)]) == 0 ? 40 : Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (ClearCartInterval)]);
    public static int CheckUsersInterval = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (CheckUsersInterval)]) == 0 ? 120 : Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (CheckUsersInterval)]);
    public static string JsonDirectory = "json";
    public static string Currency = "руб.";
    public static string StorageFilename = "json\\storage.json";
    public static string UmkaUrl = WebConfigurationManager.AppSettings[nameof (UmkaUrl)] ?? (string) null;
    public static string UmkaLogin = WebConfigurationManager.AppSettings[nameof (UmkaLogin)] ?? (string) null;
    public static string UmkaPassword = WebConfigurationManager.AppSettings[nameof (UmkaPassword)] ?? (string) null;
    public static int Tax = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (Tax)]) == 0 ? 8 : Convert.ToInt32(WebConfigurationManager.AppSettings[nameof (Tax)]);
    public static bool ShowCart = WebConfigurationManager.AppSettings[nameof (ShowCart)] != null && Convert.ToBoolean(WebConfigurationManager.AppSettings[nameof (ShowCart)]);
    public static bool ShowRadio = WebConfigurationManager.AppSettings[nameof (ShowRadio)] != null && Convert.ToBoolean(WebConfigurationManager.AppSettings[nameof (ShowRadio)]);

    public static string Version => "1.9.1.12";

    public static int TerminalId => int.Parse(WebConfigurationManager.AppSettings[nameof (TerminalId)]);
  }
}
