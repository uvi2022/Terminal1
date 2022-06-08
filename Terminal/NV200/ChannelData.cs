// Decompiled with JetBrains decompiler
// Type: Terminal.NV200.ChannelData
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

namespace Terminal.NV200
{
  public class ChannelData
  {
    public int Value;
    public byte Channel;
    public char[] Currency;
    public int Level;
    public bool Recycling;

    public ChannelData()
    {
      this.Value = 0;
      this.Channel = (byte) 0;
      this.Currency = new char[3];
      this.Level = 0;
      this.Recycling = false;
    }
  }
}
