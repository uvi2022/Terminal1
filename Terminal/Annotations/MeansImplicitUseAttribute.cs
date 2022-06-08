// Decompiled with JetBrains decompiler
// Type: Terminal.Annotations.MeansImplicitUseAttribute
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;

namespace Terminal.Annotations
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter)]
  public sealed class MeansImplicitUseAttribute : Attribute
  {
    public MeansImplicitUseAttribute()
      : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
    {
    }

    public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
      : this(useKindFlags, ImplicitUseTargetFlags.Default)
    {
    }

    public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
      : this(ImplicitUseKindFlags.Default, targetFlags)
    {
    }

    public MeansImplicitUseAttribute(
      ImplicitUseKindFlags useKindFlags,
      ImplicitUseTargetFlags targetFlags)
    {
      this.UseKindFlags = useKindFlags;
      this.TargetFlags = targetFlags;
    }

    [UsedImplicitly]
    public ImplicitUseKindFlags UseKindFlags { get; private set; }

    [UsedImplicitly]
    public ImplicitUseTargetFlags TargetFlags { get; private set; }
  }
}
