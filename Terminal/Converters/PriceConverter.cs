// Decompiled with JetBrains decompiler
// Type: Terminal.Converters.PriceConverter
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Globalization;
using System.Windows.Data;

namespace Terminal.Converters
{
  public class PriceConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value == null ? (object) string.Empty : (object) string.Format("{0} {1}", value, (object) Global.Currency);

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) -1;
    }
  }
}
