// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.ValueProviderAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
  public sealed class ValueProviderAttribute : Attribute
  {
    public ValueProviderAttribute([NotNull] string name) => this.Name = name;

    [NotNull]
    public string Name { get; private set; }
  }
}
