// Decompiled with JetBrains decompiler
// Type: Terminal.Services.PrintService
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using Core.Classes.FiscalCheck;
using PayVKP80;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Windows.Threading;
using Terminal.Classes;

namespace Terminal.Services
{
  public sealed class PrintService
  {
    private static readonly System.Lazy<PrintService> Lazy = new System.Lazy<PrintService>((Func<PrintService>) (() => new PrintService()));
    private static readonly DispatcherTimer Timer = new DispatcherTimer();

    public static PrintService Instance => PrintService.Lazy.Value;

    public event PrintService.ChangedEventHandler OnPrinterError;

    public event PrintService.ChangedEventHandler OnPrinterOk;

    private void PrinterError(EventArgs e)
    {
      PrintService.ChangedEventHandler onPrinterError = this.OnPrinterError;
      if (onPrinterError == null)
        return;
      onPrinterError((object) this, e);
    }

    private void PrinterOk(EventArgs e)
    {
      PrintService.ChangedEventHandler onPrinterOk = this.OnPrinterOk;
      if (onPrinterOk == null)
        return;
      onPrinterOk((object) this, e);
    }

    private PrintService()
    {
      PrintService.Timer.Tick += new EventHandler(this.OnTimerTick);
      PrintService.Timer.Interval = new TimeSpan(0, 0, Global.PrintCheckerInterval);
      PrintService.Timer.Start();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
      if (this.GetStatus() != 0)
        this.PrinterError(new EventArgs());
      else
        this.PrinterOk(new EventArgs());
    }

