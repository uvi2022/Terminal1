// Decompiled with JetBrains decompiler
// Type: Terminal.View.PaymentResult
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
using Terminal.ViewModels;

namespace Terminal.View
{
  public class PaymentResult : Window, IComponentConnector
  {
    internal MediaElement MediaElement;
    private bool _contentLoaded;

    public PaymentResult()
    {
      this.InitializeComponent();
      if (this.DataContext is PaymentResultViewModel)
        ;
    }

    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e) => App.OnUserActivity();

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Terminal;component/view/paymentresult.xaml", UriKind.Relative));
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
          break;
        case 2:
          this.MediaElement = (MediaElement) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
