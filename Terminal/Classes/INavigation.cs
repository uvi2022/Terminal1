// Decompiled with JetBrains decompiler
// Type: Terminal.Classes.INavigation
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System.ServiceModel;
using System.Windows.Input;

namespace Terminal.Classes
{
  [ServiceContract]
  public interface INavigation
  {
    [OperationContract]
    ICommand GotoMenuCommand();

    ICommand GotoNextPageCommand();

    ICommand GotoPrevousePageCommand();
  }
}
