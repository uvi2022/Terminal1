// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.MacroAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
  public sealed class MacroAttribute : Attribute
  {
    [CanBeNull]
    public string Expression { get; set; }

    public int Editable { get; set; }

    [CanBeNull]
    public string Target { get; set; }
  }
}
