// Decompiled with JetBrains decompiler
// Type: UIPSCore.Hardware.CardPayment.CardPaymentDevice
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Terminal.Card.CardPayment;
using UIPSCore.Exterior;
using UIPSCore.Interior;
using UIPSCore.Interior.Businesses;
using UIPSCore.Interior.Exceptions;
using UIPSCore.Logging;

namespace UIPSCore.Hardware.CardPayment
{
  internal class CardPaymentDevice : ICardPaymentDevice
  {
    private static readonly Dictionary<int, string> knownReportErrorCodes = new Dictionary<int, string>()
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
    private readonly IExceptionHandler _exceptionHandler;
    private readonly IUserActionWatcher _userActionWatcher;
    protected Thread BankCardDeviceThread;
    private CardPaymentDeviceData _deviceData;
    private readonly AutoResetEvent _startProcessEvent;
    private bool _needToStopProcessing;
    private PilotNt _pilotNt;
    private Action<uint, string, bool> _updateUi;
    private bool JustDecompileGenerated_IsActive_k__BackingField;
    private Action<uint, string, bool> _updateGui;

    public bool IsActive
    {
      get => this.JustDecompileGenerated_get_IsActive();
      set => this.JustDecompileGenerated_set_IsActive(value);
    }

    public bool JustDecompileGenerated_get_IsActive() => this.JustDecompileGenerated_IsActive_k__BackingField;

    private void JustDecompileGenerated_set_IsActive(bool value) => this.JustDecompileGenerated_IsActive_k__BackingField = value;

    public CardPaymentDevice(
      IExceptionHandler exceptionHandler,
      ILog log,
      IUserActionWatcher userActionWatcher)
    {
      this._exceptionHandler = exceptionHandler;
      this._userActionWatcher = userActionWatcher;
      this._startProcessEvent = new AutoResetEvent(false);
      this.IsActive = false;
      this._needToStopProcessing = false;
    }

    public CardPaymentDevice()
    {
    }

    public void CloseDay(out string errorText)
    {
      this._needToStopProcessing = true;
      errorText = (string) null;
      this._pilotNt = new PilotNt(this._updateGui);
      if (this._pilotNt != null)
      {
        int reportCode = this._pilotNt.CloseDay();
        if (reportCode > 0)
        {
          string reportErrorText = this.GetReportErrorText(reportCode);
          errorText = reportErrorText;
        }
        else if (reportCode != 0 || CloseDayResult.ResultRCode[0] != '0' && (CloseDayResult.ResultRCode[0] != '0' || CloseDayResult.ResultRCode[1] != '0'))
          ;
      }
      FPUHelper.FixFPU();
    }

    private void DumpAnswer7(PilotNt.auth_answer7 answer7)
    {
      try
      {
      }
      catch (Exception ex)
      {
      }
    }

    public string GetReportErrorText(int reportCode)
    {
      string str;
      return CardPaymentDevice.knownReportErrorCodes.TryGetValue(reportCode, out str) ? str : reportCode.ToString();
    }

    public string GetStatistics(out string errorText)
    {
      string statistics1 = string.Empty;
      this._needToStopProcessing = true;
      errorText = (string) null;
      this._pilotNt = new PilotNt(this._updateGui);
      if (this._pilotNt != null)
      {
        int statistics2 = this._pilotNt.GetStatistics();
        if (statistics2 > 0)
        {
          statistics1 = this.GetReportErrorText(statistics2);
          errorText = statistics1;
        }
        else if (statistics2 == 0)
          statistics1 = StatisticsResult.GetCheck();
        FPUHelper.FixFPU();
      }
      return statistics1;
    }

    public string GetTerminalID(out string errorText)
    {
      this._needToStopProcessing = true;
      errorText = string.Empty;
      string terminalId = string.Empty;
      byte[] pTerminalIDOut = (byte[]) null;
      this._pilotNt = new PilotNt(this._updateGui);
      if (this._pilotNt != null)
      {
        if (this._pilotNt.GetTerminalID(out pTerminalIDOut) <= 0)
        {
          string str = Encoding.GetEncoding("windows-1251").GetString(pTerminalIDOut);
          terminalId = str.Substring(0, str.IndexOf(char.MinValue));
        }
        else
          errorText = " Ошибка получения TerminalID";
        FPUHelper.FixFPU();
      }
      return terminalId;
    }

