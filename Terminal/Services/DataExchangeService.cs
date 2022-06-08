// Decompiled with JetBrains decompiler
// Type: Terminal.Services.DataExchangeService
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Terminal.Classes;
using Terminal.View;

namespace Terminal.Services
{
  public sealed class DataExchangeService
  {
    private static readonly System.Lazy<DataExchangeService> Lazy = new System.Lazy<DataExchangeService>((Func<DataExchangeService>) (() => new DataExchangeService()));
    private static readonly BackgroundWorker BackgroundWorkerServiceParents = new BackgroundWorker();
    private static readonly BackgroundWorker BackgroundWorkerServices = new BackgroundWorker();
    private static readonly BackgroundWorker BackgroundWorkerProducts = new BackgroundWorker();
    private static readonly BackgroundWorker BackgroundWorkerUsers = new BackgroundWorker();
    private static readonly BackgroundWorker BackgroundWorkerCheckIn = new BackgroundWorker();
    private static readonly BackgroundWorker BackgroundWorkerDateMismatch = new BackgroundWorker();
    private static readonly DispatcherTimer Timer = new DispatcherTimer();
    private static readonly DispatcherTimer TimerDateMismatch = new DispatcherTimer();
    private static readonly DispatcherTimer TimerCheckIn = new DispatcherTimer();
    private static readonly DispatcherTimer CheckInternetConnectionTimer = new DispatcherTimer();
    private static readonly DispatcherTimer CheckDatabaseConnectionTimer = new DispatcherTimer();

    public static DataExchangeService Instance => DataExchangeService.Lazy.Value;

    public event DataExchangeService.ChangedEventHandler OnDataReceived;

    public event DataExchangeService.ChangedEventHandler OnDateMismatch;

    public event DataExchangeService.ChangedEventHandler OnDatabaseConnectionError;

    public event DataExchangeService.ChangedEventHandler OnDatabaseConnectionOk;

    public event DataExchangeService.ChangedEventHandler OnInternetConnectionError;

    public event DataExchangeService.ChangedEventHandler OnInternetConnectionOk;

    private void DataReceived(EventArgs e)
    {
      DataExchangeService.ChangedEventHandler onDataReceived = this.OnDataReceived;
      if (onDataReceived == null)
        return;
      onDataReceived((object) this, e);
    }

    private void DateMismatch(EventArgs e)
    {
      DataExchangeService.ChangedEventHandler onDateMismatch = this.OnDateMismatch;
      if (onDateMismatch == null)
        return;
      onDateMismatch((object) this, e);
    }

    private void DatabaseConnectionError(EventArgs e)
    {
      DataExchangeService.ChangedEventHandler databaseConnectionError = this.OnDatabaseConnectionError;
      if (databaseConnectionError == null)
        return;
      databaseConnectionError((object) this, e);
    }

    private void DatabaseConnectionOk(EventArgs e)
    {
      DataExchangeService.ChangedEventHandler databaseConnectionOk = this.OnDatabaseConnectionOk;
      if (databaseConnectionOk == null)
        return;
      databaseConnectionOk((object) this, e);
    }

    private void InternetConnectionError(EventArgs e)
    {
      DataExchangeService.ChangedEventHandler internetConnectionError = this.OnInternetConnectionError;
      if (internetConnectionError == null)
        return;
      internetConnectionError((object) this, e);
    }

    private void InternetConnectionOk(EventArgs e)
    {
      DataExchangeService.ChangedEventHandler internetConnectionOk = this.OnInternetConnectionOk;
      if (internetConnectionOk == null)
        return;
      internetConnectionOk((object) this, e);
    }

