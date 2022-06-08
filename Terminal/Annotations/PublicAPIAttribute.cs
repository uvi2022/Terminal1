// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.PublicAPIAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
  public sealed class PublicAPIAttribute : Attribute
  {
    public PublicAPIAttribute()
    {
    }

    public PublicAPIAttribute([NotNull] string comment) => this.Comment = comment;

    [CanBeNull]
    public string Comment { get; private set; }
  }
}
