// Decompiled with JetBrains decompiler
// Type: Terminal.Services.StorageData
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Terminal.Environment;

namespace Terminal.Services
{
  [DataContract]
  public class StorageData
  {
    [DataMember]
    public List<ServiceParentItem> ServiceParents { get; private set; }

    [DataMember]
    public List<ServiceItem> Services { get; private set; }

    [DataMember]
    public List<ProductItem> Products { get; private set; }

    [DataMember]
    public List<Core.Classes.Purchase> Purchase { get; private set; }

    [DataMember]
    public List<Guid> PurchaseUploaded { get; private set; }

    [DataMember]
    public List<Core.Classes.Encashment> Encashment { get; private set; }

    [DataMember]
    public string TerminalCode { get; private set; }

    [DataMember]
    public List<ChannelLevelInfo> Stacked { get; private set; }

    [DataMember]
    public List<User> Users { get; private set; }

    [DataMember]
    public List<CheckIn> CheckIns { get; private set; }

    public void Add(PurchaseResponce purchaseResponce)
    {
      if (this.PurchaseUploaded == null)
        this.PurchaseUploaded = new List<Guid>();
      if (this.PurchaseUploaded.Count<Guid>((Func<Guid, bool>) (x => x == purchaseResponce.Guid)) != 0)
        return;
      this.PurchaseUploaded.Add(purchaseResponce.Guid);
    }

    public void Add(Core.Classes.Purchase purchase)
    {
      if (this.Purchase == null)
        this.Purchase = (List<Core.Classes.Purchase>) new EventedList<Core.Classes.Purchase>();
      if (this.Purchase.Count<Core.Classes.Purchase>((Func<Core.Classes.Purchase, bool>) (x => x.Guid == purchase.Guid)) != 0)
        return;
      this.Purchase.Add(purchase);
    }

    public void Replace(List<ServiceParentItem> serviceParents) => this.ServiceParents = serviceParents;

    public void Replace(List<ServiceItem> services) => this.Services = services;

    public void Replace(List<ProductItem> products) => this.Products = products;

    public void Replace(List<User> users) => this.Users = users;

    public void Replace(CheckIn checkIn) => this.CheckIns.Find((Predicate<CheckIn>) (x => x.TerminalCode == checkIn.TerminalCode && x.UserId == checkIn.UserId && x.DateTime == checkIn.DateTime)).Send = true;

    public void Add(Core.Classes.Encashment en)
    {
      if (this.Encashment == null)
        this.Encashment = (List<Core.Classes.Encashment>) new EventedList<Core.Classes.Encashment>();
      if (this.Encashment.Count<Core.Classes.Encashment>((Func<Core.Classes.Encashment, bool>) (x => x.Guid == en.Guid)) != 0)
        return;
      this.Encashment.Add(en);
    }

    public void Add(ChannelLevelInfo note)
    {
      if (this.Stacked == null)
      {
        this.Stacked = new List<ChannelLevelInfo>();
        this.Stacked.Add(note);
      }
      else
      {
        ChannelLevelInfo channelLevelInfo = this.Stacked.FirstOrDefault<ChannelLevelInfo>((Func<ChannelLevelInfo, bool>) (x => x.Note == note.Note));
        if (channelLevelInfo == null)
        {
          this.Stacked.Add(note);
        }
        else
        {
          note.Count += channelLevelInfo.Count;
          this.Stacked.Remove(channelLevelInfo);
          this.Stacked.Add(note);
        }
      }
    }

    public void Add(CheckIn checkIn)
    {
      if (this.CheckIns == null)
        this.CheckIns = new List<CheckIn>();
      this.CheckIns.Add(checkIn);
    }

    public void ClearStacked()
    {
      if (this.Stacked == null)
        return;
      this.Stacked.Clear();
    }

    public bool SetTerminalCode(string value)
    {
      if (this.TerminalCode == value)
        return false;
      this.TerminalCode = value;
      return true;
    }
  }
}
