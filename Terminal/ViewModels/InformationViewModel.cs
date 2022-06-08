// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.InformationViewModel
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System.Windows.Input;

namespace Terminal.ViewModels
{
  public class InformationViewModel : ViewModelBase
  {
    private string _line1;
    private string _line2;

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

    public ICommand CancelCommand { get; set; }
  }
}
