// Decompiled with JetBrains decompiler
// Type: Terminal.App
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using Quartz;
using Quartz.Impl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Terminal.Classes;
using Terminal.Environment;
using Terminal.Services;
using Terminal.View;
using Terminal.ViewModels;

namespace Terminal
{
    public class App : Application
    {
        private static Mutex InstanceCheckMutex;
        private ICommand _gotoMainMenuCommand;
        private ICommand _gotoPhoneNumberWindowCommand;
        private ICommand _gotoPaymentWindowCommand;
        private ICommand _payByCashCommand;
        private ICommand _payByCardCommand;
        private ICommand _payByBonusCommand;
        private MainWindowViewModel _mainWindowViewModel;
        private PhoneNumberViewModel _phoneNumberViewModel;
        private CartViewModel _cartViewModel;
        private PayByCardViewModel _payByCardViewModel;
        private IScheduler sched;
        private List<EquipmentStatusList> _statusList = new List<EquipmentStatusList>();
        public Cart Cart = new Cart();
        private PayByCashViewModel _payByCashViewModel;
        private static DispatcherTimer _timer;
        private MediaPlayer _mediaPlayer;
        private CancellationTokenSource source;
        private CancellationToken token;

    private static App _AppStatic; // 05.06.22
    private static object _DelegateParam;
    delegate void MyVoidDelegate(object obj); // 
    private MyVoidDelegate myVoidDelegate1 = delegate(object obj) { _AppStatic._payByCashViewModel.IsVisible = (System.Windows.Visibility) obj; };
	private MyVoidDelegate myVoidDelegate3 = delegate(object obj) { _AppStatic.PayByCardViewModel.Line1      = (String)obj; };
	private MyVoidDelegate myVoidDelegate4 = delegate(object obj) { _AppStatic.PayByCardViewModel.MiddleLine = (String)obj; };
	private MyVoidDelegate myVoidDelegate5 = delegate(object obj) { _AppStatic._payByCashViewModel.TotalInserted = (Decimal) obj; };	

    private static bool InstanceCheck()
    {
      bool createdNew;
      App.InstanceCheckMutex = new Mutex(true, "Terminal", out createdNew);
      return createdNew;
    }

    public ICommand MainServicesButtonCommand => (ICommand) new Command(new Action<object>(this.MainServiceClick), true);

    public ICommand MainServicesNextButtonCommand => (ICommand) new Command(new Action<object>(this.MainServiceNextButtonClick), true);

    public ICommand FrButtonCommand => (ICommand) new Command(new Action<object>(this.FrButtonClick), true);

    public ICommand CartButtonCommand => (ICommand) new Command(new Action<object>(this.GotoCartWindowClick), true);

    public ICommand PaymentButtonCommand => (ICommand) new Command(new Action<object>(App.GotoPaymentWindowClick), true);

    public ICommand MainServicesPreviousButtonCommand => (ICommand) new Command(new Action<object>(this.MainServicePreviousButtonClick), true);

    public ICommand GotoMainMenuCommand => this._gotoMainMenuCommand ?? (this._gotoMainMenuCommand = (ICommand) new Command(new Action<object>(App.GotoMainMenuCick), true));

    public ICommand GotoPhoneNumberWindowCommand => this._gotoPhoneNumberWindowCommand ?? (this._gotoPhoneNumberWindowCommand = (ICommand) new Command(new Action<object>(this.GotoPhoneNumberWindowClick), true));

    public ICommand ClosePaymentWindow => (ICommand) new Command(new Action<object>(App.ClosePaymentWindowClick), true);

    public ICommand ClosePromoWindow => (ICommand) new Command(new Action<object>(App.ClosePromoWindowClick), true);

    public ICommand PayByCashWindowCancel => (ICommand) new Command(new Action<object>(this.PayByCashWindowCancelClick), true);

    public ICommand ConfirmPaymentCommand => (ICommand) new Command(new Action<object>(this.ConfirmPaymentClick), true);

    public ICommand ConfirmPromoCommand => (ICommand) new Command(new Action<object>(this.ConfirmPromoClick), true);

    public ICommand GotoPaymentWindowCommand => this._gotoPaymentWindowCommand ?? (this._gotoPaymentWindowCommand = (ICommand) new Command(new Action<object>(this.GotoPaymentWindowCick), true));

    public ICommand PayByCashCommand => this._payByCashCommand ?? (this._payByCashCommand = (ICommand) new Command(new Action<object>(this.PayByCashClick), true));

    public ICommand PayByCardCommand => this._payByCardCommand ?? (this._payByCardCommand = (ICommand) new Command(new Action<object>(this.PayByCardClick), true));

