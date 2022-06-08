// Decompiled with JetBrains decompiler
// Type: Terminal.Services.KeyService
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Guardant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terminal.Interfaces;

namespace Terminal.Services
{
  public class KeyService : IHardware
  {
    private static readonly System.Lazy<KeyService> Lazy = new System.Lazy<KeyService>((Func<KeyService>) (() => new KeyService()));

    public static KeyService Instance => KeyService.Lazy.Value;

    private KeyService()
    {
    }

    public bool IsOk { get; set; }

    public void Init()
    {
      if (GrdApi.GrdStartup(GrdFMR.Local) != 0)
      {
        this.IsOk = false;
      }
      else
      {
        this.GrdHandle = GrdApi.GrdCreateHandle(GrdCHM.MultiThread);
        this.ErrorHandling(this.GrdHandle, GrdApi.GrdSetAccessCodes(this.GrdHandle, 1215324879U, 239681353U));
        GrdFM flags = GrdFM.ALL;
        uint prog = 0;
        uint ver = 0;
        uint sn = 0;
        uint mask = 0;
        uint id = 0;
        GrdDT type = GrdDT.ALL;
        GrdFMM models = GrdFMM.ALL;
        GrdFMI interfaces = GrdFMI.ALL;
        this.ErrorHandling(this.GrdHandle, GrdApi.GrdSetFindMode(this.GrdHandle, GrdFMR.Local, flags, prog, id, sn, ver, mask, type, models, interfaces));
        GrdApi.GrdFind(this.GrdHandle, GrdF.First, out id, out FindInfo _);
        GrdE nGrdE = GrdApi.GrdLogin(this.GrdHandle, -1, GrdLM.PerStation);
        if (nGrdE != 0)
        {
          this.IsOk = false;
          KeyService.CloseGuardantApi(this.GrdHandle);
        }
        else
        {
          this.ErrorHandling(this.GrdHandle, nGrdE);
          byte[] data1;
          this.ErrorHandling(this.GrdHandle, GrdApi.GrdRead(this.GrdHandle, (GrdUAM) 40, 20, out data1));
          if (KeyService.FromByteArrayToString(data1) != "StrizhkaShop")
            return;
          byte[] data2;
          this.ErrorHandling(this.GrdHandle, GrdApi.GrdRead(this.GrdHandle, (GrdUAM) 60, 10, out data2));
          if (StorageService.Instance.Data.SetTerminalCode(KeyService.FromByteArrayToString(data2)))
            StorageService.Instance.Save();
          byte[] data3;
          this.ErrorHandling(this.GrdHandle, GrdApi.GrdRead(this.GrdHandle, (GrdUAM) 70, 20, out data3));
          KeyService.FromByteArrayToString(data3);
        }
      }
    }

    public void Stop() => KeyService.CloseGuardantApi(this.GrdHandle);

    public Handle GrdHandle { get; set; }

    private static string FromByteArrayToString(byte[] bytes)
    {
      byte[] array = ((IEnumerable<byte>) bytes).Where<byte>((Func<byte, bool>) (b => b > (byte) 0)).ToArray<byte>();
      char[] chars = new char[array.Length];
      new ASCIIEncoding().GetChars(array, 0, array.Length, chars, 0);
      return new string(chars);
    }

    private static void CloseGuardantApi(Handle grdHandle)
    {
      Console.Write("Closing handle: ");
      GrdE grdE = GrdApi.GrdCloseHandle(grdHandle);
      Console.Write("Deinitializing this copy of GrdAPI: ");
      grdE = GrdApi.GrdCleanup();
    }

    private void ErrorHandling(Handle grdHandle, GrdE nGrdE)
    {
      if (nGrdE == GrdE.OK)
        return;
      this.IsOk = false;
      if (grdHandle.Address != IntPtr.Zero)
      {
        int num1 = (int) GrdApi.GrdCloseHandle(grdHandle);
      }
      int num2 = (int) GrdApi.GrdCleanup();
    }
  }
}
