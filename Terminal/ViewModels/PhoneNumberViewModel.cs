// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.PhoneNumberViewModel
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.Linq;
using System.Windows.Input;
using Terminal.Classes;

namespace Terminal.ViewModels
{
  public class PhoneNumberViewModel : ViewModelBase
  {
    private string _name;
    private string _infBlock2;
    private string _infBlock3;
    private string _infBlock4;
    private string _continueLabel;
    private string _headerLabel;
    private string _headerLabel2;
    private string _headerLabelBonus;
    private string _sexLabel;
    private string _ageLabel;
    private ICommand _checkBoxClickCommand;
    private string _phone;
    private string _phoneMaskedText;
    private ICommand _addDigitCommand;

    public string Name
    {
      get => this._name;
      set
      {
        this._name = value;
        this.OnPropertyChanged();
      }
    }

    public string InfBlock2
    {
      get => this._infBlock2;
      set
      {
        this._infBlock2 = value;
        this.OnPropertyChanged();
      }
    }

    public string InfBlock3
    {
      get => this._infBlock3;
      set
      {
        this._infBlock3 = value;
        this.OnPropertyChanged();
      }
    }

    public string InfBlock4
    {
      get => this._infBlock4;
      set
      {
        this._infBlock4 = value;
        this.OnPropertyChanged();
      }
    }

    public string Version { get; set; }

    public bool ShowRadio { get; set; }

    public string ContinueLabel
    {
      get => this._continueLabel;
      set
      {
        this._continueLabel = value;
        this.OnPropertyChanged();
      }
    }

    public string HeaderLabel
    {
      get => this._headerLabel;
      set
      {
        this._headerLabel = value;
        this.OnPropertyChanged();
      }
    }

    public string HeaderLabel2
    {
      get => this._headerLabel2;
      set
      {
        this._headerLabel2 = value;
        this.OnPropertyChanged();
      }
    }

    public string HeaderLabelBonus
    {
      get => this._headerLabelBonus;
      set
      {
        this._headerLabelBonus = value;
        this.OnPropertyChanged();
      }
    }

    public string SexLabel
    {
      get => this._sexLabel;
      set
      {
        this._sexLabel = value;
        this.OnPropertyChanged();
      }
    }

    public string SexValue { get; private set; }

    public string AgeLabel
    {
      get => this._ageLabel;
      set
      {
        this._ageLabel = value;
        this.OnPropertyChanged();
      }
    }

    public byte? AgeMinValue { get; private set; }

    public byte? AgeMaxValue { get; private set; }

    public ICommand CheckBoxClickCommand => this._checkBoxClickCommand ?? (this._checkBoxClickCommand = (ICommand) new Command(new Action<object>(this.CheckBoxClick), true));

    private void CheckBoxClick(object param)
    {
      if (!(param is string str))
        return;
      char[] chArray = new char[1]{ '_' };
      string[] strArray = str.Split(chArray);
      if (strArray[0] == "sex")
      {
        this.SexValue = strArray[1];
      }
      else
      {
        if (!(strArray[0] == "age"))
          return;
        try
        {
          this.AgeMinValue = new byte?(byte.Parse(strArray[1]));
        }
        catch
        {
          this.AgeMinValue = new byte?();
        }
        try
        {
          this.AgeMaxValue = new byte?(byte.Parse(strArray[2]));
        }
        catch
        {
          this.AgeMaxValue = new byte?();
        }
      }
    }

    public string Phone
    {
      get => this._phone;
      set
      {
        this._phone = value;
        this.PhoneMaskedText = this.MakeMaskedString(this._phone);
      }
    }

    public long PhoneNumber
    {
      get
      {
        long result;
        long.TryParse("7" + this._phone, out result);
        return result;
      }
    }

    public string MakeMaskedString(string phone) => phone.Aggregate<char, string>("8(___)___-____", (Func<string, char, string>) ((current, d) => this.ReplaceFirst(current, "_", d.ToString())));

    private string ReplaceFirst(string text, string search, string replace)
    {
      int length = text.IndexOf(search);
      return length < 0 ? text : text.Substring(0, length) + replace + text.Substring(length + search.Length);
    }

    public string PhoneMaskedText
    {
      get => this._phoneMaskedText ?? "8(___)___-____";
      set
      {
        this._phoneMaskedText = value;
        this.OnPropertyChanged();
      }
    }

    public ICommand AddDigitCommand => this._addDigitCommand ?? (this._addDigitCommand = (ICommand) new Command(new Action<object>(this.AddDigit), true));

    public Cart Cart { get; set; }

    public int Bonus { get; set; }

    private void AddDigit(object param)
    {
      if (this.Phone == null)
        this.Phone = string.Empty;
      if (!(param is string str))
        return;
      if (str == "C" && this.Phone.Length > 0)
      {
        this.Phone = this.Phone.Remove(this.Phone.Length - 1);
      }
      else
      {
        if (str == "C" || this.Phone.Length >= 10)
          return;
        this.Phone += str;
      }
    }

    public void CleanPhone() => this.Phone = "";
  }
}
