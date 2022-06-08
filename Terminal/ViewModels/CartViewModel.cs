// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.CartViewModel
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Terminal.Classes;

namespace Terminal.ViewModels
{
  public class CartViewModel : ViewModelBase
  {
    private Cart _cart;
    private string _line1;
    private string _line2;
    private string _line5;

    public Cart Cart
    {
      get => this._cart ?? new Cart();
      set
      {
        this._cart = value;
        this.Line1 = "Ваша корзина";
        this.Line2 = !(this._cart.CountServices == 0 & this._cart.CountProducts == 0) ? (!(this._cart.CountServices == 1 & this._cart.CountProducts == 0) ? (!(this._cart.CountServices == 0 & this._cart.CountProducts == 1) ? (!(this._cart.CountServices > 1 & this._cart.CountProducts == 0) ? (!(this._cart.CountServices == 0 & this._cart.CountProducts > 1) ? (!(this._cart.CountServices > 0 & this._cart.CountProducts > 0) ? "Ошибка корзины" : "Пожалуйста проверьте правильность выбранных Вами товаров и услуг.\r\nЕсли всё правильно, нажмите Далее.") : "Пожалуйста проверьте правильность выбранных Вами товаров.\r\nЕсли всё правильно, нажмите Далее.") : "Пожалуйста проверьте правильность выбранных Вами услуг.\r\nЕсли всё правильно, нажмите Далее.") : "Пожалуйста проверьте правильность выбранного Вами товара.\r\nЕсли всё правильно, нажмите Далее") : "Пожалуйста проверьте правильность выбранной Вами услуги.\r\nЕсли всё верно, нажмите Далее.") : "Ничего не выбрано.\r\nВернитесь на главный экран и выберите товары или услуги.";
        this.OnPropertyChanged("Line1");
        this.OnPropertyChanged("Line2");
        this.OnPropertyChanged("Line5");
        this.OnPropertyChanged("CartLines");
      }
    }

    public ICommand CloseCartWindow => (ICommand) new Command(new Action<object>(CartViewModel.CloseCartWindowClick), true);

    private static void CloseCartWindowClick(object param) => CartViewModel.CloseWindow(param);

    private static void CloseWindow(object param)
    {
      if (!(param is ViewModelBase viewModelBase))
        return;
      viewModelBase.CloseCommand.Execute(param);
    }

    public ICommand ClearCart => (ICommand) new Command(new Action<object>(this.ClearCartClick), true);

    private void ClearCartClick(object param)
    {
      this.Cart.EmptyCart();
      if (!(param is ViewModelBase viewModelBase))
        return;
      viewModelBase.CloseCommand.Execute(param);
    }

    public ICommand DeleteCommand => (ICommand) new Command(new Action<object>(this.DeleteCartItem), true);

    private void DeleteCartItem(object param)
    {
      IList<DataGridCellInfo> dataGridCellInfoList = param as IList<DataGridCellInfo>;
      try
      {
        DataGridCellInfo dataGridCellInfo = dataGridCellInfoList[0];
        CartLine cartLine = dataGridCellInfo.Item as CartLine;
        if (dataGridCellInfo.Column.DisplayIndex == 2)
        {
          if (this.Cart.Items[cartLine.Index].Count > 1)
            --this.Cart.Items[cartLine.Index].Count;
        }
        else if (dataGridCellInfo.Column.DisplayIndex == 4)
          ++this.Cart.Items[cartLine.Index].Count;
        else if (dataGridCellInfo.Column.DisplayIndex == 6)
          this.Cart.Items.RemoveAt(cartLine.Index);
        if (this.CartLines == null)
          return;
        this.OnPropertyChanged("Line5");
        this.OnPropertyChanged("Line2");
        this.OnPropertyChanged("CartLines");
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
      }
    }

    public string Price => string.Format("{0:F2} {1}", (object) this.Cart.Sum, (object) Global.Currency);

    public List<CartLine> CartLines => this.Cart.CartLines();

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

    public string Line5
    {
      get => string.Format("{0} {1:F2} {2}", (object) this._line5, (object) this.Cart.Sum, (object) Global.Currency);
      set
      {
        this._line5 = value;
        this.OnPropertyChanged();
      }
    }

    public string InfBlock2 { get; set; }

    public string InfBlock3 { get; set; }

    public string InfBlock4 { get; set; }

    public string Version { get; set; }

    public string Line3InProcess { get; set; }
  }
}
