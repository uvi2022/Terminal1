// Decompiled with JetBrains decompiler
// Type: Terminal.View.CheckWindow
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using Terminal.Classes;
using Terminal.Services;

namespace Terminal.View
{
  public class CheckWindow : Window, IComponentConnector
  {
    internal DataGrid dataGridService;
    internal Button PrintCheckServiceCopy;
    internal DataGrid dataGridProduct;
    internal Button PrintCheckProductCopy;
    internal DataGrid dataGridRefund;
    internal GroupBox PasswordPanel;
    internal PasswordBox PasswordBox;
    internal Button PasswordDelete;
    internal Button PasswordSubmit;
    internal Label StatusBox;
    private bool _contentLoaded;

    public CheckWindow() => this.InitializeComponent();

    private void DataGridService_Loaded(object sender, RoutedEventArgs e)
    {
      List<Purchase> Refund = (StorageService.Instance.Data.Purchase ?? new List<Purchase>()).Where<Purchase>((Func<Purchase, bool>) (x => x.ServiceId != 0L)).Where<Purchase>((Func<Purchase, bool>) (x => x.PaymentType == (short) 4)).ToList<Purchase>();
      this.dataGridService.ItemsSource = (IEnumerable) (StorageService.Instance.Data.Purchase ?? new List<Purchase>()).Where<Purchase>((Func<Purchase, bool>) (x => x.ServiceId != 0L)).Where<Purchase>((Func<Purchase, bool>) (x => x.PaymentType != (short) 4)).Where<Purchase>((Func<Purchase, bool>) (x => !Refund.Any<Purchase>((Func<Purchase, bool>) (r =>
      {
        Guid? refundGuid = r.RefundGuid;
        Guid guid = x.Guid;
        if (!refundGuid.HasValue)
          return false;
        return !refundGuid.HasValue || refundGuid.GetValueOrDefault() == guid;
      })))).OrderByDescending<Purchase, string>((Func<Purchase, string>) (x => x.PurchaseDatetime)).ToList<Purchase>();
      this.dataGridService.IsReadOnly = true;
    }

    private void DataGridProduct_Loaded(object sender, RoutedEventArgs e)
    {
      this.dataGridProduct.ItemsSource = (IEnumerable) (StorageService.Instance.Data.Purchase ?? new List<Purchase>()).Where<Purchase>((Func<Purchase, bool>) (x => x.ProductId != 0L)).OrderByDescending<Purchase, string>((Func<Purchase, string>) (x => x.PurchaseDatetime)).ToList<Purchase>();
      this.dataGridProduct.IsReadOnly = true;
    }

    private void DataGridRefund_Loaded(object sender, RoutedEventArgs e)
    {
      this.dataGridRefund.ItemsSource = (IEnumerable) (StorageService.Instance.Data.Purchase ?? new List<Purchase>()).Where<Purchase>((Func<Purchase, bool>) (x => x.ServiceId != 0L)).Where<Purchase>((Func<Purchase, bool>) (x => x.PaymentType == (short) 4)).OrderByDescending<Purchase, string>((Func<Purchase, string>) (x => x.PurchaseDatetime)).ToList<Purchase>();
      this.dataGridRefund.IsReadOnly = true;
    }

