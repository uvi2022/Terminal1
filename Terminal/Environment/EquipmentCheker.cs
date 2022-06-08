// Decompiled with JetBrains decompiler
// Type: Terminal.Environment.EquipmentCheker
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using System;
using System.Linq;
using System.Windows.Threading;

namespace Terminal.Environment
{
  public class EquipmentCheker
  {
    public event EquipmentCheker.ChangedEventHandler OnEquipmentStatusChanged;

    protected void EquipmentStatusChanged(EventArgs e)
    {
      if (this.OnEquipmentStatusChanged == null)
        return;
      this.OnEquipmentStatusChanged((object) this.EquipmentsStatus, e);
    }

    private EventedList<EquipmentStatusList> EquipmentsStatus { get; set; }

    public EquipmentCheker()
    {
      this.EquipmentsStatus = new EventedList<EquipmentStatusList>();
      this.EquipmentsStatus.Changed += new Terminal.Environment.ChangedEventHandler(this.EquipmentsStatus_Changed);
      DispatcherTimer dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.Tick += new EventHandler(this.OnTimerTick);
      dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
      dispatcherTimer.Start();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
    }

    private void EquipmentsStatus_Changed(object sender, EventArgs e) => this.EquipmentStatusChanged(e);

    private void CheckReceiptPrinter() => this.AddToEquipmentStatusList(3, (int) new ReceiptPrinter().CheckStatus());

    private void CheckDatabaseConnection() => this.AddToEquipmentStatusList(1, (int) new DatabaseConnection().CheckStatus());

    private void CheckBanknotesReceiver() => this.AddToEquipmentStatusList(0, (int) new BanknotesReceiver().CheckStatus());

    private void CheckInternetConnection() => this.AddToEquipmentStatusList(2, (int) new InternetConnection().CheckStatus());

    private void AddToEquipmentStatusList(int itemId, int statusId)
    {
      lock (this.EquipmentsStatus)
      {
        EquipmentStatusList equipmentStatusList = this.EquipmentsStatus.FirstOrDefault<EquipmentStatusList>((Func<EquipmentStatusList, bool>) (x => x.EquipmentId == itemId));
        if (equipmentStatusList == null)
        {
          this.EquipmentsStatus.Add(new EquipmentStatusList()
          {
            EquipmentId = itemId,
            EquipmentStatusId = statusId
          });
        }
        else
        {
          if (equipmentStatusList.EquipmentStatusId == statusId)
            return;
          this.EquipmentsStatus.Remove(equipmentStatusList);
          this.EquipmentsStatus.Add(new EquipmentStatusList()
          {
            EquipmentId = itemId,
            EquipmentStatusId = statusId
          });
        }
      }
    }

    public delegate void ChangedEventHandler(object sender, EventArgs e);
  }
}
