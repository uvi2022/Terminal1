// Decompiled with JetBrains decompiler
// Type: Terminal.Services.PayoutService
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Threading;
using Terminal.Classes;
using Terminal.NV200;

namespace Terminal.Services
{
  public sealed class PayoutService
  {
    private static readonly System.Lazy<PayoutService> Lazy = new System.Lazy<PayoutService>((Func<PayoutService>) (() => new PayoutService()));
    private static BackgroundWorker _backgroundWorker;
    private readonly System.Timers.Timer _timer;
    public static readonly DispatcherTimer CheckPayoutTimer = new DispatcherTimer();
    private readonly CPayout _payout;
    private readonly System.Timers.Timer _waitTimer;
    private readonly int _waitTimerInterval = 7000;
    private int pollTimer = 250;
    private int _totalAccepted = 0;
    private int _change = 0;
    private int _totalRejected = 0;
    private bool Running = true;

    public static PayoutService Instance => PayoutService.Lazy.Value;

    public event ChangedEventHandler OnNoteInserted;

    public event ChangedEventHandler OnPaymentComplete;

    public event ChangedEventHandler OnNotEnoughChange;

    public event ChangedEventHandler OnPayoutError;

    public event ChangedEventHandler OnPayoutOk;

    public event ChangedEventHandler OnNoteReading;

    private void NoteReading(EventArgs e)
    {
      ChangedEventHandler onNoteReading = this.OnNoteReading;
      if (onNoteReading == null)
        return;
      onNoteReading((object) this, e);
    }

    public event ChangedEventHandler OnNoteAccepted;

    private void NoteProcessed(EventArgs e)
    {
      ChangedEventHandler onNoteAccepted = this.OnNoteAccepted;
      if (onNoteAccepted == null)
        return;
      onNoteAccepted((object) this, e);
    }

    private void PayoutError(EventArgs e)
    {
      ChangedEventHandler onPayoutError = this.OnPayoutError;
      if (onPayoutError == null)
        return;
      onPayoutError((object) this, e);
    }

    private void PayoutOk(EventArgs e)
    {
      ChangedEventHandler onPayoutOk = this.OnPayoutOk;
      if (onPayoutOk == null)
        return;
      onPayoutOk((object) this, e);
    }

    private void NoteInserted(EventArgs e)
    {
      ChangedEventHandler onNoteInserted = this.OnNoteInserted;
      if (onNoteInserted == null)
        return;
      onNoteInserted((object) this, e);
    }

    private void PaymentComplete(EventArgs e)
    {
      ChangedEventHandler onPaymentComplete = this.OnPaymentComplete;
      if (onPaymentComplete == null)
        return;
      onPaymentComplete((object) this, e);
    }

    private void PaymentRejectedNotEnoughChange(EventArgs e)
    {
      ChangedEventHandler onNotEnoughChange = this.OnNotEnoughChange;
      if (onNotEnoughChange == null)
        return;
      onNotEnoughChange((object) this, e);
    }

    public int TotalAccepted
    {
      get => this._totalAccepted;
      private set
      {
        this._totalAccepted = value;
        this.NoteInserted((EventArgs) new ListPaymentEvents()
        {
          TotalAccepted = this._totalAccepted
        });
        if (this.Cart == null || this.TotalAccepted < this.Cart.Sum)
          return;
        this.Complete(this.TotalAccepted, this.Cart.Sum);
      }
    }

    private int Change { get; set; }

    private int TotalRejected
    {
      get => this._totalRejected;
      set => this._totalRejected = value;
    }

    private void Complete(int accepted, int price)
    {
      if (accepted > price)
      {
        if (!this.CanReturnChange(accepted - price))
        {
          this._payout.PayoutAmount(this.TotalAccepted * 100);
          this.PaymentRejectedNotEnoughChange((EventArgs) new ListPaymentEvents()
          {
            TotalAccepted = 0,
            Sum = this.Cart.Sum,
            Change = this.TotalAccepted
          });
          this.Change = this.TotalAccepted;
          this.TotalAccepted = 0;
        }
        else
        {
          this.PaymentComplete((EventArgs) new ListPaymentEvents(this.Cart)
          {
            TotalAccepted = this._totalAccepted,
            Sum = this.Cart.Sum,
            Change = (this.TotalAccepted - this.Cart.Sum),
            PaymentType = (short) 1,
            Notes = new List<int>(),
            Phone = this.Client.PhoneNumber
          });
          this.Change = accepted - price;
          this._payout.PayoutAmount(this.Change * 100);
          return;
        }
      }
      if (accepted != price)
        return;
      this.PaymentComplete((EventArgs) new ListPaymentEvents(this.Cart)
      {
        TotalAccepted = this._totalAccepted,
        Sum = this.Cart.Sum,
        Change = (this.TotalAccepted - this.Cart.Sum),
        PaymentType = (short) 1,
        Notes = new List<int>(),
        Phone = this.Client.PhoneNumber
      });
      this.Change = 0;
      this._waitTimer.Start();
    }

