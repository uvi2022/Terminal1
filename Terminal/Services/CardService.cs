// Decompiled with JetBrains decompiler
// Type: Terminal.Services.CardService
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Terminal.Card.CardPayment;
using Terminal.Classes;
using UIPSCore.Hardware.CardPayment;

namespace Terminal.Services
{
  public sealed class CardService
  {
    private static readonly System.Lazy<CardService> Lazy = new System.Lazy<CardService>((Func<CardService>) (() => new CardService()));
    public Dictionary<int, string> KnownReportErrorCodes = new Dictionary<int, string>()
    {
      {
        12,
        "Неверная настройка терминала"
      },
      {
        99,
        "Нарушился контакт с пинпадом"
      },
      {
        361,
        "Нарушился контакт с чипом карты"
      },
      {
        362,
        "Нарушился контакт с чипом карты"
      },
      {
        363,
        "Нарушился контакт с чипом карты"
      },
      {
        364,
        "Нарушился контакт с чипом карты"
      },
      {
        403,
        "Клиент ошибся при вводе ПИНа"
      },
      {
        405,
        "ПИН клиента заблокирован"
      },
      {
        444,
        "Истек срок действия карты"
      },
      {
        507,
        "Истек срок действия карты"
      },
      {
        518,
        "На терминале установлена неверная дата"
      },
      {
        521,
        "На карте недостаточно средств"
      },
      {
        572,
        "Истек срок действия карты"
      },
      {
        574,
        "Карта заблокирована"
      },
      {
        579,
        "Карта заблокирована"
      },
      {
        584,
        "Истек период обслуживания карты"
      },
      {
        585,
        "Истек период обслуживания карты"
      },
      {
        705,
        "Карта заблокирована"
      },
      {
        706,
        "Карта заблокирована"
      },
      {
        707,
        "Карта заблокирована"
      },
      {
        708,
        "ПИН клиента заблокирован"
      },
      {
        709,
        "ПИН клиента заблокирован"
      },
      {
        2000,
        "Операция прервана нажатием клавиши ОТМЕНА"
      },
      {
        2002,
        "Клиент слишком долго вводит ПИН"
      },
      {
        2004,
        "Карта заблокирована"
      },
      {
        2005,
        "Карта заблокирована"
      },
      {
        2006,
        "Карта заблокирована"
      },
      {
        2007,
        "Карта заблокирована"
      },
      {
        2405,
        "Карта заблокирована"
      },
      {
        2406,
        "Карта заблокирована"
      },
      {
        2407,
        "Карта заблокирована"
      },
      {
        3001,
        "Недостаточно средств для загрузки на карту"
      },
      {
        3002,
        "По карте клиента числится прерванная загрузка средств"
      },
      {
        3019,
        "На сервере проводятся регламентные работы"
      },
      {
        3020,
        "На сервере проводятся регламентные работы"
      },
      {
        3021,
        "На сервере проводятся регламентные работы"
      },
      {
        4100,
        "На сервере проводятся регламентные работы"
      },
      {
        4119,
        "Нет связи с банком"
      },
      {
        4101,
        "Карта терминала не проинкассирована"
      },
      {
        4102,
        "Карта терминала не проинкассирована"
      },
      {
        4103,
        "Ошибка обмена с чипом карты"
      },
      {
        4104,
        "Ошибка обмена с чипом карты"
      },
      {
        4108,
        "Неправильно введен или прочитан номер карты"
      },
      {
        4110,
        "Требуется проинкассировать карту терминала"
      },
      {
        4111,
        "Требуется проинкассировать карту терминала"
      },
      {
        4112,
        "Требуется проинкассировать карту терминала"
      },
      {
        4113,
        "Превышен лимит, допустимый без связи с банком"
      },
      {
        4114,
        "Превышен лимит, допустимый без связи с банком"
      },
      {
        4115,
        "Ручной ввод для таких карт запрещен"
      },
      {
        4116,
        "Введены неверные 4 последних цифры номера карты"
      },
      {
        4117,
        "Клиент отказался от ввода ПИНа"
      },
      {
        4120,
        "Неисправен пинпад"
      },
      {
        4125,
        "На карте есть чип"
      },
      {
        4128,
        "Неверная настройка терминала"
      },
      {
        4130,
        "Память терминала заполнена"
      },
      {
        4131,
        "Был заменен пинпад"
      },
      {
        4132,
        "Операция отклонена картой. Возможно, карту вытащили из чипового ридера до завершения печати чека"
      },
      {
        4134,
        "Слишком долго не выполнялась сверка тогов на терминале"
      },
      {
        4300,
        "Неправильно настроена касса"
      },
      {
        4301,
        "Неправильно настроена касса"
      },
      {
        4302,
        "Неправильно настроена касса"
      },
      {
        4303,
        "Неправильно настроена касса"
      },
      {
        4305,
        "Неправильно настроена касса"
      },
      {
        4306,
        "Неправильно настроена касса"
      },
      {
        4308,
        "Неправильно настроена касса"
      },
      {
        4401,
        "Нужно позвонить в банк"
      },
      {
        4404,
        "Получена команда изъять карту"
      },
      {
        4405,
        "Отказать без указания причины."
      },
      {
        4407,
        "Получена команда изъять карту"
      },
      {
        4419,
        "На сервере проводятся регламентные работы"
      },
      {
        4441,
        "Получена команда изъять карту"
      },
      {
        4443,
        "Получена команда изъять карту"
      },
      {
        4451,
        "На карте недостаточно средств"
      },
      {
        4454,
        "Карта просрочена"
      },
      {
        4455,
        "Клиент ошибся при вводе ПИНа"
      },
      {
        4457,
        "Операция не разрешена по причинам, связанным с картой"
      },
      {
        4458,
        "Операция не разрешена по причинам, связанным с настройкой терминала"
      },
      {
        4468,
        "На сервере проводятся регламентные работы"
      },
      {
        4475,
        "Клиент трижды ошибся при вводе ПИНа, и теперь он заблокирован"
      },
      {
        4496,
        "Неверная настройка терминала"
      },
      {
        4497,
        "На сервере проводятся регламентные работы"
      },
      {
        4498,
        "Неверная настройка терминала"
      },
      {
        5000,
        "Неверная настройка терминала или нарушены данные на чипе карты"
      },
      {
        5056,
        "Неверная настройка терминала или нарушены данные на чипе карты"
      },
      {
        5100,
        "Нарушены данные на чипе карты"
      },
      {
        5101,
        "Нарушены данные на чипе карты"
      },
      {
        5102,
        "Нарушены данные на чипе карты"
      },
      {
        5103,
        "Нарушены данные на чипе карты"
      },
      {
        5104,
        "Нарушены данные на чипе карты"
      },
      {
        5105,
        "Нарушены данные на чипе карты"
      },
      {
        5106,
        "Нарушены данные на чипе карты"
      },
      {
        5107,
        "Нарушены данные на чипе карты"
      },
      {
        5108,
        "Нарушены данные на чипе карты"
      },
      {
        5109,
        "Срок действия карты истек"
      },
      {
        5110,
        "Срок действия карты еще не начался"
      },
      {
        5111,
        "Для этой карты такая операция не разрешена"
      },
      {
        5116,
        "Клиент отказался от ввода ПИНа"
      },
      {
        5120,
        "Клиент отказался от ввода ПИНа"
      },
      {
        5133,
        "Операция отклонена картой"
      }
    };
    private static readonly DispatcherTimer Timer = new DispatcherTimer();
    private bool _needToStopProcessing;
    private readonly Action<uint, string, bool> _updateGui;

