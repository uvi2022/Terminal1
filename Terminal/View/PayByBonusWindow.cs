// Decompiled with JetBrains decompiler
// Type: Terminal.View.PayByBonusWindow
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
  public class PayByBonusWindow : Window, IComponentConnector
  {
    internal Grid MainGrid;
    private bool _contentLoaded;

    public PayByBonusWindow() => this.InitializeComponent();

    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e) => App.OnUserActivity();

    private void OnPayByCashLoaded(object sender, RoutedEventArgs e) => App.TimerStop();

    private void OnPayByCashClosed(object sender, EventArgs e) => App.TimerStart();

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Terminal;component/view/paybybonuswindow.xaml", UriKind.Relative));
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
          ((FrameworkElement) target).Loaded += new RoutedEventHandler(this.OnPayByCashLoaded);
          ((Window) target).Closed += new EventHandler(this.OnPayByCashClosed);
          break;
        case 2:
          this.MainGrid = (Grid) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
