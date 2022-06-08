// Decompiled with JetBrains decompiler
// Type: Terminal.Converters.PaymentTypeConverter
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Globalization;
using System.Windows.Data;

namespace Terminal.Converters
{
  public class PaymentTypeConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      switch ((short) value)
      {
        case 1:
          return (object) "нал";
        case 2:
          return (object) "бонусы";
        case 3:
          return (object) "безнал";
        case 4:
          return (object) "возврат";
        default:
          return (object) null;
      }
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
