// Decompiled with JetBrains decompiler
// Type: Terminal.View.ServiceWindow
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using log4net;
using log4net.Appender;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using Terminal.Services;

namespace Terminal.View
{
  public class ServiceWindow : Window, IComponentConnector
  {
    private int _sumOut = 0;
    private int _ostatok = 0;
    private List<Purchase> _services = new List<Purchase>();
    private List<ChannelLevelInfo> _level;
    private List<ChannelLevelInfo> _stacked;
    internal TabItem TabItem_NV200;
    internal TextBox NV200Log;
    internal TextBox PayoutInfo;
    private bool _contentLoaded;

    public ServiceWindow() => this.InitializeComponent();

    private void OnZReportClick(object sender, RoutedEventArgs e) => PrintService.Instance.PrintZFReports();

    private void OnIncasClick(object sender, RoutedEventArgs e)
    {
      Encashment incas = new Encashment()
      {
        Guid = Guid.NewGuid(),
        TerminalId = Global.TerminalId,
        Date = DateTime.UtcNow,
        SumOut = this._sumOut,
        Ostatok = this._ostatok,
        Encashed = this._services.Select<Purchase, Guid>((Func<Purchase, Guid>) (x => x.Guid)).ToList<Guid>(),
        ChannelLevelInfo = this._level,
        CashboxInfo = this._stacked
      };
      try
      {
        PaymentService.Instance.SaveEncashment(incas);
        PaymentService.Instance.SendEncashment(incas);
        StorageService.Instance.Data.ClearStacked();
      }
      catch (Exception ex)
      {
      }
      PrintService.Instance.PrintIncas(this.PayoutInfo.Text.Split('\n'));
      int num = (int) MessageBox.Show("Дождитесь печати чека и извлеките купюры из ящика купюроприемника", "Инкассация", MessageBoxButton.OK, MessageBoxImage.Asterisk);
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e) => this.ShowInfo();

    private void ShowInfo()
    {
      Encashment previousEncashment = ServiceWindow.GetPreviousEncashment();
      List<Tuple<int, int>> channelLevelInfo1 = PayoutService.Instance.GetChannelLevelInfo();
      this._level = PayoutService.Instance.GetChannelLevelInfo2();
      this.PayoutInfo.Text = "Банкноты на ленте: \r\n";
      foreach (Tuple<int, int> tuple in channelLevelInfo1)
      {
        TextBox payoutInfo = this.PayoutInfo;
        payoutInfo.Text = payoutInfo.Text + tuple.Item1.ToString() + " руб. " + tuple.Item2.ToString() + " шт\r\n";
      }
      int num1 = channelLevelInfo1.Sum<Tuple<int, int>>((Func<Tuple<int, int>, int>) (x => x.Item2 * x.Item1));
      TextBox payoutInfo1 = this.PayoutInfo;
      payoutInfo1.Text = payoutInfo1.Text + "Сумма: " + num1.ToString() + " руб. \r\n";
      this.PayoutInfo.Text += string.Format("Последняя инкассация: {0} \r\n", (object) previousEncashment.Date);
      int ostatok = previousEncashment.Ostatok;
      this.PayoutInfo.Text += string.Format("Остаток на начало работы: {0} руб.\r\n", (object) ostatok);
      this._services = PaymentService.Instance.GetPaidServicesForIncas();
      int num2 = this._services.Sum<Purchase>((Func<Purchase, int>) (x => x.Price));
      this.PayoutInfo.Text += string.Format("Сумма по услугам: {0} руб. \r\n", (object) num2);
      this._stacked = StorageService.Instance.Data.Stacked;
      if (this._stacked == null)
      {
        TextBox payoutInfo2 = this.PayoutInfo;
        payoutInfo2.Text = payoutInfo2.Text + "Доступно для инкассации: " + (num2 - num1 + ostatok).ToString() + " руб. \r\n";
        this._ostatok = num1;
        this._sumOut = num2 - num1 + ostatok;
      }
      else
      {
        this.PayoutInfo.Text += "Банкноты в ящике:\r\n";
        foreach (ChannelLevelInfo channelLevelInfo2 in this._stacked)
        {
          TextBox payoutInfo3 = this.PayoutInfo;
          payoutInfo3.Text = payoutInfo3.Text + channelLevelInfo2.Note.ToString() + " руб. " + channelLevelInfo2.Count.ToString() + " шт\r\n";
          this._sumOut += channelLevelInfo2.Note * (int) channelLevelInfo2.Count;
        }
        this._ostatok = num1;
        TextBox payoutInfo4 = this.PayoutInfo;
        payoutInfo4.Text = payoutInfo4.Text + "Доступно для инкассации: " + this._sumOut.ToString() + " руб. \r\n";
      }
    }