    private bool CanReturnChange(int value)
    {
      int sum = 0;
      List<int> source = new List<int>();
      foreach (ChannelLevelInfo channelLevelInfo in (IEnumerable<ChannelLevelInfo>) this._payout.GetChannelLevelInfo3().OrderByDescending<ChannelLevelInfo, int>((Func<ChannelLevelInfo, int>) (o => o.Note)))
      {
        for (int index = 0; index < (int) channelLevelInfo.Count; ++index)
          source.Add(channelLevelInfo.Note);
      }
      while (sum < value)
      {
        int num = source.FirstOrDefault<int>((Func<int, bool>) (w => w <= value - sum));
        if (num == 0)
          return false;
        source.Remove(num);
        sum += num;
        if (!source.Any<int>() && sum < value)
          return false;
      }
      return sum == value;
    }

    public Cart Cart { get; set; }

    public BonusResponse Client { get; set; }

    public bool IsOk { get; set; }

    private PayoutService()
    {
      this._payout = new CPayout();
      this._payout.OnNoteInserted += new Terminal.NV200.ChangedEventHandler(this.NoteInserted);
      this._payout.OnNoteDispensed += new Terminal.NV200.ChangedEventHandler(this.NoteDispensed);
      this._payout.OnAllNotesDispensed += new Terminal.NV200.ChangedEventHandler(this.PayoutAllNotesDispensed);
      this._payout.OnNoteProcessed += new Terminal.NV200.ChangedEventHandler(this.NoteProcessed);
      this._payout.OnNoteStacked += new Terminal.NV200.ChangedEventHandler(this.NoteStacked);
      this._payout.OnNoteReading += new Terminal.NV200.ChangedEventHandler(this.NoteReading);
      this._payout.OnErrorDuringPayout += new Terminal.NV200.ChangedEventHandler(this.ErrorDuringPayout);
      this._timer = new System.Timers.Timer((double) this.pollTimer);
      this._timer.Elapsed += new ElapsedEventHandler(this.OnTimerTick);
      this._waitTimer = new System.Timers.Timer((double) this._waitTimerInterval);
      this._waitTimer.Elapsed += new ElapsedEventHandler(this.OnWaitTimerTick);
      PayoutService.CheckPayoutTimer.Tick += new EventHandler(this.OnCheckPayoutTick);
      PayoutService.CheckPayoutTimer.Interval = new TimeSpan(0, 0, Global.CheckPayoutInterval);
      PayoutService.CheckPayoutTimer.Start();
      PayoutService._backgroundWorker = new BackgroundWorker();
      PayoutService._backgroundWorker.DoWork += new DoWorkEventHandler(this.MainLoop);
      PayoutService._backgroundWorker.WorkerReportsProgress = true;
      PayoutService._backgroundWorker.WorkerSupportsCancellation = true;
      PayoutService._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
    }

    private void NoteDispensed(object sender, EventArgs e)
    {
      if (!(e is ListPaymentEvents listPaymentEvents))
        return;
      this.TotalRejected = listPaymentEvents.TotalRejected;
    }

    private void NoteReading(object sender, EventArgs e) => this.NoteReading((EventArgs) new PaymentEvent());

    private void NoteProcessed(object sender, EventArgs e) => this.NoteProcessed((EventArgs) new PaymentEvent());

    private void NoteStacked(object sender, EventArgs e)
    {
      if (!(e is ListPaymentEvents listPaymentEvents))
        return;
      StorageService.Instance.Data.Add(new ChannelLevelInfo()
      {
        Note = listPaymentEvents.TotalAccepted,
        Count = (short) 1
      });
    }

    private void NoteInserted(object sender, EventArgs e)
    {
      if (!(e is ListPaymentEvents listPaymentEvents))
        return;
      this.TotalAccepted += listPaymentEvents.TotalAccepted;
    }

