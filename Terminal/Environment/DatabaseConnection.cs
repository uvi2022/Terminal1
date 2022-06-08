// Decompiled with JetBrains decompiler
// Type: Terminal.Environment.DatabaseConnection
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.IO;

namespace Terminal.Environment
{
  public class DatabaseConnection : EquipmentBase
  {
    public override EquipmentStatus CheckStatus()
    {
      string[] strArray = new string[4];
      try
      {
        using (StreamReader streamReader = File.OpenText("Environment/statuses.txt"))
        {
          int index = 0;
          while (!streamReader.EndOfStream)
          {
            strArray[index] = streamReader.ReadLine();
            ++index;
          }
        }
      }
      catch (Exception ex)
      {
      }
      switch (strArray[1])
      {
        case "0":
          return EquipmentStatus.Ok;
        case "1":
          return EquipmentStatus.Error;
        default:
          return EquipmentStatus.Unknown;
      }
    }
  }
}