    public uint GetVer(out string errorText)
    {
      this._needToStopProcessing = true;
      errorText = string.Empty;
      this._pilotNt = new PilotNt(this._updateGui);
      uint ver = 0;
      if (this._pilotNt != null)
      {
        ver = this._pilotNt.GetVer();
        if (ver <= 0U)
          errorText = "Ошибка получения версии pilot_nt.dll";
        FPUHelper.FixFPU();
      }
      return ver;
    }

    private void ProcessingFunc()
    {
      EventWaitHandle eventWaitHandle = InfiniteWorker.GetEvent();
      while (true)
      {
        try
        {
          if (this._deviceData != null)
          {
            if (this._needToStopProcessing)
              break;
            CardPaymentDeviceData deviceData = this._deviceData;
            Encoding.GetEncoding("windows-1251");
            this.IsActive = true;
            this._needToStopProcessing = false;
            this._pilotNt = new PilotNt(this._updateGui);
            byte[] track2out = (byte[]) null;
            int num1 = this._pilotNt.ReadTrack2(out track2out);
            if (num1 > 0)
            {
              this._needToStopProcessing = true;
              this._pilotNt.GetHalDispFuncResultCmd();
              this._pilotNt.GetHalDispFuncResultPar1();
              this._pilotNt.GetHalDispFuncResultPar2();
              this.StopCardProcessing_1();
              FPUHelper.FixFPU();
            }
            else if (num1 == 0)
            {
              ReadTrack2Result.ResultTrack2 = track2out;
              PilotNt.auth_answer7 answer = new PilotNt.auth_answer7()
              {
                ans = new PilotNt.auth_answer()
              };
              string check = "";
              int num2 = this._pilotNt.CardAuthorize7((uint) this._deviceData.ExpectedMoney, track2out, out answer, out check);
              this.DumpAnswer7(answer);
              if (num2 > 0)
              {
                this._needToStopProcessing = true;
                this._deviceData.ProcessCompleteFunc(answer.ans.AMessage);
                this._pilotNt = (PilotNt) null;
                FPUHelper.FixFPU();
              }
              else if (num2 == 0)
              {
                string amessage = answer.ans.AMessage;
                this._needToStopProcessing = true;
                this._pilotNt = (PilotNt) null;
                FPUHelper.FixFPU();
                this._deviceData.ProcessCompleteFunc(amessage);
              }
            }
            else if (num1 < 0)
            {
              this._needToStopProcessing = true;
              this._pilotNt.GetHalDispFuncResultCmd();
              this._pilotNt.GetHalDispFuncResultPar1();
              this._pilotNt.GetHalDispFuncResultPar2();
              this.StopCardProcessing_1();
              FPUHelper.FixFPU();
            }
            this.IsActive = false;
          }
          if (eventWaitHandle.WaitOne(0))
            break;
        }
        catch (Exception ex)
        {
          this._exceptionHandler.HandleException((object) ex);
          this._deviceData = (CardPaymentDeviceData) null;
          this.IsActive = false;
          this._needToStopProcessing = false;
        }
      }
    }

    public void SetUpdateUi(Action<uint, string, bool> updateUi) => this._updateUi = updateUi;

    public void StartCardProcessing(
      IBusiness business,
      Decimal expectedMoney,
      Func<bool> isStoppedFunc,
      Action<string> processCompleteFunc)
    {
      this._needToStopProcessing = false;
      this.BankCardDeviceThread = new Thread(new ThreadStart(this.ProcessingFunc));
      this.BankCardDeviceThread.Start();
      this._deviceData = new CardPaymentDeviceData()
      {
        ExpectedMoney = expectedMoney,
        IsStoppedFunc = isStoppedFunc,
        ProcessCompleteFunc = processCompleteFunc
      };
    }

    public void StopCardProcessing_1()
    {
      this._needToStopProcessing = true;
      this._deviceData.ProcessCompleteFunc(this._pilotNt != null ? this._pilotNt.GetHalDispFuncResultPar1() : string.Empty);
      ReadTrack2Result.Clear();
    }

    public void StopCardProcessing_2() => this._needToStopProcessing = true;

    public void StopCardProcessing_EjectCard()
    {
      this._needToStopProcessing = true;
      if (this._pilotNt == null)
        return;
      this._pilotNt.EjectCard();
      FPUHelper.FixFPU();
    }
  }
}
