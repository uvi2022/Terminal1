// Decompiled with JetBrains decompiler
// Type: Terminal.View.PaymentWindow
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Terminal.View
{
  public class PaymentWindow : Window, IComponentConnector
  {
    internal MediaElement MediaElement;
    private bool _contentLoaded;

    public PaymentWindow() => this.InitializeComponent();

    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e) => App.OnUserActivity();

    private void OnButtonClick(object sender, RoutedEventArgs e) => this.MediaElement.Stop();

    private void PaymentWindow_OnLoaded(object sender, RoutedEventArgs e) => this.MediaElement.Play();

    private void PaymentWindow_OnClosing(object sender, CancelEventArgs e) => this.MediaElement.Stop();

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Terminal;component/view/paymentwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.OnPreviewMouseDown);
          ((FrameworkElement) target).Loaded += new RoutedEventHandler(this.PaymentWindow_OnLoaded);
          ((Window) target).Closing += new CancelEventHandler(this.PaymentWindow_OnClosing);
          break;
        case 2:
          this.MediaElement = (MediaElement) target;
          break;
        case 3:
          ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.OnButtonClick);
          break;
        case 4:
          ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.OnButtonClick);
          break;
        case 5:
          ((UIElement) target).PreviewMouseDown += new MouseButtonEventHandler(this.OnButtonClick);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