    public ICommand PayByBonusCommand => this._payByBonusCommand ?? (this._payByBonusCommand = (ICommand) new Command(new Action<object>(this.PayByBonusClick), true));

    public MainWindowViewModel MainWindowViewModel
    {
      get
      {
        MainWindowViewModel mainWindowViewModel1 = this._mainWindowViewModel;
        if (mainWindowViewModel1 == null)
        {
          MainWindowViewModel mainWindowViewModel2 = new MainWindowViewModel();
          mainWindowViewModel2.InfBlock1 = "Любая стрижка за 250 рублей!";
          mainWindowViewModel2.InfBlock2 = " ";
          mainWindowViewModel2.InfBlock3 = "www.strizhka-shop.ru";
          mainWindowViewModel2.InfBlock4 = " ";
          mainWindowViewModel2.Version = Global.Version;
          mainWindowViewModel2.ButtonCommand = this.MainServicesButtonCommand;
          mainWindowViewModel2.PriviousPageCommand = this.MainServicesPreviousButtonCommand;
          mainWindowViewModel2.NextPageCommand = this.MainServicesNextButtonCommand;
          mainWindowViewModel2.FrCommand = this.FrButtonCommand;
          mainWindowViewModel2.CartCommand = this.CartButtonCommand;
          mainWindowViewModel2.GotoMenuCommand = this.GotoMainMenuCommand;
          MainWindowViewModel mainWindowViewModel3 = mainWindowViewModel2;
          this._mainWindowViewModel = mainWindowViewModel2;
          mainWindowViewModel1 = mainWindowViewModel3;
        }
        return mainWindowViewModel1;
      }
    }

    public PhoneNumberViewModel PhoneNumberViewModel
    {
      get
      {
        PhoneNumberViewModel phoneNumberViewModel1 = this._phoneNumberViewModel;
        if (phoneNumberViewModel1 != null)
          return phoneNumberViewModel1;
        PhoneNumberViewModel phoneNumberViewModel2 = new PhoneNumberViewModel();
        phoneNumberViewModel2.GotoMenuCommand = this.GotoMainMenuCommand;
        phoneNumberViewModel2.GotoPreviousPageCommand = this.CartButtonCommand;
        phoneNumberViewModel2.GotoNextPageCommand = this.GotoPaymentWindowCommand;
        phoneNumberViewModel2.InfBlock2 = " ";
        phoneNumberViewModel2.InfBlock3 = "www.strizhka-shop.ru";
        phoneNumberViewModel2.InfBlock4 = " ";
        phoneNumberViewModel2.Version = Global.Version;
        phoneNumberViewModel2.ShowRadio = Global.ShowRadio;
        phoneNumberViewModel2.ContinueLabel = "Для продолжения нажмите стрелку вправо";
        phoneNumberViewModel2.HeaderLabel = "Для получения бонусных баллов в размере";
        phoneNumberViewModel2.HeaderLabelBonus = "5%";
        phoneNumberViewModel2.HeaderLabel2 = "от стоимости услуг, введите номер мобильного телефона";
        phoneNumberViewModel2.SexLabel = "Выберите Ваш пол";
        phoneNumberViewModel2.AgeLabel = "Выберите Ваш возраст";
        PhoneNumberViewModel phoneNumberViewModel3 = phoneNumberViewModel2;
        this._phoneNumberViewModel = phoneNumberViewModel2;
        return phoneNumberViewModel3;
      }
    }

    public CartViewModel CartViewModel
    {
      get
      {
        CartViewModel cartViewModel1 = this._cartViewModel;
        if (cartViewModel1 != null)
          return cartViewModel1;
        CartViewModel cartViewModel2 = new CartViewModel();
        cartViewModel2.GotoMenuCommand = this.GotoMainMenuCommand;
        cartViewModel2.GotoPreviousPageCommand = this.GotoMainMenuCommand;
        cartViewModel2.GotoNextPageCommand = this.GotoPhoneNumberWindowCommand;
        cartViewModel2.InfBlock2 = " ";
        cartViewModel2.InfBlock3 = "www.strizhka-shop.ru";
        cartViewModel2.InfBlock4 = " ";
        cartViewModel2.Line5 = "Итого: ";
        cartViewModel2.Version = Global.Version;
        cartViewModel2.Cart = this.Cart;
        CartViewModel cartViewModel3 = cartViewModel2;
        this._cartViewModel = cartViewModel2;
        return cartViewModel3;
      }
    }

