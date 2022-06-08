// Decompiled with JetBrains decompiler
// Type: Terminal.Services.StorageService
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace Terminal.Services
{
  public sealed class StorageService
  {
    private static readonly System.Lazy<StorageService> Lazy = new System.Lazy<StorageService>((Func<StorageService>) (() => new StorageService()));
    private static readonly Mutex Mutex = new Mutex();

    public static StorageService Instance => StorageService.Lazy.Value;

    public StorageData Data { get; set; }

    public void Init()
    {
    }

    private StorageService() => this.Load();

    private void Load()
    {
      try
      {
        string storageFilename = Global.StorageFilename;
        if (File.Exists(storageFilename))
          this.Data = JsonConvert.DeserializeObject<StorageData>(File.ReadAllText(storageFilename));
        else
          this.Data = new StorageData();
      }
      catch (Exception ex)
      {
      }
    }

    public bool Save()
    {
      try
      {
        StorageService.Mutex.WaitOne();
        string storageFilename = Global.StorageFilename;
        string str = Global.StorageFilename + "t" + DateTime.Now.ToString("ddMMyyyy");
        using (StreamWriter text = File.CreateText(str))
          new JsonSerializer().Serialize((TextWriter) text, (object) this.Data);
        File.Delete(storageFilename);
        File.Move(str, storageFilename);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
      finally
      {
        StorageService.Mutex.ReleaseMutex();
      }
    }
  }
}
