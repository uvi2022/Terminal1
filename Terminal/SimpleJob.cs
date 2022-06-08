// Decompiled with JetBrains decompiler
// Type: Terminal.SimpleJob
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Quartz;
using System.Threading.Tasks;
using Terminal.Services;

namespace Terminal
{
  public class SimpleJob : IJob
  {
    public async Task Execute(IJobExecutionContext context)
    {
      Logger.Log.Info((object) "Выполняем свекрку итогов");
      Logger.Log.Info((object) ("Сверка итогов выполнена с кодом: " + CardService.Instance.CloseDay().ToString()));
      Logger.Log.Info((object) "Рестартуем купюроприемник");
      LoggerNV200.Log.Info((object) "Рестартуем купюроприемник");
      PayoutService.Instance.Reset();
    }
  }
}