    public PaymentViewModel PaymentViewModel
    {
      get
      {
        PaymentViewModel paymentViewModel = new PaymentViewModel();
        paymentViewModel.Line3 = "На вашем счету {0} бонуса(ов)";
        paymentViewModel.Line3Empty = "Не указан номер телефона";
        paymentViewModel.Line3InProcess = "Пожалуйста подождите, \r\nполучаем информацию о бонусных баллах... ";
        paymentViewModel.Line5 = "Итого к оплате: ";
        paymentViewModel.PayByBonusButtonText = "ОПЛАТИТЬ\r\nБОНУСНЫМИ БАЛЛАМИ";
        paymentViewModel.PayByCardButtonText = "ОПЛАТИТЬ\r\nБАНКОВСКОЙ КАРТОЙ";
        paymentViewModel.PayByCashButtonText = "ОПЛАТИТЬ\r\nНАЛИЧНЫМИ";
        paymentViewModel.CartButtonText = "НАЗАД\r\nК КОРЗИНЕ";
        paymentViewModel.PaymentButtonText = "ДАЛЕЕ\r\nК ОПЛАТЕ";
        paymentViewModel.PaymentVisibility = !Global.ShowCart;
        paymentViewModel.GotoMenuCommand = this.GotoMainMenuCommand;
        paymentViewModel.GotoPreviousPageCommand = this.GotoPhoneNumberWindowCommand;
        paymentViewModel.PayByCashCommand = this.PayByCashCommand;
        paymentViewModel.PayByCardCommand = this.PayByCardCommand;
        paymentViewModel.PayByBonusCommand = this.PayByBonusCommand;
        paymentViewModel.GoToCartCommand = this.CartButtonCommand;
        paymentViewModel.GoToPaymentCommand = this.PaymentButtonCommand;
        paymentViewModel.InfBlock2 = " ";
        paymentViewModel.InfBlock3 = "www.strizhka-shop.ru";
        paymentViewModel.InfBlock4 = " ";
        paymentViewModel.Version = Global.Version;
        return paymentViewModel;
      }
    }

    public PayByCardViewModel PayByCardViewModel
    {
      get => this._payByCardViewModel;
      set => this._payByCardViewModel = value;
    }

    public PayByBonusViewModel PayByBonusViewModel => new PayByBonusViewModel()
    {
      Line1 = "Введите уникальный СМС код,\r\n полученный на ваш мобильный телефон",
      Line2 = "смс код:",
      ErrorText = "Уникальный СМС код не верен",
      ErrorButtonText = "Набрать код заново",
      ConfirmButtonText = "Подтвердить",
      CancelButtonText = "Отменить",
      CancelCommand = this.ClosePaymentWindow,
      ConfirmCommand = this.ConfirmPaymentCommand,
      Cart = this.Cart,
      IsWrongCode = false
    };

    public PromoViewModel PromoViewModel => new PromoViewModel()
    {
      Line1 = "Введите промокод,\r\nуказанный на рекламной листовке",
      Line2 = "Промокод:",
      ErrorText = "Промокод не верен",
      ErrorButtonText = "Набрать промокод заново",
      ConfirmButtonText = "Подтвердить",
      CancelButtonText = "Отменить",
      CancelCommand = this.ClosePromoWindow,
      ConfirmCommand = this.ConfirmPromoCommand,
      IsWrongCode = false
    };

    public PaymentResultViewModel PaymentCompleteViewModel => new PaymentResultViewModel();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetDllDirectory(string lpPathName);

