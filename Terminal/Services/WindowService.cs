// Decompiled with JetBrains decompiler
// Type: Terminal.Services.WindowService
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Windows;
using Terminal.ViewModels;

namespace Terminal.Services
{
  public class WindowService
  {
    public static void ShowWindow<T>(object dataContext) where T : Window, new()
    {
      WindowService.GetWindowCollection();
      Application.Current.Dispatcher.Invoke((Action) (() =>
      {
        new T() { DataContext = dataContext }.Show();
      }));
    }

    public static void Close(object dataContext)
    {
      foreach (Window window1 in WindowService.GetWindowCollection())
      {
        Window window = window1;
        Application.Current.Dispatcher.Invoke((Action) (() =>
        {
          if (window.DataContext is MainWindowViewModel dataContext2)
          {
            dataContext2.ShowHairLines = false;
            if (dataContext2.Brand != 0)
            {
              dataContext2.Page = 0;
              dataContext2.Brand = 0;
            }
            else if (dataContext2.ProductParent != 0)
            {
              dataContext2.Page = 0;
              dataContext2.Brand = 0;
              dataContext2.ProductParent = 0;
            }
            else if (dataContext2.Shop)
            {
              dataContext2.Page = 0;
              dataContext2.Brand = 0;
              dataContext2.ProductParent = 0;
              dataContext2.Shop = false;
            }
            else if (dataContext2.Parent != 0)
            {
              dataContext2.Page = 0;
              dataContext2.Parent = 0;
            }
            else
            {
              dataContext2.Page = 0;
              dataContext2.Parent = 0;
              dataContext2.Brand = 0;
              dataContext2.ProductParent = 0;
              dataContext2.Shop = false;
            }
            dataContext2.ShowHairLines = false;
          }
          else
          {
            if (window.DataContext != dataContext)
              return;
            window.Close();
          }
        }));
      }
    }

    public static void CloseAllExceptOne(object dataContext)
    {
      foreach (Window window1 in WindowService.GetWindowCollection())
      {
        Window window = window1;
        Application.Current.Dispatcher.Invoke((Action) (() =>
        {
          if (window.DataContext != null && window.DataContext != dataContext)
            window.Close();
          if (!(window.DataContext is MainWindowViewModel dataContext2))
            return;
          dataContext2.ShowHairLines = false;
          dataContext2.Page = 0;
          dataContext2.Parent = 0;
          dataContext2.Brand = 0;
          dataContext2.ProductParent = 0;
          dataContext2.Shop = false;
          dataContext2.ShowHairLines = false;
        }));
      }
    }

    private static WindowCollection GetWindowCollection()
    {
      WindowCollection windowCollection = new WindowCollection();
      Application.Current.Dispatcher.Invoke((Action) (() => windowCollection = Application.Current.Windows));
      return windowCollection;
    }
  }
}
