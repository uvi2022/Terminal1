// Decompiled with JetBrains decompiler
// Type: Terminal.Environment.EventedList`1
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Collections.Generic;

namespace Terminal.Environment
{
  public class EventedList<T> : List<T>
  {
    public event ChangedEventHandler Changed;

    protected virtual void OnChanged(EventArgs e)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, e);
    }

    public T Add(T value)
    {
      base.Add(value);
      this.OnChanged(EventArgs.Empty);
      return value;
    }

    public new void Clear()
    {
      base.Clear();
      this.OnChanged(EventArgs.Empty);
    }

    public new T this[int index]
    {
      set
      {
        base[index] = value;
        this.OnChanged(EventArgs.Empty);
      }
    }
  }
}