    private void PrintCheckServiceCopy_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        if (!(this.dataGridService.SelectedItem is Purchase selectedItem))
        {
          int num1 = (int) MessageBox.Show("Пожалуйста выберите услугу", "Не выбран платеж", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        else if (selectedItem.ServiceId == 0L)
        {
          int num2 = (int) MessageBox.Show("Не верный платеж", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        else
        {
          ListPaymentEvents listPaymentEvents = new ListPaymentEvents(selectedItem);
          PrintService.Instance.PrintCopy(new Core.Classes.Check()
          {
            Change = (double) selectedItem.Change,
            TotalAccepted = (double) selectedItem.TotalAccepted,
            IsPhoneEntered = selectedItem.Phone != 0L,
            Phone = selectedItem.Phone,
            PaymentType = selectedItem.PaymentType,
            ListPaymentEvents = listPaymentEvents,
            DateTime = new DateTime?(selectedItem.TimeStamp)
          });
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
      }
    }

    private void PrintCheckProductCopy_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        if (!(this.dataGridProduct.SelectedItem is Purchase selectedItem))
        {
          int num1 = (int) MessageBox.Show("Пожалуйста выберите товар", "Не выбран платеж", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        else if (selectedItem.ProductId == 0L)
        {
          int num2 = (int) MessageBox.Show("Не верный платеж", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        else
        {
          ListPaymentEvents listPaymentEvents = new ListPaymentEvents(selectedItem);
          PrintService.Instance.PrintCopy(new Core.Classes.Check()
          {
            Change = (double) selectedItem.Change,
            TotalAccepted = (double) selectedItem.TotalAccepted,
            IsPhoneEntered = selectedItem.Phone != 0L,
            Phone = selectedItem.Phone,
            PaymentType = selectedItem.PaymentType,
            ListPaymentEvents = listPaymentEvents,
            DateTime = new DateTime?(selectedItem.TimeStamp)
          });
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
      }
    }

    private void Button_Click(object sender, RoutedEventArgs e) => this.PasswordBox.Password += ((Button) sender).Content?.ToString();

    private void PasswordDelete_Click(object sender, RoutedEventArgs e)
    {
      if (this.PasswordBox.Password.Length <= 0)
        return;
      this.PasswordBox.Password = this.PasswordBox.Password.Substring(0, this.PasswordBox.Password.Length - 1);
    }

    private void PasswordSubmit_Click(object sender, RoutedEventArgs e)
    {
      string password = this.PasswordBox.Password;
      int terminalId = Global.TerminalId;
      string str1 = terminalId.ToString();
      terminalId = Global.TerminalId;
      string str2 = terminalId.ToString();
      string str3 = str1 + str2;
      if (password == str3)
        this.PasswordPanel.Visibility = Visibility.Hidden;
      else if (DataExchangeService.Instance.GetUsers().Count<User>((Func<User, bool>) (x =>
      {
        int? pin = x.Pin;
        int int32 = Convert.ToInt32(this.PasswordBox.Password);
        return pin.GetValueOrDefault() == int32 & pin.HasValue;
      })) > 0)
      {
        this.PasswordPanel.Visibility = Visibility.Hidden;
        User user = DataExchangeService.Instance.GetUsers().First<User>((Func<User, bool>) (x =>
        {
          int? pin = x.Pin;
          int int32 = Convert.ToInt32(this.PasswordBox.Password);
          return pin.GetValueOrDefault() == int32 & pin.HasValue;
        }));
        CheckIn checkIn = new CheckIn()
        {
          UserId = user.Id,
          TerminalCode = Global.TerminalId,
          DateTime = DateTime.UtcNow,
          Send = false
        };
        int num1 = 0;
        if (StorageService.Instance.Data.CheckIns != null)
          num1 = StorageService.Instance.Data.CheckIns.Count<CheckIn>((Func<CheckIn, bool>) (x => x.UserId == user.Id && x.DateTime.Date == DateTime.Today));
        if (num1 % 2 == 0)
        {
          int num2 = (int) MessageBox.Show("Здраствуйте, " + user.Fio + "\r\nУдачного дня\r\nУходя, не забудьте расчикиниться", "Чекин", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        else
        {
          int num3 = (int) MessageBox.Show("До свидания, " + user.Fio + "\r\nУдачного дня", "Чекин", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        StorageService.Instance.Data.Add(checkIn);
        DataExchange.SendCheckIn(checkIn);
      }
      else
        this.StatusBox.Content = (object) "Неверный пароль";
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Terminal;component/view/checkwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.dataGridService = (DataGrid) target;
          this.dataGridService.Loaded += new RoutedEventHandler(this.DataGridService_Loaded);
          break;
        case 2:
          this.PrintCheckServiceCopy = (Button) target;
          this.PrintCheckServiceCopy.Click += new RoutedEventHandler(this.PrintCheckServiceCopy_Click);
          break;
        case 3:
          this.dataGridProduct = (DataGrid) target;
          this.dataGridProduct.Loaded += new RoutedEventHandler(this.DataGridProduct_Loaded);
          break;
        case 4:
          this.PrintCheckProductCopy = (Button) target;
          this.PrintCheckProductCopy.Click += new RoutedEventHandler(this.PrintCheckProductCopy_Click);
          break;
        case 5:
          this.dataGridRefund = (DataGrid) target;
          this.dataGridRefund.Loaded += new RoutedEventHandler(this.DataGridRefund_Loaded);
          break;
        case 6:
          this.PasswordPanel = (GroupBox) target;
          break;
        case 7:
          this.PasswordBox = (PasswordBox) target;
          break;
        case 8:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 9:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 10:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 11:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 12:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 13:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 14:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 15:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 16:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 17:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
          break;
        case 18:
          this.PasswordDelete = (Button) target;
          this.PasswordDelete.Click += new RoutedEventHandler(this.PasswordDelete_Click);
          break;
        case 19:
          this.PasswordSubmit = (Button) target;
          this.PasswordSubmit.Click += new RoutedEventHandler(this.PasswordSubmit_Click);
          break;
        case 20:
          this.StatusBox = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