    protected override async void OnStartup(StartupEventArgs e)
    {
      _AppStatic = this; // 05.06.22

      if (!App.InstanceCheck())
      {
        int num = (int) MessageBox.Show("Программа уже запущена\nЕсли окно не активно\nоткройте диспетчер задач и завершите процесс Terminal.exe", "Программа уже запущена", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        Application.Current.Shutdown();
      }
      Logger.InitLogger();
      Directory.CreateDirectory(Global.JsonDirectory);
      try
      {
        App.SetDllDirectory(Global.DllPath);
        Logger.Log.Info((object) ("Подключен путь библиотек " + Global.DllPath));
      }
      catch (Exception ex)
      {
        Logger.Log.Fatal((object) ("Не удалось подключить путь библиотек " + Global.DllPath));
        Logger.Log.Info((object) ex.ToString());
        throw;
      }
      KeyService.Instance.Init();
      WindowService.ShowWindow<MainWindow>((object) this.MainWindowViewModel);
      this.MainWindowViewModel.ButtonsServiceParents = DataExchangeService.Instance.GetServiceParentItems();
      this.MainWindowViewModel.ButtonsServices = DataExchangeService.Instance.GetServiceItems();
      this.MainWindowViewModel.ButtonsProducts = DataExchangeService.Instance.GetProductItems();
      this.MainWindowViewModel.EquipmentStatus = this._statusList;
      this.MainWindowViewModel.Cart = this.Cart;
      DataExchangeService.Instance.OnDataReceived += new DataExchangeService.ChangedEventHandler(this.OnServiceItemReceived);
      DataExchangeService.Instance.OnInternetConnectionError += new DataExchangeService.ChangedEventHandler(this.OnInternetConnectionError);
      DataExchangeService.Instance.OnInternetConnectionOk += new DataExchangeService.ChangedEventHandler(this.OnInternetConnectionOk);
      DataExchangeService.Instance.OnDatabaseConnectionError += new DataExchangeService.ChangedEventHandler(this.OnDatabaseConnectionError);
      DataExchangeService.Instance.OnDatabaseConnectionOk += new DataExchangeService.ChangedEventHandler(this.OnDatabaseConnectionOk);
      PrintService.Instance.OnPrinterError += new PrintService.ChangedEventHandler(this.OnPrinterError);
      PrintService.Instance.OnPrinterOk += new PrintService.ChangedEventHandler(this.OnPrinterOk);
      PayoutService.Instance.OnPayoutError += new Terminal.Services.ChangedEventHandler(this.OnPayoutError);
      PayoutService.Instance.OnPayoutOk += new Terminal.Services.ChangedEventHandler(this.OnPayoutOk);
      PayoutService.Instance.OnNoteInserted += new Terminal.Services.ChangedEventHandler(this.NoteInserted);
      PayoutService.Instance.OnPaymentComplete += new Terminal.Services.ChangedEventHandler(this.PaymentComplete);
      PayoutService.Instance.OnNotEnoughChange += new Terminal.Services.ChangedEventHandler(App.NotEnoughChange);
      PayoutService.Instance.OnNoteReading += new Terminal.Services.ChangedEventHandler(this.NoteInserting);
      PayoutService.Instance.OnNoteAccepted += new Terminal.Services.ChangedEventHandler(this.NoteAccepted);
      CardService.Instance.OnPaymentComplete += new Terminal.Services.ChangedEventHandler(this.CardPaymentComplete);
      CardService.Instance.OnPaymentCancelled += new Terminal.Services.ChangedEventHandler(this.CardPaymentCancelled);
      CardService.Instance.OnCallBack += new Terminal.Services.ChangedEventHandler(this.PinpadCallback);
      PaymentService.Instance.Init();
      StorageService.Instance.Init();
      this.SetActivityTimer();
      StdSchedulerFactory factory = new StdSchedulerFactory();
      IScheduler sched = await factory.GetScheduler(new CancellationToken());
      await sched.Start();
      IJobDetail job = JobBuilder.Create<SimpleJob>().WithIdentity("myJob", "group1").Build();
      int year = DateTime.Now.Year;
      DateTime now = DateTime.Now;
      int month = now.Month;
      now = DateTime.Now;
      int day = now.Day;
      DateTime utcDateTime = new DateTime(year, month, day, 22, 45, 0, DateTimeKind.Local);
      DateTimeOffset dto = new DateTimeOffset(utcDateTime);
      ITrigger trigger = TriggerBuilder.Create().WithIdentity("myTrigger", "group1").StartAt(dto).WithSimpleSchedule((Action<SimpleScheduleBuilder>) (x => x.WithIntervalInHours(24).RepeatForever())).Build();
      DateTimeOffset dateTimeOffset = await sched.ScheduleJob(job, trigger);
      string str = await DataExchange.SendVersion();
      factory = (StdSchedulerFactory) null;
      sched = (IScheduler) null;
      job = (IJobDetail) null;
      trigger = (ITrigger) null;
    }

    private void NoteAccepted(object sender, EventArgs e)
    {
      if (this._payByCashViewModel == null)
        return;
// ?? Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Delegate) (() => this._payByCashViewModel.IsVisible = Visibility.Visible));
      _DelegateParam = Visibility.Visible; // 05.06.22 
      Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, myVoidDelegate1);
    }

    private void NoteInserting(object sender, EventArgs e)
    {
      if (this._payByCashViewModel == null)
        return;
// ?? Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Delegate) (() => this._payByCashViewModel.IsVisible = Visibility.Hidden));
      _DelegateParam = Visibility.Hidden; // 05.06.22 
      Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, myVoidDelegate1); 
    }