    private DataExchangeService()
    {
      DataExchangeService.BackgroundWorkerServiceParents.DoWork += new DoWorkEventHandler(DataExchangeService.DataExchangeServiceParents);
      DataExchangeService.BackgroundWorkerServiceParents.WorkerReportsProgress = true;
      DataExchangeService.BackgroundWorkerServiceParents.WorkerSupportsCancellation = true;
      DataExchangeService.BackgroundWorkerServiceParents.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.DataExchangeCompleted);
      DataExchangeService.BackgroundWorkerServices.DoWork += new DoWorkEventHandler(DataExchangeService.DataExchangeServices);
      DataExchangeService.BackgroundWorkerServices.WorkerReportsProgress = true;
      DataExchangeService.BackgroundWorkerServices.WorkerSupportsCancellation = true;
      DataExchangeService.BackgroundWorkerServices.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.DataExchangeCompleted);
      DataExchangeService.BackgroundWorkerProducts.DoWork += new DoWorkEventHandler(DataExchangeService.DataExchangeProducts);
      DataExchangeService.BackgroundWorkerProducts.WorkerReportsProgress = true;
      DataExchangeService.BackgroundWorkerProducts.WorkerSupportsCancellation = true;
      DataExchangeService.BackgroundWorkerProducts.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.DataExchangeCompleted);
      DataExchangeService.BackgroundWorkerUsers.DoWork += new DoWorkEventHandler(DataExchangeService.DataExchangeUsers);
      DataExchangeService.BackgroundWorkerUsers.WorkerReportsProgress = true;
      DataExchangeService.BackgroundWorkerUsers.WorkerSupportsCancellation = true;
      DataExchangeService.BackgroundWorkerUsers.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.DataExchangeCompleted);
      DataExchangeService.BackgroundWorkerCheckIn.DoWork += new DoWorkEventHandler(DataExchangeService.DataExchangeCheckIn);
      DataExchangeService.BackgroundWorkerCheckIn.WorkerReportsProgress = true;
      DataExchangeService.BackgroundWorkerCheckIn.WorkerSupportsCancellation = true;
      DataExchangeService.BackgroundWorkerCheckIn.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.DataExchangeCompleted);
      DataExchangeService.Timer.Tick += new EventHandler(DataExchangeService.OnTimerTick);
      DataExchangeService.Timer.Interval = new TimeSpan(0, 0, Global.ServicesCheckerInterval);
      DataExchangeService.Timer.Start();
      DataExchangeService.TimerCheckIn.Tick += new EventHandler(DataExchangeService.OnTimerCheckInTick);
      DataExchangeService.TimerCheckIn.Interval = new TimeSpan(0, 0, Global.PaymentSendInterval);
      DataExchangeService.TimerCheckIn.Start();
      DataExchangeService.BackgroundWorkerDateMismatch.DoWork += new DoWorkEventHandler(this.GetDateTime);
      DataExchangeService.BackgroundWorkerDateMismatch.WorkerReportsProgress = true;
      DataExchangeService.BackgroundWorkerDateMismatch.WorkerSupportsCancellation = true;
      DataExchangeService.BackgroundWorkerDateMismatch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.DateTimeExchangeCompleted);
      DataExchangeService.TimerDateMismatch.Tick += new EventHandler(this.OnDateMismatchTick);
      DataExchangeService.TimerDateMismatch.Interval = new TimeSpan(0, 0, Global.DateCheckerInterval);
      DataExchangeService.TimerDateMismatch.Start();
      DataExchangeService.CheckInternetConnectionTimer.Tick += new EventHandler(this.OnCheckInternetConnectionTick);
      DataExchangeService.CheckInternetConnectionTimer.Interval = new TimeSpan(0, 0, Global.CheckServerInterval);
      DataExchangeService.CheckInternetConnectionTimer.Start();
      DataExchangeService.CheckDatabaseConnectionTimer.Tick += new EventHandler(this.OnCheckDatabaseConnectionTick);
      DataExchangeService.CheckDatabaseConnectionTimer.Interval = new TimeSpan(0, 0, Global.CheckDatabaseInterval);
      DataExchangeService.CheckDatabaseConnectionTimer.Start();
    }

    private void OnCheckDatabaseConnectionTick(object sender, EventArgs e) => this.CheckDatabaseConnectionTick();

    private async void CheckDatabaseConnectionTick()
    {
      int result = await DataExchange.CheckDatabaseConnectionAsync();
      if (result == 0)
        this.DatabaseConnectionOk(new EventArgs());
      else
        this.DatabaseConnectionError(new EventArgs());
    }

    private void OnCheckInternetConnectionTick(object sender, EventArgs e) => this.CheckInternetConnection();

    private async void CheckInternetConnection()
    {
      TestConnectionResponse result = await DataExchange.CheckInternetConnectionAsync();
      if (result.Status == 0)
      {
        this.IsInternetOk = true;
        this.InternetConnectionOk(new EventArgs());
        if (result.Command == null)
        {
          result = (TestConnectionResponse) null;
        }
        else
        {
          this.TerminalCommand(result.Command);
          result = (TestConnectionResponse) null;
        }
      }
      else
      {
        this.IsInternetOk = false;
        this.InternetConnectionError(new EventArgs());
        result = (TestConnectionResponse) null;
      }
    }

    public bool IsInternetOk { get; set; }

    private void DateTimeExchangeCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (Math.Abs((DateTime.UtcNow - (DateTime) e.Result).TotalDays) <= 1.0)
        return;
      this.DateMismatch(new EventArgs());
    }

    private void GetDateTime(object sender, DoWorkEventArgs e)
    {
      DateTime dateTime = DataExchange.GetDateTime();
      e.Result = (object) dateTime;
    }

    private void OnDateMismatchTick(object sender, EventArgs e)
    {
      if (DataExchangeService.BackgroundWorkerDateMismatch.IsBusy)
        return;
      DataExchangeService.BackgroundWorkerDateMismatch.RunWorkerAsync();
    }

    private void DataExchangeCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Result == null)
        return;
      this.DataReceived((EventArgs) e);
    }

    private static void DataExchangeServiceParents(object sender, DoWorkEventArgs e)
    {
      try
      {
        ServerResponse serviceParents = DataExchange.GetServiceParents();
        if (serviceParents.Status == 0)
        {
          StorageService.Instance.Data.Replace(serviceParents.ServiceParents);
          e.Result = (object) serviceParents.ServiceParents;
        }
        else
          e.Result = (object) StorageService.Instance.Data.ServiceParents;
      }
      catch (Exception ex)
      {
        e.Result = (object) StorageService.Instance.Data.ServiceParents;
      }
    }

    private static void DataExchangeServices(object sender, DoWorkEventArgs e)
    {
      try
      {
        ServerResponse services = DataExchange.GetServices();
        if (services.Status == 0)
        {
          StorageService.Instance.Data.Replace(services.Services);
          e.Result = (object) services.Services;
        }
        else
          e.Result = (object) StorageService.Instance.Data.Services;
      }
      catch (Exception ex)
      {
        e.Result = (object) StorageService.Instance.Data.Services;
      }
    }

    private static void DataExchangeProducts(object sender, DoWorkEventArgs e)
    {
      try
      {
        ServerResponse products = DataExchange.GetProducts();
        if (products.Status == 0)
        {
          StorageService.Instance.Data.Replace(products.Products);
          e.Result = (object) products.Products;
        }
        else
          e.Result = (object) StorageService.Instance.Data.Products;
      }
      catch (Exception ex)
      {
        e.Result = (object) StorageService.Instance.Data.Products;
      }
    }

    private static void DataExchangeUsers(object sender, DoWorkEventArgs e)
    {
      try
      {
        ServerResponse users = DataExchange.GetUsers();
        if (users.Status == 0)
        {
          StorageService.Instance.Data.Replace(users.Users);
          e.Result = (object) users.Users;
        }
        else
          e.Result = (object) StorageService.Instance.Data.Users;
      }
      catch (Exception ex)
      {
        e.Result = (object) StorageService.Instance.Data.Users;
      }
    }

    private static void DataExchangeCheckIn(object sender, DoWorkEventArgs e)
    {
      try
      {
        List<CheckIn> unuploadedCheckIn = DataExchangeService.GetUnuploadedCheckIn();
        if (unuploadedCheckIn == null)
          return;
        foreach (CheckIn checkIn in unuploadedCheckIn)
          DataExchange.SendCheckIn(checkIn);
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
      }
    }

    private static List<CheckIn> GetUnuploadedCheckIn() => StorageService.Instance.Data.CheckIns == null ? (List<CheckIn>) null : StorageService.Instance.Data.CheckIns.FindAll((Predicate<CheckIn>) (x => !x.Send));

    private static void OnTimerTick(object sender, EventArgs e)
    {
      try
      {
        if (DataExchangeService.BackgroundWorkerServiceParents.IsBusy)
          return;
        DataExchangeService.BackgroundWorkerServiceParents.RunWorkerAsync();
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex);
        throw;
      }
      try
      {
        if (DataExchangeService.BackgroundWorkerServices.IsBusy)
          return;
        DataExchangeService.BackgroundWorkerServices.RunWorkerAsync();
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex);
        throw;
      }
      try
      {
        if (DataExchangeService.BackgroundWorkerProducts.IsBusy)
          return;
        DataExchangeService.BackgroundWorkerProducts.RunWorkerAsync();
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex);
        throw;
      }
      try
      {
        if (DataExchangeService.BackgroundWorkerUsers.IsBusy)
          return;
        DataExchangeService.BackgroundWorkerUsers.RunWorkerAsync();
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex);
        throw;
      }
    }

    private static void OnTimerCheckInTick(object sender, EventArgs e)
    {
      try
      {
        if (DataExchangeService.BackgroundWorkerCheckIn.IsBusy)
          return;
        DataExchangeService.BackgroundWorkerCheckIn.RunWorkerAsync();
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex);
        throw;
      }
    }

    public List<ServiceParentItem> GetServiceParentItems() => StorageService.Instance.Data.ServiceParents ?? new List<ServiceParentItem>();

    public List<ServiceItem> GetServiceItems() => StorageService.Instance.Data.Services ?? new List<ServiceItem>();

    public List<ProductItem> GetProductItems() => StorageService.Instance.Data.Products ?? new List<ProductItem>();

    public List<User> GetUsers() => StorageService.Instance.Data.Users ?? new List<User>();

    private void TerminalCommand(string Command)
    {
      Logger.Log.Info((object) ("Получена команда: " + Command));
      switch (Command)
      {
        case "OpenServiceWindow":
          Logger.Log.Info((object) "Открываем сервисное окно");
          ServiceWindow serviceWindow = new ServiceWindow();
          serviceWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
          serviceWindow.ShowDialog();
          break;
      }
    }

    public delegate void ChangedEventHandler(object sender, EventArgs e);
  }
}
