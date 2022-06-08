﻿// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.AspMvcAreaAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class AspMvcAreaAttribute : Attribute
  {
    public AspMvcAreaAttribute()
    {
    }

    public AspMvcAreaAttribute([NotNull] string anonymousProperty) => this.AnonymousProperty = anonymousProperty;

    [CanBeNull]
    public string AnonymousProperty { get; private set; }
  }
}