    private void ShowNV200Log()
    {
      this.NV200Log.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
      FileAppender fileAppender = LogManager.GetRepository().GetAppenders().OfType<FileAppender>().FirstOrDefault<FileAppender>((Func<FileAppender, bool>) (fa => fa.Name == "LogFileAppenderNV200"));
      if (fileAppender == null)
        return;
      using (StreamReader streamReader = new StreamReader(fileAppender.File, Encoding.Default))
      {
        this.NV200Log.Text = streamReader.ReadToEnd();
        streamReader.Close();
      }
    }

    private async void ShowNV200LogAuto()
    {
      this.NV200Log.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
      FileAppender rootAppender = LogManager.GetRepository().GetAppenders().OfType<FileAppender>().FirstOrDefault<FileAppender>((Func<FileAppender, bool>) (fa => fa.Name == "LogFileAppenderNV200"));
      if (rootAppender != null)
      {
        AutoResetEvent wh = new AutoResetEvent(false);
        FileSystemWatcher fsw = new FileSystemWatcher(Path.GetDirectoryName(rootAppender.File))
        {
          Filter = rootAppender.File,
          EnableRaisingEvents = true
        };
        fsw.Changed += (FileSystemEventHandler) ((s, e) => wh.Set());
        FileStream fs = new FileStream(rootAppender.File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using (StreamReader sr = new StreamReader((Stream) fs))
        {
          string s = "";
          while (true)
          {
            s = await sr.ReadLineAsync();
            if (s != null)
              this.NV200Log.Text = s;
            else
              wh.WaitOne(10000);
          }
        }
      }
      else
        rootAppender = (FileAppender) null;
    }

    public void MonitorTailOfFile(string filePath)
    {
      long offset = new FileInfo(filePath).Length - 1024L;
      if (offset < 0L)
        offset = 0L;
      while (true)
      {
        try
        {
          if (new FileInfo(filePath).Length > offset)
          {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
              fileStream.Seek(offset, SeekOrigin.Begin);
              byte[] numArray = new byte[1024];
              while (true)
              {
                int count = fileStream.Read(numArray, 0, numArray.Length);
                offset += (long) count;
                if (count != 0)
                  this.NV200Log.Text = Encoding.ASCII.GetString(numArray, 0, count);
                else
                  break;
              }
            }
          }
        }
        catch
        {
        }
        Thread.Sleep(1000);
      }
    }

    private static Encashment GetPreviousEncashment() => PaymentService.Instance.GetPreviousEncashment();

    private void OnPrintClick(object sender, RoutedEventArgs e) => PrintService.Instance.PrintIncas(this.PayoutInfo.Text.Split('\n'));

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e) => PayoutService.Instance.EmptyAll();

    private void OnBankClick(object sender, RoutedEventArgs e) => CardService.Instance.CloseDay();

    private void OnPayoutResetClick(object sender, RoutedEventArgs e)
    {
      Logger.Log.Info((object) "Рестартуем купюроприемник");
      LoggerNV200.Log.Info((object) "Рестартуем купюроприемник");
      PayoutService.Instance.Reset();
    }

    private void TabItem_NV200_Selected(object sender, RoutedEventArgs e)
    {
      if (!(sender is TabItem))
        return;
      this.ShowNV200Log();
      this.NV200Log.Focus();
      this.NV200Log.ScrollToEnd();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Terminal;component/view/servicewindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((FrameworkElement) target).Loaded += new RoutedEventHandler(this.OnWindowLoaded);
          break;
        case 2:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.OnBankClick);
          break;
        case 3:
          this.TabItem_NV200 = (TabItem) target;
          this.TabItem_NV200.AddHandler(Selector.SelectedEvent, (Delegate) new RoutedEventHandler(this.TabItem_NV200_Selected));
          break;
        case 4:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.OnPayoutResetClick);
          break;
        case 5:
          this.NV200Log = (TextBox) target;
          break;
        case 6:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.OnPrintClick);
          break;
        case 7:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.OnIncasClick);
          break;
        case 8:
          this.PayoutInfo = (TextBox) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
