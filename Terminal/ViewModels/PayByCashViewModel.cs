// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.PayByCashViewModel
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Windows;
using System.Windows.Input;

namespace Terminal.ViewModels
{
  public class PayByCashViewModel : ViewModelBase
  {
    private string _line1;
    private string _line2;
    private string _stotalInserted;
    private Decimal _totalInserted;
    private string _line4;
    private Decimal _serviceCost;
    private string _cancelButtonText;
    private Visibility _isVisible;

    public string Line1
    {
      get => this._line1 ?? string.Empty;
      set
      {
        this._line1 = value;
        this.OnPropertyChanged();
      }
    }

    public string Line2
    {
      get => this._line2 ?? string.Empty;
      set
      {
        this._line2 = value;
        this.OnPropertyChanged();
      }
    }

    public string STotalInserted
    {
      get => string.Format(this._stotalInserted ?? string.Empty, (object) this.TotalInserted.ToString("G"));
      set => this._stotalInserted = value;
    }

    public Decimal TotalInserted
    {
      get => this._totalInserted;
      set
      {
        this._totalInserted = value;
        this.OnPropertyChanged();
        this.OnPropertyChanged("STotalInserted");
        this.OnPropertyChanged("Line4");
      }
    }

    public string Line4
    {
      get => string.Format(this._line4 ?? string.Empty, (object) (this.ServiceCost - this.TotalInserted < 0M ? 0M : this.ServiceCost - this.TotalInserted).ToString("F2"));
      set => this._line4 = value;
    }

    public Decimal ServiceCost
    {
      get => this._serviceCost;
      set
      {
        this._serviceCost = value;
        this.OnPropertyChanged();
      }
    }

    public ICommand CancelCommand { get; set; }

    public string CancelButtonText
    {
      get => this._cancelButtonText;
      set
      {
        this._cancelButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public Visibility IsVisible
    {
      get => this._isVisible;
      set
      {
        this._isVisible = value;
        this.OnPropertyChanged();
      }
    }
  }
}
