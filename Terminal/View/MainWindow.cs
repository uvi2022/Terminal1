// Decompiled with JetBrains decompiler
// Type: Terminal.View.MainWindow
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
using System.IO;  // 05.06.22

namespace Terminal.View
{
  public class MainWindow : Window, IComponentConnector
  {
    private const double RequiredWidth = 1920.0;
    private const double RequiredHeight = 1080.0;
    internal Grid Hair;
    internal ListBox Lb;
    internal Button Version;
    internal ListBox horizontalListBox;
    private bool _contentLoaded;

    public MainWindow() => this.InitializeComponent();

    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e) => App.OnUserActivity();

    public double ScaleHeight => this.ActualHeight / 1080.0;

    public double ScaleWidth => this.ActualWidth / 1920.0;

    private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key != Key.M || Keyboard.Modifiers != ModifierKeys.Control)
        return;
      ServiceWindow serviceWindow = new ServiceWindow();
      serviceWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      serviceWindow.ShowDialog();
    }

    private void Version_Click(object sender, RoutedEventArgs e)
    {
      CheckWindow checkWindow = new CheckWindow();
      checkWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      checkWindow.ShowDialog();
    }

    private void HorizontalListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      ListBoxItem selectedItem = (sender as ListBox).SelectedItem as ListBoxItem;
      (sender as ListBox).ScrollIntoView((object) selectedItem);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;

	  Application.LoadComponent((object) this, new Uri("/Terminal;component/view/mainwindow.xaml", UriKind.Relative)); 

// 05.06.22
// ?? Application.LoadComponent((object) this, new Uri("/Terminal;component/Resources/mainwindow.xaml", UriKind.Relative));
// см. https://stackoverflow.com/questions/710329/load-external-xaml-file-using-loadcomponent
// ?? System.Xml.XmlReader XmlRead = System.Xml.XmlReader.Create("view/mainwindow.xaml");
// ?? Application.Current.Resources = (ResourceDictionary)XamlReader.Load(XmlRead);  // ОШИБКА !?
// ?? XmlRead.Close();
// см. https://professorweb.ru/my/WPF/base_WPF/level2/2_10.php
// ?? DependencyObject rootElement;
// ?? using (FileStream fs = new FileStream("view/mainwindow.xaml", FileMode.Open))
// ?? { rootElement = (DependencyObject)XamlReader.Load(fs); }
// ?? this.Content = rootElement;
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
          ((UIElement) target).KeyDown += new KeyEventHandler(this.MainWindow_OnKeyDown);
          break;
        case 2:
          this.Hair = (Grid) target;
          break;
        case 3:
          this.Lb = (ListBox) target;
          break;
        case 4:
          this.Version = (Button) target;
          this.Version.Click += new RoutedEventHandler(this.Version_Click);
          break;
        case 5:
          this.horizontalListBox = (ListBox) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
