// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.AspChildControlTypeAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public sealed class AspChildControlTypeAttribute : Attribute
  {
    public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
    {
      this.TagName = tagName;
      this.ControlType = controlType;
    }

    [NotNull]
    public string TagName { get; private set; }

    [NotNull]
    public Type ControlType { get; private set; }
  }
}