    public static CardService Instance => CardService.Lazy.Value;

    public event ChangedEventHandler OnPaymentComplete;

    public event ChangedEventHandler OnPaymentCancelled;

    public event ChangedEventHandler OnCallBack;

    private void PaymentComplete(EventArgs e)
    {
      ChangedEventHandler onPaymentComplete = this.OnPaymentComplete;
      if (onPaymentComplete == null)
        return;
      onPaymentComplete((object) this, e);
    }

    private void PaymentCancelled(EventArgs e)
    {
      ChangedEventHandler paymentCancelled = this.OnPaymentCancelled;
      if (paymentCancelled == null)
        return;
      paymentCancelled((object) this, e);
    }

    private void CallBack(EventArgs e)
    {
      ChangedEventHandler onCallBack = this.OnCallBack;
      if (onCallBack == null)
        return;
      onCallBack((object) this, e);
    }

    public bool IsBusy { get; set; }

    private CardService()
    {
      this._updateGui = new Action<uint, string, bool>(this.CallBackFromPinpad);
      this.IsOk = true;
    }

    private void CallBackFromPinpad(uint arg1, string arg2, bool arg3)
    {
      if (arg2 == string.Empty || arg2 == "Вставьте карту")
        return;
      this.CallBack((EventArgs) new PinpadEvent()
      {
        Caption = arg2
      });
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
    }

