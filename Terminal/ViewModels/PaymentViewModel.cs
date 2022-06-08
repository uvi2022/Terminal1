// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.PaymentViewModel
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.Windows.Input;
using Terminal.Classes;
using Terminal.Services;
using Terminal.View;

namespace Terminal.ViewModels
{
  public class PaymentViewModel : ViewModelBase
  {
    private Cart _cart;
    private BonusResponse _client;
    private string _line1;
    private string _line2;
    private string _line3;
    private string _line3Empty;
    private string _line5;
    private bool _canPayByCash;
    private string _payByBonusButtonText;
    private string _payByCardButtonText;
    private string _payByCashButtonText;
    private string _cartButtonText;
    private string _paymentButtonText;
    private bool _canPayByCard;
    private bool _paymentVisibility;

    public ICommand PayByCashCommand { get; set; }

    public ICommand PayByCardCommand { get; set; }

    public ICommand PayByBonusCommand { get; set; }

    public ICommand GoToCartCommand { get; set; }

    public ICommand GoToPaymentCommand { get; set; }

    public Cart Cart
    {
      get => this._cart ?? new Cart();
      set
      {
        this._cart = value;
        this.Line1 = "Оплата";
        this.Line2 = !(this._cart.CountServices == 0 & this._cart.CountProducts == 0) ? (!(this._cart.CountServices == 1 & this._cart.CountProducts == 0) ? (!(this._cart.CountServices == 0 & this._cart.CountProducts == 1) ? (!(this._cart.CountServices > 1 & this._cart.CountProducts == 0) ? (!(this._cart.CountServices == 0 & this._cart.CountProducts > 1) ? (!(this._cart.CountServices > 0 & this._cart.CountProducts > 0) ? "Ошибка корзины" : "Пожалуйста проверьте правильность выбранных Вами товаров и услуг.\r\nЕсли всё правильно, выберите способ оплаты.") : "Пожалуйста проверьте правильность выбранных Вами товаров.\r\nЕсли всё правильно, выберите способ оплаты.") : "Пожалуйста проверьте правильность выбранных Вами услуг.\r\nЕсли всё правильно, выберите способ оплаты.") : "Пожалуйста проверьте правильность выбранного Вами товара.\r\nЕсли всё правильно, выберите способ оплаты.") : "Пожалуйста проверьте правильность выбранной Вами услуги.\r\nЕсли всё правильно, выберите способ оплаты.") : "Ничего не выбрано.\r\nВернитесь на главный экран и выберите товары или услуги.";
        this.OnPropertyChanged("Price");
        this.OnPropertyChanged("ServiceName");
        this.OnPropertyChanged("CanPayByBonuses");
      }
    }

    public BonusResponse Client
    {
      get => this._client ?? new BonusResponse();
      set
      {
        this._client = value;
        this.OnPropertyChanged("Bonuses");
        this.OnPropertyChanged("CanPayByBonuses");
        this.Discount();
      }
    }

    private async void Discount()
    {
      PayByBonusViewModel payByBonusViewModel;
      if (!this.Client.DiscountNewPhone)
      {
        payByBonusViewModel = (PayByBonusViewModel) null;
      }
      else
      {
        payByBonusViewModel = this.DiscountViewModel;
        payByBonusViewModel.PlayMedia = true;
        payByBonusViewModel.Client = this.Client;
        PayByBonusViewModel byBonusViewModel = payByBonusViewModel;
        PinResponse pinResponse = await DataExchange.GetPinResponse(this.Client.PhoneNumber);
        byBonusViewModel.PinResponse = pinResponse;
        byBonusViewModel = (PayByBonusViewModel) null;
        pinResponse = (PinResponse) null;
        payByBonusViewModel.Cart = this.Cart;
        WindowService.ShowWindow<PayByBonusWindow>((object) payByBonusViewModel);
        payByBonusViewModel = (PayByBonusViewModel) null;
      }
    }

