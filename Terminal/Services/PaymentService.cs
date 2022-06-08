// Decompiled with JetBrains decompiler
// Type: Terminal.Services.PaymentService
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using Terminal.Classes;

namespace Terminal.Services
{
  public sealed class PaymentService
  {
    private static readonly System.Lazy<PaymentService> Lazy = new System.Lazy<PaymentService>((Func<PaymentService>) (() => new PaymentService()));
    private static readonly DispatcherTimer Timer = new DispatcherTimer();
    private static readonly Mutex Mutex = new Mutex();

    public static PaymentService Instance => PaymentService.Lazy.Value;

    public void Init()
    {
    }

    private PaymentService()
    {
      PaymentService.Timer.Tick += new EventHandler(this.OnTimerTick);
      PaymentService.Timer.Interval = new TimeSpan(0, 0, Global.PaymentSendInterval);
      PaymentService.Timer.Start();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
      if (this.IsBusy)
        return;
      try
      {
        this.IsBusy = true;
        foreach (Purchase unuploadedPayment in PaymentService.GetUnuploadedPayments())
          this.SendPayment(unuploadedPayment);
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.IsBusy = false;
      }
    }

    private static List<Purchase> GetUnuploadedPayments() => StorageService.Instance.Data.Purchase.Where<Purchase>((Func<Purchase, bool>) (x => !(StorageService.Instance.Data.PurchaseUploaded ?? new List<Guid>()).Contains(x.Guid))).ToList<Purchase>();

    public bool IsBusy { get; set; }

    public async void SendPayment(Purchase ps)
    {
      ps.CashboxInfo = StorageService.Instance.Data.Stacked;
      PurchaseResponce responce = await DataExchange.SendPayment(ps);
      if (responce.Status != 0)
      {
        responce = (PurchaseResponce) null;
      }
      else
      {
        PaymentService.SetStatusOk(responce);
        responce = (PurchaseResponce) null;
      }
    }

    private static void SetStatusOk(PurchaseResponce r)
    {
      StorageService.Instance.Data.Add(r);
      StorageService.Instance.Save();
    }

    private static void SavePayment(Purchase ps)
    {
      if (ps == null)
        return;
      StorageService.Instance.Data.Add(ps);
      StorageService.Instance.Save();
    }

    public void CreatePayment(
      int terminalId,
      PaymentEvent pe,
      ListPaymentEvents lpe,
      List<ChannelLevelInfo> info)
    {
      Purchase ps = new Purchase(terminalId, pe, lpe, info);
      PaymentService.SavePayment(ps);
      this.SendPayment(ps);
    }

    public void CreatePayment(Purchase ps)
    {
      PaymentService.SavePayment(ps);
      this.SendPayment(ps);
    }

    public List<Purchase> GetPaidServicesForIncas()
    {
      List<Guid> en = (StorageService.Instance.Data.Encashment ?? new List<Encashment>()).SelectMany<Encashment, Guid>((Func<Encashment, IEnumerable<Guid>>) (x => (IEnumerable<Guid>) x.Encashed)).ToList<Guid>();
      return (StorageService.Instance.Data.Purchase ?? new List<Purchase>()).ToList<Purchase>().Where<Purchase>((Func<Purchase, bool>) (x => !en.Contains(x.Guid) && x.PaymentType == (short) 1)).ToList<Purchase>();
    }

    public PurchaseResponce SendEncashment(Encashment incas)
    {
      try
      {
        WebClient webClient = new WebClient()
        {
          Encoding = Encoding.UTF8
        };
        Uri address = new Uri(Global.WebApiUrl + "/encashment/");
        string data = JsonConvert.SerializeObject((object) incas);
        webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
        PurchaseResponce purchaseResponce = JsonConvert.DeserializeObject<PurchaseResponce>(webClient.UploadString(address, "PUT", data));
        Logger.Log.Info((object) ("Отправлена информация о инкассации: " + data.ToString()));
        return purchaseResponce;
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) "Не удалось отправить информацию о инкассации");
        Logger.Log.Error((object) ex.ToString());
        return new PurchaseResponce()
        {
          Guid = incas.Guid,
          Status = 1,
          StatusMessage = "Error"
        };
      }
    }

    public Encashment GetPreviousEncashment() => (StorageService.Instance.Data.Encashment ?? new List<Encashment>()).OrderByDescending<Encashment, DateTime>((Func<Encashment, DateTime>) (x => x.Date)).FirstOrDefault<Encashment>() ?? new Encashment();

    public void SaveEncashment(Encashment incas)
    {
      StorageService.Instance.Data.Add(incas);
      StorageService.Instance.Save();
    }
  }
}
