// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.PromoViewModel
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.Windows;
using System.Windows.Input;
using Terminal.Classes;

namespace Terminal.ViewModels
{
  public class PromoViewModel : ViewModelBase
  {
    private string _line1;
    private string _smsCode;
    private bool _isWrongCode;
    private Visibility _isMainGridVisible;
    private string _line2;
    private string _errorText;
    private string _errorButtonText;
    private string _confirmButtonText;
    private string _cancelButtonText;

    public ICommand CancelCommand { get; set; }

    public ICommand ConfirmCommand { get; set; }

    public ICommand RepeatCommand => (ICommand) new Command(new Action<object>(this.RepeatCommandClick), true);

    public ICommand AddDigitCommand => (ICommand) new Command(new Action<object>(this.AddDigitClick), true);

    public string Line1
    {
      get => this._line1;
      set
      {
        this._line1 = value;
        this.OnPropertyChanged();
      }
    }

    public string SmsCode
    {
      get => this._smsCode;
      set
      {
        this._smsCode = value;
        this.OnPropertyChanged();
      }
    }

    public int Pin
    {
      get
      {
        int result;
        int.TryParse(this.SmsCode, out result);
        return result;
      }
    }

    public bool IsWrongCode
    {
      get => this._isWrongCode;
      set
      {
        this._isWrongCode = value;
        this.SetMainGridVisibility(!value);
        this.OnPropertyChanged();
      }
    }

    private void SetMainGridVisibility(bool v) => this.IsMainGridVisible = v ? Visibility.Visible : Visibility.Collapsed;

    public Visibility IsMainGridVisible
    {
      get => this._isMainGridVisible;
      set
      {
        this._isMainGridVisible = value;
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

    public string ErrorText
    {
      get => this._errorText;
      set
      {
        this._errorText = value;
        this.OnPropertyChanged();
      }
    }

    public string ErrorButtonText
    {
      get => this._errorButtonText;
      set
      {
        this._errorButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public string ConfirmButtonText
    {
      get => this._confirmButtonText;
      set
      {
        this._confirmButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public string CancelButtonText
    {
      get => this._cancelButtonText;
      set
      {
        this._cancelButtonText = value;
        this.OnPropertyChanged();
      }
    }

    public ButtonItem ServiceItem { get; set; }

    private void RepeatCommandClick(object obj)
    {
      this.SmsCode = string.Empty;
      this.IsWrongCode = false;
    }

    private void AddDigitClick(object param)
    {
      string s = param as string;
      switch (s)
      {
        case "C":
          this.SymbolRemove();
          break;
        case null:
          break;
        default:
          this.SymbolAdd(s);
          break;
      }
    }

    private void SymbolAdd(string s) => this.SmsCode += s;

    private void SymbolRemove()
    {
      if (this.SmsCode.Length == 0)
        return;
      this.SmsCode = this.SmsCode.Remove(this.SmsCode.Length - 1);
    }
  }
}
