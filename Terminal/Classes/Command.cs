// Decompiled with JetBrains decompiler
// Type: Terminal.Classes.Command
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Windows.Input;

namespace Terminal.Classes
{
  public class Command : ICommand
  {
    private readonly Action<object> _action;
    private readonly bool _canExecute;

    public Command(Action<object> action, bool canExecute)
    {
      this._action = action;
      this._canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => this._canExecute;

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter) => this._action(parameter);
  }
}
