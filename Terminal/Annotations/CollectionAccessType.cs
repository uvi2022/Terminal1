// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.CollectionAccessType
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [Flags]
  public enum CollectionAccessType
  {
    None = 0,
    Read = 1,
    ModifyExistingContent = 2,
    UpdatedContent = 6,
  }
}