    private void ErrorDuringPayout(object sender, EventArgs e)
    {
      if (!(e is ListPaymentEvents listPaymentEvents))
        return;
      this.TotalRejected = listPaymentEvents.TotalRejected;
      ILog log1 = Logger.Log;
      int num = this.TotalRejected;
      string str1 = num.ToString();
      num = this.Change;
      string str2 = num.ToString();
      string message1 = "Ошибка при выдаче выдано: " + str1 + " из " + str2;
      log1.Info((object) message1);
      ILog log2 = LoggerNV200.Log;
      num = this.TotalRejected;
      string str3 = num.ToString();
      num = this.Change;
      string str4 = num.ToString();
      string message2 = "Ошибка при выдаче выдано: " + str3 + " из " + str4;
      log2.Info((object) message2);
      this.Change -= this.TotalRejected;
      ILog log3 = Logger.Log;
      num = this.Change;
      string message3 = "Осталось выдать: " + num.ToString();
      log3.Info((object) message3);
      ILog log4 = LoggerNV200.Log;
      num = this.Change;
      string message4 = "Осталось выдать: " + num.ToString();
      log4.Info((object) message4);
      this._payout.PayoutAmount(this.Change * 100);
      ILog log5 = Logger.Log;
      num = this.Change;
      string message5 = "Отправляем повторно: " + num.ToString();
      log5.Info((object) message5);
      ILog log6 = LoggerNV200.Log;
      num = this.Change;
      string message6 = "Отправляем повторно: " + num.ToString();
      log6.Info((object) message6);
    }

    public void ConnectToPayout()
    {
      this._payout.SSPComms.CloseComPort();
      this._payout.CommandStructure.ComPort = Global.Nv200ComPort;
      this._payout.CommandStructure.SSPAddress = Global.Nv200SSPAddress;
      this._payout.CommandStructure.Timeout = 3000U;
      if (this.ConnectToValidator(Global.Nv200ReconnectionAttempts))
      {
        this._payout.ConfigureBezel((byte) 0, (byte) 0, byte.MaxValue);
        DataExchange.SendNv200Status("OK");
      }
      else
        DataExchange.SendNv200Status("ErrorPort");
    }

    private void OnCheckPayoutTick(object sender, EventArgs e)
    {
      if (!this.IsOk)
      {
        this.ConnectToPayout();
        LoggerNV200.Log.Info((object) "Соединяемся с валидатором");
      }
      if (this.SendSync())
      {
        this.IsOk = true;
        DataExchange.SendNv200Status("OK");
        this.PayoutOk(new EventArgs());
        LoggerNV200.Log.Info((object) "Успешно соединились с валидатором");
      }
      else
      {
        this.IsOk = false;
        DataExchange.SendNv200Status("Error");
        this.PayoutError(new EventArgs());
        LoggerNV200.Log.Info((object) "Ошибка соединения с валидатором");
        this.ConnectToPayout();
        LoggerNV200.Log.Info((object) "Соединяемся с валидатором");
      }
    }

    public void ShutUpAndTakeMoney(Cart cart, BonusResponse client)
    {
      if (cart.Items != null)
      {
        foreach (CartItem cartItem in cart.Items)
        {
          ButtonItem buttonItem = new ButtonItem();
          if (cartItem.ServiceItem != null)
          {
            buttonItem.Name = cartItem.ServiceItem.Name;
            buttonItem.Price = new int?(cartItem.ServiceItem.Price);
          }
          else if (cartItem.ProductItem != null)
          {
            buttonItem.Name = cartItem.ProductItem.Name;
            buttonItem.Price = new int?(cartItem.ProductItem.Price);
          }
          Logger.Log.Info((object) string.Format("Метод Nv200({0},{1})", (object) buttonItem.Name, (object) buttonItem.Price));
          LoggerNV200.Log.Info((object) string.Format("Метод Nv200({0},{1})", (object) buttonItem.Name, (object) buttonItem.Price));
        }
      }
      this.TotalAccepted = 0;
      this.TotalRejected = 0;
      this.Change = 0;
      this.Cart = cart;
      this.Client = client;
      if (PayoutService._backgroundWorker.IsBusy)
        return;
      PayoutService._backgroundWorker.RunWorkerAsync();
    }

    private PayoutService(ButtonItem serviceItem, BonusResponse client)
    {
    }

    private void OnTimerTick(object sender, EventArgs e) => this._timer.Stop();

    private void OnWaitTimerTick(object sender, EventArgs e)
    {
      this.Running = false;
      this._waitTimer.Stop();
    }

    private void PayoutAllNotesDispensed(object sender, EventArgs e) => this.Running = false;

    private void MainLoop(object sender, DoWorkEventArgs doWorkEventArgs)
    {
      this.EnablePayout();
      this._payout.ConfigureBezel((byte) 0, byte.MaxValue, (byte) 0);
      this.Running = true;
      while (this.Running)
      {
        if (PayoutService._backgroundWorker.CancellationPending)
        {
          doWorkEventArgs.Cancel = true;
          this.Running = false;
          return;
        }
        if (!this._payout.DoPoll())
        {
          this._payout.SSPComms.CloseComPort();
          if (!this.ConnectToValidator(Global.Nv200ReconnectionAttempts))
          {
            this._payout.SSPComms.CloseComPort();
            return;
          }
        }
        this._timer.Start();
        while (this._timer.Enabled)
          Thread.Sleep(0);
      }
      this._payout.DisablePayout();
      this._payout.DisableValidator();
    }

