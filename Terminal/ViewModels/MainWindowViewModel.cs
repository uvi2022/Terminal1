// Decompiled with JetBrains decompiler
// Type: Terminal.ViewModels.MainWindowViewModel
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Terminal.Environment;

namespace Terminal.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private int _pageLimit = 12;
    private readonly int _pageProductLimit = 3;
    private List<ServiceParentItem> _buttonsServiceParents;
    private List<ServiceItem> _buttonsServices;
    private List<ProductItem> _buttonsProducts;
    private bool _showHairLines;
    private string _statusBar1;
    private string _statusBar2;
    private string _statusBar3;
    private string _statusBar4;
    private string _welcomeStatusBar;
    private string _infBlock1;
    private string _infBlock2;
    private string _infBlock3;
    private string _infBlock4;
    private Visibility _errorPanelStatus;
    private Visibility _welcomePanelStatus;
    private int _page;
    private int _parent;
    private bool _shop;
    private int _productParent;
    private int _brand;
    private Cart _cart;

    public MainWindowViewModel()
    {
      this.StatusPanelColor = (Brush) new BrushConverter().ConvertFromString("White");
      this.ShowMenuButton = false;
      this.ShowHairLines = false;
    }

    public bool ShowMenuButton { get; private set; }

    public bool ShowBackButton { get; private set; }

    public bool ShowHairLines
    {
      get => this._showHairLines;
      set
      {
        this._showHairLines = value;
        this.OnPropertyChanged(nameof (ShowHairLines));
      }
    }

    public bool ShowCart => Global.ShowCart && this.Cart != null && !(this.Page == 0 & this.Parent == 0 & this.ProductParent == 0 & this.Brand == 0 & !this.Shop & this.Cart.Count == 0);

    public List<ButtonItem> Buttons
    {
      get
      {
        List<ButtonItem> buttons = new List<ButtonItem>();
        if (this.Page == 0 & this.Parent == 0 & this.ProductParent == 0 & this.Brand == 0 & !this.Shop)
        {
          foreach (ServiceParentItem buttonsServiceParent in this.ButtonsServiceParents)
            buttons.Add(new ButtonItem()
            {
              Id = buttonsServiceParent.Id,
              Name = buttonsServiceParent.Name,
              Weight = 310,
              Height = 140,
              Image = buttonsServiceParent.Image,
              Type = (byte) 1
            });
          if (this._buttonsProducts != null && this._buttonsProducts.Count<ProductItem>() > 0)
            buttons.Add(new ButtonItem()
            {
              Id = 0L,
              Name = "Витрина",
              Weight = 310,
              Height = 140,
              Image = "/Images/shop.png",
              Type = (byte) 5
            });
          this.ShowHairLines = false;
          this.ShowMenuButton = false;
          this.ShowBackButton = false;
        }
        else if (this.Page == 0 & this.Parent == 0 & this.ProductParent == 0 & this.Brand == 0 & this.Shop)
        {
          foreach (ProductItem productItem in (this._buttonsProducts ?? new List<ProductItem>()).Where<ProductItem>((Func<ProductItem, bool>) (x =>
          {
            int? productParent = x.ProductParent;
            int num = 0;
            return !(productParent.GetValueOrDefault() == num & productParent.HasValue);
          })).GroupBy<ProductItem, int?>((Func<ProductItem, int?>) (x => x.ProductParent)).Select<IGrouping<int?, ProductItem>, ProductItem>((Func<IGrouping<int?, ProductItem>, ProductItem>) (y => y.First<ProductItem>())))
            buttons.Add(new ButtonItem()
            {
              Id = (long) productItem.ProductParent.Value,
              Name = productItem.ProductParentName,
              Weight = productItem.Weight,
              Height = productItem.Height,
              Type = (byte) 3
            });
          this.ShowHairLines = false;
          this.ShowMenuButton = true;
          this.ShowBackButton = false;
        }
        else if (this.Page == 0 & this.Parent == 0 & this.ProductParent != 0 & this.Brand == 0 & this.Shop)
        {
          foreach (ProductItem productItem in (this._buttonsProducts ?? new List<ProductItem>()).Where<ProductItem>((Func<ProductItem, bool>) (x =>
          {
            int? productParent1 = x.ProductParent;
            int productParent2 = this.ProductParent;
            return productParent1.GetValueOrDefault() == productParent2 & productParent1.HasValue;
          })).GroupBy<ProductItem, int?>((Func<ProductItem, int?>) (x => x.Brand)).Select<IGrouping<int?, ProductItem>, ProductItem>((Func<IGrouping<int?, ProductItem>, ProductItem>) (y => y.First<ProductItem>())))
            buttons.Add(new ButtonItem()
            {
              Id = (long) productItem.Brand.Value,
              Name = productItem.BrandName,
              Weight = productItem.Weight,
              Height = productItem.Height,
              Type = (byte) 4
            });
          this.ShowHairLines = false;
          this.ShowMenuButton = true;
          this.ShowBackButton = true;
        }
        else if (this.ProductParent != 0 & this.Brand != 0)
        {
          foreach (ProductItem productItem in (this._buttonsProducts ?? new List<ProductItem>()).Where<ProductItem>((Func<ProductItem, bool>) (x =>
          {
            int? nullable = x.ProductParent;
            int productParent = this.ProductParent;
            int num1 = nullable.GetValueOrDefault() == productParent & nullable.HasValue ? 1 : 0;
            nullable = x.Brand;
            int brand = this.Brand;
            int num2 = nullable.GetValueOrDefault() == brand & nullable.HasValue ? 1 : 0;
            return (num1 & num2) != 0;
          })).OrderBy<ProductItem, int>((Func<ProductItem, int>) (y => y.Page)).ThenBy<ProductItem, int>((Func<ProductItem, int>) (y => y.DisplayOrder)).Skip<ProductItem>(this._pageProductLimit * this.Page).Take<ProductItem>(this._pageProductLimit).ToArray<ProductItem>())
            buttons.Add(new ButtonItem()
            {
              Id = productItem.Id,
              Name = productItem.Name,
              Weight = productItem.Weight,
              Height = productItem.Height + 350,
              Price = new int?(productItem.Price),
              Image = productItem.Image,
              Bonus = productItem.Bonus,
              Type = (byte) 2
            });
          this.ShowHairLines = false;
          this.ShowMenuButton = true;
          this.ShowBackButton = true;
        }
        else if (this.Parent != 0)
        {
          int? pageLimit = this.ButtonsServiceParents.Where<ServiceParentItem>((Func<ServiceParentItem, bool>) (x => x.Id == (long) this.Parent)).FirstOrDefault<ServiceParentItem>().PageLimit;
          if (pageLimit.HasValue)
            this._pageLimit = pageLimit.Value;
          foreach (ServiceItem serviceItem in (this._buttonsServices ?? new List<ServiceItem>()).Where<ServiceItem>((Func<ServiceItem, bool>) (x =>
          {
            int? parent1 = x.Parent;
            int parent2 = this.Parent;
            return parent1.GetValueOrDefault() == parent2 & parent1.HasValue;
          })).OrderBy<ServiceItem, int>((Func<ServiceItem, int>) (y => y.Page)).ThenBy<ServiceItem, int>((Func<ServiceItem, int>) (y => y.DisplayOrder)).Skip<ServiceItem>(this._pageLimit * this.Page).Take<ServiceItem>(this._pageLimit).ToArray<ServiceItem>())
            buttons.Add(new ButtonItem()
            {
              Id = serviceItem.Id,
              Name = serviceItem.Name,
              Weight = this._pageLimit == 16 ? serviceItem.Weight - 40 : serviceItem.Weight,
              Height = serviceItem.Height,
              Price = new int?(serviceItem.Price),
              Bonus = serviceItem.Bonus,
              Promo = serviceItem.Promo,
              Type = (byte) 0
            });
          this.ShowHairLines = this._pageLimit == 16;
          this.ShowMenuButton = true;
          this.ShowBackButton = false;
        }
        return buttons;
      }
    }

    public List<ButtonItem> ButtonsSecond
    {
      get
      {
        List<ButtonItem> buttonsSecond = new List<ButtonItem>();
        foreach (ServiceParentItem buttonsServiceParent in this.ButtonsServiceParents)
          buttonsSecond.Add(new ButtonItem()
          {
            Id = buttonsServiceParent.Id,
            Name = buttonsServiceParent.Name,
            Weight = 300,
            Height = 160,
            Image = buttonsServiceParent.Image,
            ImageInactive = buttonsServiceParent.ImageInactive,
            IsActive = buttonsServiceParent.Id == (long) this.Parent,
            Type = (byte) 1
          });
        if (this._buttonsProducts != null && this._buttonsProducts.Count<ProductItem>() > 0)
          buttonsSecond.Add(new ButtonItem()
          {
            Id = 0L,
            Name = "Витрина",
            Weight = 300,
            Height = 160,
            Image = "/Images/shop.png",
            ImageInactive = "/Images/shop_1.png",
            IsActive = this.Shop,
            Type = (byte) 5
          });
        return buttonsSecond;
      }
    }

    public List<ServiceParentItem> ButtonsServiceParents
    {
      get => this._buttonsServiceParents ?? new List<ServiceParentItem>();
      set
      {
        this._buttonsServiceParents = value;
        this.OnPropertyChanged();
        this.OnPropertyChanged("ButtonsSecond");
        this.OnPropertyChanged("PreviousPageEnabled");
        this.OnPropertyChanged("NextPageEnabled");
      }
    }

    public List<ServiceItem> ButtonsServices
    {
      get => (this._buttonsServices ?? new List<ServiceItem>()).Where<ServiceItem>((Func<ServiceItem, bool>) (x => x.Page == this.Page)).OrderBy<ServiceItem, int>((Func<ServiceItem, int>) (y => y.DisplayOrder)).ToList<ServiceItem>();
      set
      {
        this._buttonsServices = value;
        this.OnPropertyChanged();
        this.OnPropertyChanged("ButtonsSecond");
        this.OnPropertyChanged("PreviousPageEnabled");
        this.OnPropertyChanged("NextPageEnabled");
      }
    }

    public List<ProductItem> ButtonsProducts
    {
      get => (this._buttonsProducts ?? new List<ProductItem>()).Where<ProductItem>((Func<ProductItem, bool>) (x => x.Page == this.Page)).OrderBy<ProductItem, int>((Func<ProductItem, int>) (y => y.DisplayOrder)).ToList<ProductItem>();
      set
      {
        this._buttonsProducts = value;
        this.OnPropertyChanged();
        this.OnPropertyChanged("ButtonsSecond");
        this.OnPropertyChanged("PreviousPageEnabled");
        this.OnPropertyChanged("NextPageEnabled");
      }
    }

    public string StatusBar1
    {
      get => this._statusBar1;
      set
      {
        this._statusBar1 = value;
        this.OnPropertyChanged();
      }
    }

    public string StatusBar2
    {
      get => this._statusBar2;
      set
      {
        this._statusBar2 = value;
        this.OnPropertyChanged();
      }
    }

    public string StatusBar3
    {
      get => this._statusBar3;
      set
      {
        this._statusBar3 = value;
        this.OnPropertyChanged();
      }
    }

    public string StatusBar4
    {
      get => this._statusBar4;
      set
      {
        this._statusBar4 = value;
        this.OnPropertyChanged();
      }
    }

    public string WelcomeStatusBar
    {
      get => this._welcomeStatusBar;
      set
      {
        this._welcomeStatusBar = value;
        this.OnPropertyChanged();
      }
    }

    public string InfBlock1
    {
      get => this._infBlock1;
      set
      {
        this._infBlock1 = value;
        this.OnPropertyChanged();
      }
    }

    public string InfBlock2
    {
      get => this._infBlock2;
      set
      {
        this._infBlock2 = value;
        this.OnPropertyChanged();
      }
    }

    public string InfBlock3
    {
      get => this._infBlock3;
      set
      {
        this._infBlock3 = value;
        this.OnPropertyChanged();
      }
    }

    public string InfBlock4
    {
      get => this._infBlock4;
      set
      {
        this._infBlock4 = value;
        this.OnPropertyChanged();
      }
    }

    public string Version { get; set; }

    public Visibility ErrorPanelStatus
    {
      get => this._errorPanelStatus;
      set
      {
        this._errorPanelStatus = value;
        this.OnPropertyChanged();
      }
    }

    public Visibility WelcomePanelStatus
    {
      get => this._welcomePanelStatus;
      set
      {
        this._welcomePanelStatus = value;
        this.OnPropertyChanged();
      }
    }

    public List<EquipmentStatusList> EquipmentStatus
    {
      set
      {
        List<EquipmentStatusList> list = value.Where<EquipmentStatusList>((Func<EquipmentStatusList, bool>) (x => x.EquipmentStatusId > 0)).ToList<EquipmentStatusList>();
        if (!list.Any<EquipmentStatusList>())
        {
          this.ErrorPanelStatus = Visibility.Visible;
          this.WelcomeStatusBar = "Добро пожаловать!";
          this.StatusBar1 = string.Empty;
          this.StatusBar2 = string.Empty;
          this.StatusBar3 = string.Empty;
          this.StatusBar4 = string.Empty;
          this.StatusPanelColor = (Brush) new BrushConverter().ConvertFromString("White");
        }
        else
        {
          this.ErrorPanelStatus = Visibility.Visible;
          this.WelcomeStatusBar = string.Empty;
          this.StatusBar1 = string.Empty;
          this.StatusBar2 = string.Empty;
          this.StatusBar3 = string.Empty;
          this.StatusBar4 = string.Empty;
          this.StatusPanelColor = (Brush) new BrushConverter().ConvertFromString("#EAB9B9");
        }
        foreach (EquipmentStatusList equipmentStatusList in list)
        {
          if (equipmentStatusList.EquipmentId == 0)
            this.StatusBar1 = "ИЗВИНИТЕ, ОПЛАТА НАЛИЧНЫМИ ВРЕМЕННО НЕВОЗМОЖНА";
          if (equipmentStatusList.EquipmentId == 1)
            this.StatusBar2 = "ИЗВИНИТЕ, ОПЛАТА БОНУСАМИ ВРЕМЕННО НЕДОСТУПНА";
          if (equipmentStatusList.EquipmentId == 2)
            this.StatusBar3 = "ИЗВИНИТЕ, ОТСУТСТВУЕТ ИНТЕРНЕТ. Возможна только оплата наличными";
          if (equipmentStatusList.EquipmentId == 3)
            this.StatusBar4 = "ИЗВИНИТЕ, ПЕЧАТЬ ЧЕКА ВРЕМЕННО НЕВОЗМОЖНА";
        }
      }
    }

    public int Page
    {
      get => this._page;
      set
      {
        this._page = value;
        this.OnPropertyChanged("ShowHairLines");
        this.OnPropertyChanged("Buttons");
        this.OnPropertyChanged("ButtonsSecond");
        this.OnPropertyChanged("ShowMenuButton");
        this.OnPropertyChanged("ShowBackButton");
        this.OnPropertyChanged("PreviousPageEnabled");
        this.OnPropertyChanged("NextPageEnabled");
      }
    }

    public int Parent
    {
      get => this._parent;
      set
      {
        this._parent = value;
        this.OnPropertyChanged("ShowHairLines");
        this.OnPropertyChanged("Buttons");
        this.OnPropertyChanged("ButtonsSecond");
        this.OnPropertyChanged("ShowMenuButton");
        this.OnPropertyChanged("ShowBackButton");
        this.OnPropertyChanged("PreviousPageEnabled");
        this.OnPropertyChanged("NextPageEnabled");
      }
    }

    public bool Shop
    {
      get => this._shop;
      set
      {
        this._shop = value;
        this.OnPropertyChanged();
        this.OnPropertyChanged("ShowHairLines");
        this.OnPropertyChanged("Buttons");
        this.OnPropertyChanged("ButtonsSecond");
        this.OnPropertyChanged("ShowMenuButton");
        this.OnPropertyChanged("ShowBackButton");
        this.OnPropertyChanged("PreviousPageEnabled");
        this.OnPropertyChanged("NextPageEnabled");
      }
    }

    public int ProductParent
    {
      get => this._productParent;
      set
      {
        this._productParent = value;
        this.OnPropertyChanged("ShowHairLines");
        this.OnPropertyChanged("Buttons");
        this.OnPropertyChanged("ButtonsSecond");
        this.OnPropertyChanged("ShowMenuButton");
        this.OnPropertyChanged("ShowBackButton");
        this.OnPropertyChanged("PreviousPageEnabled");
        this.OnPropertyChanged("NextPageEnabled");
      }
    }

    public int Brand
    {
      get => this._brand;
      set
      {
        this._brand = value;
        this.OnPropertyChanged("ShowHairLines");
        this.OnPropertyChanged("Buttons");
        this.OnPropertyChanged("ButtonsSecond");
        this.OnPropertyChanged("ShowMenuButton");
        this.OnPropertyChanged("ShowBackButton");
        this.OnPropertyChanged("PreviousPageEnabled");
        this.OnPropertyChanged("NextPageEnabled");
      }
    }

    public ICommand ButtonCommand { get; set; }

    public bool PreviousPageEnabled => 0 < this.Page;

    public bool NextPageEnabled
    {
      get
      {
        int num1 = 0;
        if (this.Page == 0 & this.Parent == 0 & this.ProductParent == 0 & this.Brand == 0)
          num1 = 0;
        else if (this.Parent != 0)
        {
          int num2 = (this._buttonsServices ?? new List<ServiceItem>()).Where<ServiceItem>((Func<ServiceItem, bool>) (x =>
          {
            int? parent1 = x.Parent;
            int parent2 = this.Parent;
            return parent1.GetValueOrDefault() == parent2 & parent1.HasValue;
          })).Count<ServiceItem>();
          num1 = num2 / this._pageLimit;
          if (num2 % this._pageLimit == 0)
            --num1;
        }
        else if (this.ProductParent != 0 && this.Brand != 0)
        {
          int num3 = (this._buttonsProducts ?? new List<ProductItem>()).Where<ProductItem>((Func<ProductItem, bool>) (x =>
          {
            int? nullable = x.ProductParent;
            int productParent = this.ProductParent;
            int num4 = nullable.GetValueOrDefault() == productParent & nullable.HasValue ? 1 : 0;
            nullable = x.Brand;
            int brand = this.Brand;
            int num5 = nullable.GetValueOrDefault() == brand & nullable.HasValue ? 1 : 0;
            return (num4 & num5) != 0;
          })).Count<ProductItem>();
          num1 = num3 / this._pageProductLimit;
          if (num3 % this._pageProductLimit == 0)
            --num1;
        }
        return num1 > this.Page;
      }
    }

    public Cart Cart
    {
      get => this._cart;
      set
      {
        this._cart = value;
        this.OnPropertyChanged(this.Cart.SumText);
        this.OnPropertyChanged(this.Cart.CountText);
      }
    }

    public ICommand PriviousPageCommand { get; set; }

    public ICommand NextPageCommand { get; set; }

    public ICommand FrCommand { get; set; }

    public ICommand CartCommand { get; set; }

    public Brush StatusPanelColor { get; set; }
  }
}