    public PayByBonusViewModel DiscountViewModel => new PayByBonusViewModel()
    {
      Line1 = "Для получения скидки введите уникальный СМС код, полученный на ваш мобильный телефон",
      Line2 = "смс код:",
      ErrorText = "Уникальный СМС код не верен",
      ErrorButtonText = "Набрать код заново",
      ConfirmButtonText = "Подтвердить",
      CancelButtonText = "Отменить",
      CancelCommand = this.ClosePaymentWindow,
      ConfirmCommand = this.ConfirmPaymentCommand,
      IsWrongCode = false
    };

    public ICommand ClosePaymentWindow => (ICommand) new Command(new Action<object>(PaymentViewModel.ClosePaymentWindowClick), true);

    private static void ClosePaymentWindowClick(object param) => PaymentViewModel.CloseWindow(param);

    private static void CloseWindow(object param)
    {
      if (!(param is ViewModelBase viewModelBase))
        return;
      viewModelBase.CloseCommand.Execute(param);
    }

    public ICommand ConfirmPaymentCommand => (ICommand) new Command(new Action<object>(this.ConfirmPaymentClick), true);

    private void ConfirmPaymentClick(object param)
    {
      if (!(param is PayByBonusViewModel byBonusViewModel) || byBonusViewModel.PinResponse == null)
        return;
      if (byBonusViewModel.Pin == byBonusViewModel.PinResponse.Pin)
        PaymentViewModel.CloseWindow(param);
      else
        PaymentViewModel.CloseWindow(param);
    }

    public string Price => string.Format("{0:F2} {1}", (object) this.Cart.Sum, (object) Global.Currency);

    public string ServiceName => this.Cart.ServiceName;

    public string Line1
    {
      get => this._line1;
      set
      {
        this._line1 = value;
        this.OnPropertyChanged();
      }
    }

    public string Line2
    {
      get => this._line2;
      set
      {
        this._line2 = value;
        this.OnPropertyChanged();
      }
    }

    public string Line3
    {
      get => this._line3 ?? string.Empty;
      set
      {
        this._line3 = value;
        this.OnPropertyChanged("Bonuses");
      }
    }

    public string Line3Empty
    {
      get => this._line3Empty ?? string.Empty;
      set
      {
        this._line3Empty = value;
        this.OnPropertyChanged("Bonuses");
      }
    }

    public string Bonuses
    {
      get
      {
        if (this.Client.PhoneNumber == 0L || this.Client.PhoneNumber == 7L)
          return this.Line3Empty;
        return this.Client.Status == -99 ? this.Line3InProcess : string.Format(this.Line3, (object) this.Client.Bonuses);
      }
    }

    public string Line5
    {
      get => string.Format("{0} {1:F2} {2}", (object) this._line5, (object) this.Cart.Sum, (object) Global.Currency);
      set
      {
        this._line5 = value;
        this.OnPropertyChanged();
      }
    }

    public bool CanPayByBonuses => this.Client.Bonuses >= this.Cart.Sum & this.Cart.CountProducts == 0 & this.Cart.Sum >= 50;

    public bool CanPayByCash
    {
      get => this._canPayByCash;
      set
      {
        this._canPayByCash = value;
        this.OnPropertyChanged();
      }
    }

    public string PayByBonusButtonText
    {
      get => this._payByBonusButtonText;
      set
      {
        this._payByBonusButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public string PayByCardButtonText
    {
      get => this._payByCardButtonText;
      set
      {
        this._payByCardButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public string PayByCashButtonText
    {
      get => this._payByCashButtonText;
      set
      {
        this._payByCashButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public string CartButtonText
    {
      get => this._cartButtonText;
      set
      {
        this._cartButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public string PaymentButtonText
    {
      get => this._paymentButtonText;
      set
      {
        this._paymentButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public bool CanPayByCard
    {
      get => this._canPayByCard;
      set
      {
        this._canPayByCard = value;
        this.OnPropertyChanged();
      }
    }

    public bool PaymentVisibility
    {
      get => this._paymentVisibility;
      set
      {
        this._paymentVisibility = value;
        this.OnPropertyChanged();
      }
    }

    public bool PaymentVisibilityInverse => !this._paymentVisibility;

    public string InfBlock2 { get; set; }

    public string InfBlock3 { get; set; }

    public string InfBlock4 { get; set; }

    public string Version { get; set; }

    public string Line3InProcess { get; set; }
  }
}
