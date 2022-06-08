// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.ViewModelBase
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.ComponentModel;
using System.Windows.Input;
using Terminal.Annotations;
using Terminal.Classes;
using Terminal.Services;

namespace Terminal.ViewModels
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged(string propertyName = null)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand CloseCommand => (ICommand) new Command(new Action<object>(this.Close), true);

    public void Close(object param) => WindowService.Close(param);

    public virtual ICommand GotoMenuCommand { get; set; }

    public virtual ICommand GotoNextPageCommand { get; set; }

    public virtual ICommand GotoPreviousPageCommand { get; set; }
  }
}
