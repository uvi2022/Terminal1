// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.PathReferenceAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class PathReferenceAttribute : Attribute
  {
    public PathReferenceAttribute()
    {
    }

    public PathReferenceAttribute([NotNull, PathReference] string basePath) => this.BasePath = basePath;

    [CanBeNull]
    public string BasePath { get; private set; }
  }
}
