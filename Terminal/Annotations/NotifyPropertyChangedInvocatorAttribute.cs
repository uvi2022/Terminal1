// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.NotifyPropertyChangedInvocatorAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
  {
    public NotifyPropertyChangedInvocatorAttribute()
    {
    }

    public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName) => this.ParameterName = parameterName;

    [CanBeNull]
    public string ParameterName { get; private set; }
  }
}