    private void PinpadCallback(object sender, EventArgs e)
    {
      if (this.PayByCardViewModel == null)
        return;
      PinpadEvent evnt = e as PinpadEvent;
      if (evnt == null)
        return;
      _DelegateParam = evnt.Caption; // 05.06.22 
      if (!evnt.Caption.Contains("*"))
// ??   Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Delegate) (() => this.PayByCardViewModel.Line1      = evnt.Caption));
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, myVoidDelegate3);
      else 
// ??   Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Delegate) (() => this.PayByCardViewModel.MiddleLine = evnt.Caption));
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, myVoidDelegate4);
      if (!evnt.Caption.Contains("ПИН"))
        return;
      string uriString = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\Wave\\04.wav";
      this._mediaPlayer = new MediaPlayer();
      this._mediaPlayer.Open(new Uri(uriString));
      this._mediaPlayer.Play();
    }

    private void CardPaymentCancelled(object sender, EventArgs e)
    {
      WindowService.CloseAllExceptOne((object) this.MainWindowViewModel);
      PinpadEvent pinpadEvent1 = e as PinpadEvent;
      PaymentResultViewModel dataContext = new PaymentResultViewModel();
      dataContext.OkCommand = (ICommand) new Command(new Action<object>(App.ClosePaymentResultWindow), true);
      dataContext.StatusText1 = "Операция отменена";
      PaymentResultViewModel paymentResultViewModel = dataContext;
      PinpadEvent pinpadEvent2 = pinpadEvent1;
      if (pinpadEvent2 == null)
        pinpadEvent2 = new PinpadEvent()
        {
          Caption = string.Empty
        };
      string caption = pinpadEvent2.Caption;
      paymentResultViewModel.StatusText2 = caption;
      WindowService.ShowWindow<PaymentResult>((object) dataContext);
      if (this.source == null)
        return;
      this.source.Cancel();
    }

    private void CardPaymentComplete(object sender, EventArgs e)
    {
      if (!(e is ListPaymentEvents listPaymentEvents))
        return;
      this.PaymentSuccessful(listPaymentEvents);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
      StorageService.Instance.Save();
      if (this.sched != null)
        await this.sched.Shutdown();
      base.OnExit(e);
    }

    private void OnPayoutOk(object sender, EventArgs e) => this.DeviceOk(0);

    private void OnPayoutError(object sender, EventArgs e) => this.DeviceError(0);

    private void OnDatabaseConnectionOk(object sender, EventArgs e) => this.DeviceOk(1);

    private void OnDatabaseConnectionError(object sender, EventArgs e) => this.DeviceError(1);

    private void OnInternetConnectionOk(object sender, EventArgs e) => this.DeviceOk(2);

    private void OnInternetConnectionError(object sender, EventArgs e) => this.DeviceError(2);

    private void OnPrinterOk(object sender, EventArgs e) => this.DeviceOk(3);

    private void DeviceOk(int i)
    {
      if (!this._statusList.Any<EquipmentStatusList>((Func<EquipmentStatusList, bool>) (x => x.EquipmentId == i)))
        return;
      foreach (EquipmentStatusList equipmentStatusList in this._statusList.Where<EquipmentStatusList>((Func<EquipmentStatusList, bool>) (x => x.EquipmentId == i)).ToList<EquipmentStatusList>())
        this._statusList.Remove(equipmentStatusList);
      this.MainWindowViewModel.EquipmentStatus = this._statusList;
    }

    private void OnPrinterError(object sender, EventArgs e) => this.DeviceError(3);

    private void DeviceError(int i)
    {
      if (this._statusList.Any<EquipmentStatusList>((Func<EquipmentStatusList, bool>) (x => x.EquipmentId == i)))
        return;
      this._statusList.Add(new EquipmentStatusList()
      {
        EquipmentId = i,
        EquipmentStatusId = 1
      });
      this.MainWindowViewModel.EquipmentStatus = this._statusList;
    }

    private void OnServiceItemReceived(object sender, EventArgs e)
    {
      if (!(e is RunWorkerCompletedEventArgs completedEventArgs))
        return;
      if (completedEventArgs.Result is List<ServiceParentItem>)
        this.MainWindowViewModel.ButtonsServiceParents = (List<ServiceParentItem>) completedEventArgs.Result;
      else if (completedEventArgs.Result is List<ServiceItem>)
      {
        this.MainWindowViewModel.ButtonsServices = (List<ServiceItem>) completedEventArgs.Result;
      }
      else
      {
        if (!(completedEventArgs.Result is List<ProductItem>))
          return;
        this.MainWindowViewModel.ButtonsProducts = (List<ProductItem>) completedEventArgs.Result;
      }
    }

    private void EqOnStatusChanged(object sender, EventArgs eventArgs)
    {
    }

    private void SetActivityTimer()
    {
      App.LastUserActivityTime = DateTime.Now;
      App._timer = new DispatcherTimer();
      App._timer.Tick += new EventHandler(this.OnTimerTick);
      App._timer.Interval = new TimeSpan(0, 0, 5);
      App._timer.Start();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
      if ((DateTime.Now - App.LastUserActivityTime).Seconds <= Global.ClearCartInterval)
        return;
      WindowService.CloseAllExceptOne((object) this.MainWindowViewModel);
      this.Cart.EmptyCart();
    }

    private void MainServiceClick(object param)
    {
      this.Button = param as ButtonItem;
      if (this.Button == null)
        return;
      if (this.Button.Type == (byte) 1)
      {
        this.MainWindowViewModel.ShowHairLines = false;
        this.MainWindowViewModel.Page = 0;
        this.MainWindowViewModel.Parent = (int) this.Button.Id;
        this.MainWindowViewModel.Shop = false;
        this.MainWindowViewModel.ProductParent = 0;
        this.MainWindowViewModel.Brand = 0;
      }
      else if (this.Button.Type == (byte) 0)
      {
        this.MainWindowViewModel.ShowHairLines = false;
        this.MainWindowViewModel.Page = 0;
        this.MainWindowViewModel.Shop = false;
        int? promo = this.Button.Promo;
        int num = 0;
        if (promo.GetValueOrDefault() == num & promo.HasValue)
        {
          if (!Global.ShowCart)
            this.Cart.EmptyCart();
          this.Cart.Add(this.Button);
          this.MainWindowViewModel.Cart = this.Cart;
          this.GotoCartWindowClick((object) null);
        }
        else
        {
          PromoViewModel promoViewModel = this.PromoViewModel;
          promoViewModel.ServiceItem = this.Button;
          WindowService.ShowWindow<PayByBonusWindow>((object) promoViewModel);
        }
      }
      else if (this.Button.Type == (byte) 5)
      {
        this.MainWindowViewModel.ShowHairLines = false;
        this.MainWindowViewModel.Page = 0;
        this.MainWindowViewModel.Shop = true;
        this.MainWindowViewModel.Parent = 0;
        this.MainWindowViewModel.ProductParent = 0;
        this.MainWindowViewModel.Brand = 0;
      }
      else if (this.Button.Type == (byte) 3)
      {
        this.MainWindowViewModel.ShowHairLines = false;
        this.MainWindowViewModel.Page = 0;
        this.MainWindowViewModel.ProductParent = (int) this.Button.Id;
        this.MainWindowViewModel.Shop = true;
        this.MainWindowViewModel.Parent = 0;
      }
      else if (this.Button.Type == (byte) 4)
      {
        this.MainWindowViewModel.ShowHairLines = false;
        this.MainWindowViewModel.Page = 0;
        this.MainWindowViewModel.Brand = (int) this.Button.Id;
        this.MainWindowViewModel.Shop = true;
        this.MainWindowViewModel.Parent = 0;
      }
      else
      {
        if (this.Button.Type != (byte) 2)
          return;
        this.MainWindowViewModel.ShowHairLines = false;
        this.MainWindowViewModel.Page = 0;
        this.MainWindowViewModel.Shop = true;
        if (!Global.ShowCart)
          this.Cart.EmptyCart();
        this.Cart.Add(this.Button);
        this.MainWindowViewModel.Cart = this.Cart;
        this.GotoCartWindowClick((object) null);
      }
    }

    public ButtonItem Button { get; set; }

    private void FrButtonClick(object obj) => WindowService.ShowWindow<FrWindow>((object) new FrViewModel());

    private void MainServiceNextButtonClick(object obj) => ++this.MainWindowViewModel.Page;

    private void MainServicePreviousButtonClick(object obj) => --this.MainWindowViewModel.Page;

    private static void GotoMainMenuCick(object param) => App.CloseWindow(param);

    private static void CloseWindow(object param)
    {
      if (!(param is ViewModelBase viewModelBase))
        return;
      viewModelBase.CloseCommand.Execute(param);
    }

    private async void GotoPaymentWindowCick(object param)
    {
      PaymentViewModel paymentViewModel;
      if (!(param is PhoneNumberViewModel viewModel))
      {
        viewModel = (PhoneNumberViewModel) null;
        paymentViewModel = (PaymentViewModel) null;
      }
      else
      {
        paymentViewModel = this.PaymentViewModel;
        paymentViewModel.CanPayByCash = PayoutService.Instance.IsOk;
        paymentViewModel.CanPayByCard = DataExchangeService.Instance.IsInternetOk && CardService.Instance.IsOk;
        paymentViewModel.Cart = this.Cart;
        if (this.Cart.Sum % 50 != 0)
          paymentViewModel.CanPayByCash = false;
        paymentViewModel.PaymentVisibility = !Global.ShowCart;
        paymentViewModel.Client = new BonusResponse()
        {
          Status = -99,
          Bonuses = 0,
          PhoneNumber = viewModel.PhoneNumber
        };
        WindowService.ShowWindow<PaymentWindow>((object) paymentViewModel);
        App.CloseWindow((object) viewModel);
        BonusResponse bonusResponse = await DataExchange.GetClientInfo(viewModel.PhoneNumber, viewModel.SexValue, viewModel.AgeMinValue, viewModel.AgeMaxValue);
        this.Client = bonusResponse;
        bonusResponse = (BonusResponse) null;
        if (this.Client.PhoneNumber < 70000000001L)
        {
          this.Client.Bonuses = 0;
          this.Client.PhoneNumber = 0L;
        }
        paymentViewModel.Client = this.Client;
        viewModel = (PhoneNumberViewModel) null;
        paymentViewModel = (PaymentViewModel) null;
      }
    }

    public BonusResponse Client { get; set; }

    private void GotoPhoneNumberWindowClick(object param)
    {
      WindowService.ShowWindow<PhoneNumberWindow>((object) this.PhoneNumberViewModel);
      this.PhoneNumberViewModel.CleanPhone();
      App.CloseWindow(param);
    }

    private void GotoCartWindowClick(object param)
    {
      if (Global.ShowCart)
      {
        WindowService.ShowWindow<CartWindow>((object) this.CartViewModel);
      }
      else
      {
        WindowService.ShowWindow<PhoneNumberWindow>((object) this.PhoneNumberViewModel);
        this.PhoneNumberViewModel.CleanPhone();
      }
      App.CloseWindow(param);
    }

    private static void ClosePaymentWindowClick(object param) => App.CloseWindow(param);

    private static void ClosePromoWindowClick(object param) => App.CloseWindow(param);

    private static void GotoPaymentWindowClick(object param)
    {
      if (!(param is PaymentViewModel paymentViewModel))
        return;
      paymentViewModel.PaymentVisibility = true;
    }

    private void ConfirmPaymentClick(object param)
    {
      if (!(param is PayByBonusViewModel byBonusViewModel) || byBonusViewModel.PinResponse == null)
        return;
      if (byBonusViewModel.Pin == byBonusViewModel.PinResponse.Pin)
      {
        if (byBonusViewModel.Client.Bonuses < byBonusViewModel.Cart.Sum)
          return;
        byBonusViewModel.IsWrongCode = false;
        this.PaymentSuccessful(new ListPaymentEvents(byBonusViewModel.Cart)
        {
          Change = 0,
          TotalAccepted = byBonusViewModel.Cart.Sum,
          TotalRejected = 0,
          Notes = new List<int>(),
          PaymentType = (short) 2,
          Phone = byBonusViewModel.Client.PhoneNumber
        });
      }
      else
        byBonusViewModel.IsWrongCode = true;
    }

    private void ConfirmPromoClick(object param)
    {
      if (!(param is PromoViewModel promoViewModel) || promoViewModel.ServiceItem == null)
        return;
      int pin = promoViewModel.Pin;
      int? promo = promoViewModel.ServiceItem.Promo;
      int valueOrDefault = promo.GetValueOrDefault();
      if (pin == valueOrDefault & promo.HasValue)
      {
        promoViewModel.IsWrongCode = false;
        this.Cart.Add(promoViewModel.ServiceItem);
        this.MainWindowViewModel.Cart = this.Cart;
        App.CloseWindow(param);
        this.GotoCartWindowClick((object) null);
      }
      else
        promoViewModel.IsWrongCode = true;
    }

    private void PayByCashClick(object param)
    {
      if (!(param is PaymentViewModel paymentViewModel))
        return;
      this._payByCashViewModel = new PayByCashViewModel()
      {
        Line1 = "Принимаемые номиналы купюр: 50, 100, 200, 500, 1000, 2000 руб.",
        Line2 = "Вы внесли:",
        Line4 = "Осталось внести {0} руб.",
        CancelButtonText = "Отменить",
        ServiceCost = 0M,
        TotalInserted = 0M,
        STotalInserted = "{0} руб.",
        CancelCommand = this.PayByCashWindowCancel
      };
      this._payByCashViewModel.ServiceCost = (Decimal) paymentViewModel.Cart.Sum;
      WindowService.ShowWindow<PayByCashWindow>((object) this._payByCashViewModel);
      PayoutService.Instance.ShutUpAndTakeMoney(paymentViewModel.Cart, paymentViewModel.Client);
    }

    private static void NotEnoughChange(object sender, EventArgs e) => WindowService.ShowWindow<PaymentResult>((object) new PaymentResultViewModel(2)
    {
      OkCommand = (ICommand) new Command(new Action<object>(App.ClosePaymentResultWindow), true)
    });

    private static void ClosePaymentResultWindow(object obj) => App.CloseWindow(obj);

    private void NoteInserted(object sender, EventArgs e)
    {
      ListPaymentEvents eventArgument = e as ListPaymentEvents;
      if (eventArgument == null)
        return;
// ?? Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Delegate) (() => this._payByCashViewModel.TotalInserted = (Decimal) eventArgument.TotalAccepted));
      _DelegateParam = eventArgument.TotalAccepted; // 05.06.22 
	  Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, myVoidDelegate5);
    }

    private void PaymentComplete(object sender, EventArgs e)
    {
      try
      {
        ListPaymentEvents eventArgument = e as ListPaymentEvents;
        if (eventArgument == null)
          return;
// ?? 	Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Delegate) (() => this._payByCashViewModel.TotalInserted = (Decimal) eventArgument.TotalAccepted));
        _DelegateParam = eventArgument.TotalAccepted; // 05.06.22
    	Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, myVoidDelegate5);
        this.PaymentSuccessful(eventArgument);
      }
      catch
      {
      }
    }

    private void PaymentSuccessful(ListPaymentEvents listPaymentEvents)
    {
      List<ChannelLevelInfo> info = new List<ChannelLevelInfo>();
      try
      {
        info = PayoutService.Instance.GetChannelLevelInfo2();
      }
      catch (Exception ex)
      {
      }
      try
      {
        foreach (PaymentEvent paymentEvent in listPaymentEvents.PaymentEvents)
          PaymentService.Instance.CreatePayment(Global.TerminalId, paymentEvent, listPaymentEvents, info);
        WindowService.CloseAllExceptOne((object) this.MainWindowViewModel);
        WindowService.ShowWindow<PaymentResult>((object) new PaymentResultViewModel(0)
        {
          OkCommand = (ICommand) new Command(new Action<object>(App.ClosePaymentResultWindow), true)
        });
        PrintService.Instance.PrintReceipt(listPaymentEvents, this.Client.Bonuses);
        this.Cart.EmptyCart();
      }
      catch
      {
      }
    }

    [Obsolete]
    private void PayByCashWindowCancelClick(object param)
    {
      if (param is PayByCashViewModel)
        App.CloseWindow(param);
      WindowService.ShowWindow<PaymentResult>((object) new PaymentResultViewModel(PayoutService.Instance.TotalAccepted == 0 ? 1 : 3)
      {
        OkCommand = (ICommand) new Command(new Action<object>(App.ClosePaymentResultWindow), true)
      });
      PayoutService.Instance.Cancel();
    }

    private void PayByCardClick(object obj)
    {
      if (this._pbc)
        return;
      this._pbc = true;
      PaymentViewModel paymentViewModel = obj as PaymentViewModel;
      if (paymentViewModel == null)
        return;
      this.PayByCardViewModel = new PayByCardViewModel()
      {
        Line1 = "Вставьте карту",
        Line2 = "Для отмены операции нажмите \"Отмена\" на клавиатуре",
        MiddleLine = string.Empty,
        CancelCommand = this.ClosePaymentWindow,
        Client = paymentViewModel.Client,
        Cart = paymentViewModel.Cart
      };
      WindowService.ShowWindow<PayByCardWindow>((object) this.PayByCardViewModel);
      this.source = new CancellationTokenSource();
      this.token = this.source.Token;
      Task.Run<string>((Func<string>) (() => CardService.Instance.TakeMoney((uint) paymentViewModel.Cart.Sum, paymentViewModel.Client, paymentViewModel.Cart)), this.token);
      this._pbc = false;
    }

    public bool _pbc { get; set; }

    private async void PayByBonusClick(object obj)
    {
      PayByBonusViewModel payByBonusViewModel;
      if (!(obj is PaymentViewModel paymentViewModel))
      {
        paymentViewModel = (PaymentViewModel) null;
        payByBonusViewModel = (PayByBonusViewModel) null;
      }
      else
      {
        payByBonusViewModel = this.PayByBonusViewModel;
        payByBonusViewModel.Client = paymentViewModel.Client;
        PayByBonusViewModel byBonusViewModel = payByBonusViewModel;
        PinResponse pinResponse = await DataExchange.GetPinResponse(paymentViewModel.Client.PhoneNumber);
        byBonusViewModel.PinResponse = pinResponse;
        byBonusViewModel = (PayByBonusViewModel) null;
        pinResponse = (PinResponse) null;
        payByBonusViewModel.Cart = paymentViewModel.Cart;
        WindowService.ShowWindow<PayByBonusWindow>((object) payByBonusViewModel);
        paymentViewModel = (PaymentViewModel) null;
        payByBonusViewModel = (PayByBonusViewModel) null;
      }
    }

    public static void OnUserActivity() => App.LastUserActivityTime = DateTime.Now;

    private static DateTime LastUserActivityTime { get; set; }

    public static void TimerStop() => App._timer.Stop();

    public static void TimerStart() => App._timer.Start();

    [STAThread]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public static void Main() => new App().Run();
  }
}