    public string TakeMoney(uint value, BonusResponse client, Cart cart)
    {
      this.Client = client;
      this.Cart = cart;
      string check = (string) null;
      Logger.Log.Info((object) "Стопим проверку Купюрника");
      LoggerNV200.Log.Info((object) "Стопим проверку Купюрника");
      PayoutService.CheckPayoutTimer.Stop();
      try
      {
        if (!this._needToStopProcessing)
        {
          this.IsBusy = true;
          this._needToStopProcessing = false;
          Logger.Log.Info((object) "Начало процедуры оплаты по карте");
          using (PilotNt pilotNt = new PilotNt(this._updateGui))
          {
            int res1 = 0;
            byte[] track2 = (byte[]) null;
            if (res1 > 0)
            {
              this._needToStopProcessing = true;
              ReadTrack2Result.Clear();
              this.PaymentCancelled((EventArgs) new PinpadEvent()
              {
                Caption = this.GetErrorText(res1)
              });
              Logger.Log.Error((object) this.GetErrorText(res1));
              DataExchange.SendCardStatus(this.GetErrorText(res1));
            }
            else if (res1 == 0)
            {
              ReadTrack2Result.ResultTrack2 = track2;
              PilotNt.auth_answer7 answer = new PilotNt.auth_answer7()
              {
                ans = new PilotNt.auth_answer()
              };
              int res2 = pilotNt.CardAuthorize7(value * 100U, track2, out answer, out check);
              if (res2 > 0)
              {
                this._needToStopProcessing = true;
                string amessage = answer.ans.AMessage;
                this.PaymentCancelled((EventArgs) new PinpadEvent()
                {
                  Caption = this.GetErrorText(res2)
                });
                Logger.Log.Error((object) this.GetErrorText(res2));
                DataExchange.SendCardStatus(this.GetErrorText(res2));
              }
              else if (res2 == 0)
              {
                this._needToStopProcessing = true;
                this.PaymentComplete((EventArgs) new ListPaymentEvents(this.Cart)
                {
                  TotalAccepted = this.Cart.Sum,
                  Sum = this.Cart.Sum,
                  Change = 0,
                  PaymentType = (short) 3,
                  Notes = new List<int>(),
                  Phone = this.Client.PhoneNumber,
                  BankCheck = check
                });
                DataExchange.SendCardStatus("OK");
              }
              Logger.Log.Info((object) "Банковский чек");
              Logger.Log.Info((object) check);
            }
            else if (res1 < 0)
            {
              this._needToStopProcessing = true;
              ReadTrack2Result.Clear();
              this.PaymentCancelled((EventArgs) new PinpadEvent()
              {
                Caption = this.GetErrorText(res1)
              });
              Logger.Log.Error((object) this.GetErrorText(res1));
              DataExchange.SendCardStatus(this.GetErrorText(res1));
            }
            this.IsBusy = false;
          }
        }
      }
      catch (Exception ex)
      {
        Logger.Log.Warn((object) ex.ToString());
        DataExchange.SendCardStatus(ex.ToString());
        this.IsBusy = false;
        this._needToStopProcessing = false;
      }
      Logger.Log.Info((object) "Окончание процедуры оплаты по карте");
      Logger.Log.Info((object) "Запускаем проверку Купюрника");
      LoggerNV200.Log.Info((object) "Запускаем проверку Купюрника");
      PayoutService.CheckPayoutTimer.Start();
      this._needToStopProcessing = false;
      this.IsBusy = false;
      return check;
    }

    private string GetErrorText(int res)
    {
      string errorText = "";
      if (this.KnownReportErrorCodes.ContainsKey(res))
        errorText = this.KnownReportErrorCodes[res];
      return errorText;
    }

    public Cart Cart { get; set; }

    public BonusResponse Client { get; set; }

    public bool IsOk { get; set; }

    public void StopCardProcessing_1()
    {
      this._needToStopProcessing = true;
      ReadTrack2Result.Clear();
    }

    public void StopCardProcessing_2() => this._needToStopProcessing = true;

    public void StopCardProcessing_EjectCard()
    {
      this._needToStopProcessing = true;
      new PilotNt(this._updateGui).EjectCard();
      FPUHelper.FixFPU();
    }

    public int CloseDay() => new PilotNt(this._updateGui).CloseDay();
  }
}
