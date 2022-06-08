// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.PaymentResultViewModel
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System.Windows.Input;

namespace Terminal.ViewModels
{
  public class PaymentResultViewModel : ViewModelBase
  {
    public PaymentResultViewModel(int i = -1)
    {
      this.Result = i;
      if (i == 0 || i == 4)
      {
        this.StatusText1 = "Оплата завершена";
        this.StatusText2 = i == 4 ? "Дождитесь сдачи и печати чека" : "Дождитесь печати чека";
      }
      if (i == 1 || i == 3)
      {
        this.StatusText1 = "Операция отменена";
        if (i == 3)
          this.StatusText2 = "Дождитесь возврата внесеных стредств";
      }
      if (i != 2)
        return;
      this.StatusText1 = "Извините, в терминале отсутствует сдача";
      this.StatusText2 = "Внесите, пожалуйста, без сдачи";
    }

    public PaymentResultViewModel()
    {
    }

    public string StatusText1 { get; set; }

    public string StatusText2 { get; set; }

    public int Result { get; set; }

    public string OkButtonText => "ОК";

    public ICommand OkCommand { get; set; }
  }
}