    public int GetStatus()
    {
      string str = (string) null;
      try
      {
        SerialPort serialPort = new SerialPort(Global.VkpComPort, Global.VkpBaudRate);
        if (serialPort != null)
        {
          serialPort.Open();
          Logger.Log.Info((object) string.Format("Порт принтера {0} открыт на скорости {1}", (object) Global.VkpComPort, (object) Global.VkpBaudRate));
          if (serialPort.IsOpen)
          {
            byte[] buffer = new byte[3]
            {
              (byte) 16,
              (byte) 4,
              (byte) 20
            };
            serialPort.Write(buffer, 0, buffer.Length);
            foreach (byte num in serialPort.ReadExisting())
              str = str + num.ToString() + " ";
            Logger.Log.Info((object) str);
            serialPort.Close();
          }
          DataExchange.SendPrinterStatus(str);
          return 0;
        }
        Logger.Log.Warn((object) string.Format("Не удалось открыть Порт принтера {0} открыт на скорости {1}", (object) Global.VkpComPort, (object) Global.VkpBaudRate));
        DataExchange.SendPrinterStatus(str);
        return -1;
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) "Ошибка порта принтера");
        Logger.Log.Error((object) ex.ToString());
        return ex.HResult;
      }
    }

    public void PrintReceipt(ListPaymentEvents lpe, int bonus)
    {
      Core.Classes.Check check = new Core.Classes.Check(lpe, bonus);
      try
      {
        PrintService.Timer.Stop();
        check.FiscalCheck = this.GetFiscalCheck(lpe);
        SerialPort serialPort = new SerialPort(Global.VkpComPort, Global.VkpBaudRate);
        if (serialPort != null)
        {
          if (serialPort.IsOpen)
          {
            serialPort.Close();
            Logger.Log.Info((object) "Закрываем порт принтера");
          }
          serialPort.Open();
          Logger.Log.Info((object) "Открываем порт принтера");
          if (serialPort.IsOpen)
          {
            ThermalPrinter thermalPrinter = new ThermalPrinter(serialPort, (byte) 2, (byte) 180, (byte) 2);
            Logger.Log.Info((object) "Печатаем чек");
            thermalPrinter.Print(check);
            serialPort.Close();
            Logger.Log.Info((object) "Закрываем порт принтера");
          }
          else
            Logger.Log.Error((object) "Не удалось открыть порт принтера");
        }
        else
          Logger.Log.Error((object) "Ошибка порта принтера");
        PrintService.Timer.Start();
      }
      catch
      {
      }
    }

    public void PrintCopy(Core.Classes.Check check)
    {
      try
      {
        PrintService.Timer.Stop();
        SerialPort serialPort = new SerialPort(Global.VkpComPort, Global.VkpBaudRate);
        if (serialPort != null)
        {
          if (serialPort.IsOpen)
          {
            serialPort.Close();
            Logger.Log.Info((object) "Закрываем порт принтера");
          }
          serialPort.Open();
          Logger.Log.Info((object) "Открываем порт принтера");
          if (serialPort.IsOpen)
          {
            ThermalPrinter thermalPrinter = new ThermalPrinter(serialPort, (byte) 2, (byte) 180, (byte) 2);
            Logger.Log.Info((object) "Печатаем чек");
            thermalPrinter.PrintCheckCopy(check);
            serialPort.Close();
            Logger.Log.Info((object) "Закрываем порт принтера");
          }
          else
            Logger.Log.Error((object) "Не удалось открыть порт принтера");
        }
        else
          Logger.Log.Error((object) "Ошибка порта принтера");
        PrintService.Timer.Start();
      }
      catch
      {
        throw;
      }
    }

    public void PrintBankCheck(string check)
    {
      try
      {
        SerialPort serialPort = new SerialPort(Global.VkpComPort, Global.VkpBaudRate);
        if (serialPort == null)
          return;
        if (serialPort.IsOpen)
          serialPort.Close();
        serialPort.Open();
        if (serialPort.IsOpen)
        {
          new ThermalPrinter(serialPort, (byte) 2, (byte) 180, (byte) 2).Print(check);
          serialPort.Close();
        }
      }
      catch
      {
      }
    }

    private FiscalCheckResponce GetFiscalCheck(ListPaymentEvents lpe)
    {
      if (string.IsNullOrEmpty(Global.UmkaUrl) | string.IsNullOrEmpty(Global.UmkaLogin) | string.IsNullOrEmpty(Global.UmkaPassword))
        return (FiscalCheckResponce) null;
      try
      {
        WebClient webClient1 = new WebClient()
        {
          Encoding = Encoding.UTF8
        };
        string base64String = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Global.UmkaLogin + ":" + Global.UmkaPassword));
        webClient1.Headers.Add("Authorization", "Basic " + base64String);
        long? nullable = FiscalCheckStatus.FromJson(webClient1.DownloadString(Global.UmkaUrl + "/cashboxstatus.json")).CashboxStatus.FsStatus.CycleIsOpen;
        long num1 = 1;
        if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        {
          Logger.Log.Info((object) "Смена открыта");
        }
        else
        {
          Logger.Log.Info((object) "Смена закрыта");
          Logger.Log.Info((object) "Открываем смену");
          nullable = FiscalCheckCycleOpen.FromJson(webClient1.DownloadString(Global.UmkaUrl + "/cycleopen.json")).Document.Result;
          long num2 = 0;
          if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          {
            Logger.Log.Info((object) "Смена открыта");
          }
          else
          {
            Logger.Log.Error((object) "Не удалось открыть смену");
            return (FiscalCheckResponce) null;
          }
        }
        FiscalCheckRequest self1 = new FiscalCheckRequest()
        {
          Document = new Document()
          {
            SessionId = Guid.NewGuid().ToString(),
            Data = new Data()
            {
              Type = new long?(1L),
              Sum = new long?((long) (lpe.TotalAccepted * 100))
            }
          }
        };
        if (lpe.PaymentType == (short) 1)
          self1.Document.Data.MoneyType = new long?(1L);
        else if (lpe.PaymentType == (short) 3)
          self1.Document.Data.MoneyType = new long?(2L);
        if (lpe.PaymentType == (short) 2)
          self1.Document.Data.MoneyType = new long?(5L);
        self1.Document.Data.Fiscprops = new List<DataFiscprop>();
        self1.Document.Data.Fiscprops.Add(new DataFiscprop()
        {
          Tag = new long?(1054L),
          Value = new Value?((Value) 1L)
        });
        self1.Document.Data.Fiscprops.Add(new DataFiscprop()
        {
          Tag = new long?(1055L),
          Value = new Value?((Value) (long) Global.Tax)
        });
        self1.Document.Data.Fiscprops.Add(new DataFiscprop()
        {
          Tag = new long?(1008L),
          Value = new Value?((Value) lpe.Phone)
        });
        foreach (CartItem cartItem in lpe.Cart.Items)
        {
          List<FiscpropFiscprop> fiscpropFiscpropList = new List<FiscpropFiscprop>();
          fiscpropFiscpropList.Add(new FiscpropFiscprop()
          {
            Tag = new long?(1214L),
            Value = new Value?((Value) 4L)
          });
          if (cartItem.ServiceItem != null)
          {
            fiscpropFiscpropList.Add(new FiscpropFiscprop()
            {
              Tag = new long?(1212L),
              Value = new Value?((Value) 4L)
            });
            fiscpropFiscpropList.Add(new FiscpropFiscprop()
            {
              Tag = new long?(1030L),
              Value = new Value?((Value) cartItem.ServiceItem.Name)
            });
            fiscpropFiscpropList.Add(new FiscpropFiscprop()
            {
              Tag = new long?(1079L),
              Value = new Value?((Value) (long) (cartItem.ServiceItem.Price * 100))
            });
          }
          else if (cartItem.ProductItem != null)
          {
            fiscpropFiscpropList.Add(new FiscpropFiscprop()
            {
              Tag = new long?(1212L),
              Value = new Value?((Value) 1L)
            });
            fiscpropFiscpropList.Add(new FiscpropFiscprop()
            {
              Tag = new long?(1030L),
              Value = new Value?((Value) cartItem.ProductItem.Name)
            });
            fiscpropFiscpropList.Add(new FiscpropFiscprop()
            {
              Tag = new long?(1079L),
              Value = new Value?((Value) (long) (cartItem.ProductItem.Price * 100))
            });
          }
          fiscpropFiscpropList.Add(new FiscpropFiscprop()
          {
            Tag = new long?(1023L),
            Value = new Value?((Value) cartItem.Count.ToString("F3", (IFormatProvider) CultureInfo.GetCultureInfo("en-us")))
          });
          fiscpropFiscpropList.Add(new FiscpropFiscprop()
          {
            Tag = new long?(1199L),
            Value = new Value?((Value) 6L)
          });
          self1.Document.Data.Fiscprops.Add(new DataFiscprop()
          {
            Tag = new long?(1059L),
            Fiscprops = fiscpropFiscpropList
          });
        }
        Logger.Log.Info((object) "Отправляем чек");
        WebClient webClient2 = new WebClient()
        {
          Encoding = Encoding.UTF8
        };
        webClient2.Headers.Add("Authorization", "Basic " + base64String);
        webClient2.Headers.Add(HttpRequestHeader.ContentType, "application/json");
        string json = webClient2.UploadString(Global.UmkaUrl + "/fiscalcheck.json", "POST", self1.ToJson());
        Logger.Log.Info((object) self1.ToJson());
        FiscalCheckResponce self2 = FiscalCheckResponce.FromJson(json);
        Logger.Log.Info((object) self2.ToJson());
        long? result = self2.Document.Result;
        long num3 = 0;
        return result.GetValueOrDefault() == num3 & result.HasValue ? self2 : (FiscalCheckResponce) null;
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
        return (FiscalCheckResponce) null;
      }
    }

    public void PrintZFReports()
    {
    }

    public void PrintIncas(string[] text)
    {
      try
      {
        SerialPort serialPort = new SerialPort(Global.VkpComPort, Global.VkpBaudRate);
        if (serialPort.IsOpen)
          serialPort.Close();
        serialPort.Open();
        if (!serialPort.IsOpen)
          return;
        ThermalPrinter thermalPrinter = new ThermalPrinter(serialPort, (byte) 2, (byte) 180, (byte) 2);
        string[] info = WebConfigurationManager.AppSettings["ChequeInfo"].Split('|');
        thermalPrinter.PrintIncas(text, info);
        serialPort.Close();
      }
      catch
      {
      }
    }

    public void Cut()
    {
    }

    public delegate void ChangedEventHandler(object sender, EventArgs e);
  }
}
