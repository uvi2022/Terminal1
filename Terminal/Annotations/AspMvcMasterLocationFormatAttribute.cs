﻿// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.AspMvcMasterLocationFormatAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  public sealed class AspMvcMasterLocationFormatAttribute : Attribute
  {
    public AspMvcMasterLocationFormatAttribute([NotNull] string format) => this.Format = format;

    [NotNull]
    public string Format { get; private set; }
  }
}
