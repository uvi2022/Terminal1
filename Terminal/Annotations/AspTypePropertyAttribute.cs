﻿// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.AspTypePropertyAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class AspTypePropertyAttribute : Attribute
  {
    public bool CreateConstructorReferences { get; private set; }

    public AspTypePropertyAttribute(bool createConstructorReferences) => this.CreateConstructorReferences = createConstructorReferences;
  }
}