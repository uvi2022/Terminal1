// Decompiled with JetBrains decompiler
// Type: Terminal.Converters.NameConverter
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Globalization;
using System.Windows.Data;

namespace Terminal.Converters
{
  public class NameConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string str = value.ToString();
      int length = str.IndexOf("(");
      return length == -1 ? (object) str : (object) str.Substring(0, length);
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