    private bool ConnectToValidator(int attempts)
    {
      Logger.Log.Info((object) "Пытаюсь подключиться к валидатору");
      LoggerNV200.Log.Info((object) "Пытаюсь подключиться к валидатору");
      for (int index = 0; index < attempts; ++index)
      {
        this._payout.SSPComms.CloseComPort();
        this._payout.CommandStructure.EncryptionStatus = false;
        if (this._payout.OpenComPort() && this._payout.NegotiateKeys())
        {
          this._payout.CommandStructure.EncryptionStatus = true;
          byte maxProtocolVersion = this.FindMaxProtocolVersion();
          if (maxProtocolVersion < (byte) 6)
            return false;
          this._payout.SetProtocolVersion(maxProtocolVersion);
          this._payout.PayoutSetupRequest();
          return PayoutService.IsUnitValid(this._payout.UnitType);
        }
      }
      return false;
    }

    private void EnablePayout()
    {
      this._payout.SetInhibits();
      this._payout.GetSerialNumber();
      this._payout.EnableValidator();
      this._payout.SetRoutes();
    }

    private static bool IsUnitValid(char unitType) => unitType == '\u0006';

    private byte FindMaxProtocolVersion()
    {
      byte pVersion = 6;
      do
      {
        this._payout.SetProtocolVersion(pVersion);
        if (this._payout.CommandStructure.ResponseData[0] != (byte) 248)
          ++pVersion;
        else
          goto label_1;
      }
      while (pVersion <= (byte) 12);
      goto label_3;
label_1:
      byte num;
      return num = (byte) ((uint) pVersion - 1U);
label_3:
      return 6;
    }

    private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Cancelled)
      {
        Logger.Log.Info((object) "Работа BackgroundWorker была прервана пользователем!");
        LoggerNV200.Log.Info((object) "Работа BackgroundWorker была прервана пользователем!");
      }
      else if (e.Error != null)
      {
        Logger.Log.Info((object) ("Worker exception: " + e.Error?.ToString()));
        LoggerNV200.Log.Info((object) ("Worker exception: " + e.Error?.ToString()));
      }
      else
      {
        Logger.Log.Info((object) ("Работа закончена успешно. Результат - " + e.Result?.ToString() + ". "));
        LoggerNV200.Log.Info((object) ("Работа закончена успешно. Результат - " + e.Result?.ToString() + ". "));
      }
    }

    public int Cancel()
    {
      LoggerNV200.Log.Info((object) ("Отменяем операцию. Возвращаем: " + this.TotalAccepted.ToString()));
      if (this.TotalAccepted > 0)
      {
        this.Running = false;
        Thread.Sleep(1000);
        LoggerNV200.Log.Info((object) ("Возвращаем: " + this.TotalAccepted.ToString()));
        this._payout.EnablePayout();
        this._payout.PayoutAmount(this.TotalAccepted * 100);
      }
      if (this.TotalAccepted == 0)
        this.Running = false;
      return this.TotalAccepted;
    }

    public int Cancel(int Refund)
    {
      LoggerNV200.Log.Info((object) ("Возврат операции. Возвращаем: " + Refund.ToString()));
      if (!this.CanReturnChange(Refund))
        return 0;
      this._payout.PayoutAmount(Refund * 100);
      this.Running = false;
      return Refund;
    }

    public List<Tuple<int, int>> GetChannelLevelInfo() => this._payout.GetChannelLevelInfo2();

    public List<ChannelLevelInfo> GetChannelLevelInfo2()
    {
      List<Tuple<int, int>> channelLevelInfo2_1 = this._payout.GetChannelLevelInfo2();
      List<ChannelLevelInfo> channelLevelInfo2_2 = new List<ChannelLevelInfo>();
      foreach (Tuple<int, int> tuple in channelLevelInfo2_1)
        channelLevelInfo2_2.Add(new ChannelLevelInfo()
        {
          Note = tuple.Item1,
          Count = (short) (sbyte) tuple.Item2
        });
      return channelLevelInfo2_2;
    }

    public bool SendSync() => PayoutService._backgroundWorker.IsBusy || this._payout.SendSync();

    public void EmptyAll() => this._payout.EmptyPayoutDevice();

    public void Reset() => this._payout.Reset();
  }
}
