// Decompiled with JetBrains decompiler
// Type: Terminal.Classes.DataExchange
// Assembly: Terminal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFB95130-AF60-462C-8ECD-11AF0F129423
// Assembly location: C:\Users\Пользователь\Downloads\Terminal\Release\Terminal.exe

using Core.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Terminal.Services;

namespace Terminal.Classes
{
  public class DataExchange
  {
    public static ServerResponse GetServiceParents() => Task.Run<ServerResponse>((Func<ServerResponse>) (() =>
    {
      ServerResponse serviceParents = new ServerResponse()
      {
        Status = -1,
        StatusMessage = "empty"
      };
      try
      {
        serviceParents = JsonConvert.DeserializeObject<ServerResponse>(new WebClient()
        {
          Encoding = Encoding.UTF8
        }.DownloadString(string.Format("{0}/service-parent/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId)));
        Logger.Log.Info((object) "Получаем Группы Услуг с сервера");
        if (serviceParents.Status != 0)
          Logger.Log.Warn((object) "Cервер вернул ошибку");
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ("Не удалось получить Группы Услуг с сервера; " + ex.ToString()));
      }
      return serviceParents;
    })).Result;

    public static ServerResponse GetServices() => Task.Run<ServerResponse>((Func<ServerResponse>) (() =>
    {
      ServerResponse services = new ServerResponse()
      {
        Status = -1,
        StatusMessage = "empty"
      };
      try
      {
        services = JsonConvert.DeserializeObject<ServerResponse>(new WebClient()
        {
          Encoding = Encoding.UTF8
        }.DownloadString(string.Format("{0}/services/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId)));
        Logger.Log.Info((object) "Получаем Услуги с сервера");
        if (services.Status != 0)
          Logger.Log.Warn((object) "Cервер вернул ошибку");
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ("Не удалось получить Услуги с сервера; " + ex.ToString()));
      }
      return services;
    })).Result;

    public static ServerResponse GetProducts() => Task.Run<ServerResponse>((Func<ServerResponse>) (() =>
    {
      ServerResponse products = new ServerResponse()
      {
        Status = -1,
        StatusMessage = "empty"
      };
      try
      {
        products = JsonConvert.DeserializeObject<ServerResponse>(new WebClient()
        {
          Encoding = Encoding.UTF8
        }.DownloadString(string.Format("{0}/products/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId)));
        Logger.Log.Info((object) "Получаем Товары с сервера");
        if (products.Status != 0)
          Logger.Log.Warn((object) "Cервер вернул ошибку");
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ("Не удалось получить Товары с сервера; " + ex.ToString()));
      }
      return products;
    })).Result;

    public static ServerResponse GetUsers() => Task.Run<ServerResponse>((Func<ServerResponse>) (() =>
    {
      ServerResponse users = new ServerResponse()
      {
        Status = -1,
        StatusMessage = "empty"
      };
      try
      {
        users = JsonConvert.DeserializeObject<ServerResponse>(new WebClient()
        {
          Encoding = Encoding.UTF8
        }.DownloadString(string.Format("{0}/users/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId)));
        Logger.Log.Info((object) "Получаем Пользователей с сервера");
        if (users.Status != 0)
          Logger.Log.Warn((object) "Cервер вернул ошибку");
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ("Не удалось получить Пользователей с сервера; " + ex.ToString()));
      }
      return users;
    })).Result;

    public static Task<BonusResponse> GetClientInfo(
      long phoneNumber,
      string sexValue,
      byte? minAgeValue,
      byte? maxAgeValue)
    {
      return Task.Run<BonusResponse>((Func<BonusResponse>) (() =>
      {
        if (phoneNumber == 0L || phoneNumber == 7L)
          return new BonusResponse()
          {
            PhoneNumber = 0,
            Bonuses = 0
          };
        BonusResponse clientInfo = new BonusResponse()
        {
          PhoneNumber = phoneNumber,
          Bonuses = 0
        };
        try
        {
          string str = JsonConvert.SerializeObject((object) new JObject()
          {
            [nameof (sexValue)] = (JToken) sexValue,
            [nameof (minAgeValue)] = (JToken) minAgeValue,
            [nameof (maxAgeValue)] = (JToken) maxAgeValue
          });
          Logger.Log.Warn((object) str);
          WebClient webClient = new WebClient()
          {
            Encoding = Encoding.UTF8
          };
          webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
          string message = webClient.UploadString(string.Format("{0}/bonuses/info/{1}/{2}", (object) Global.WebApiUrl, (object) Global.TerminalId, (object) phoneNumber), str);
          Logger.Log.Warn((object) message);
          clientInfo = JsonConvert.DeserializeObject<BonusResponse>(message);
        }
        catch (Exception ex)
        {
        }
        return clientInfo;
      }));
    }

    public static Task<PinResponse> GetPinResponse(long phoneNumber) => Task.Run<PinResponse>((Func<PinResponse>) (() =>
    {
      if (phoneNumber == 0L)
        return new PinResponse()
        {
          PhoneNumber = 0,
          Pin = 0
        };
      return JsonConvert.DeserializeObject<PinResponse>(new WebClient()
      {
        Encoding = Encoding.UTF8
      }.DownloadString(string.Format("{0}/bonuses/pin/{1}/{2}", (object) Global.WebApiUrl, (object) Global.TerminalId, (object) phoneNumber)));
    }));

    public static DateTime GetDateTime() => DateTime.UtcNow;

    public static async Task<PurchaseResponce> SendPayment(Purchase ps)
    {
      if (string.IsNullOrEmpty(ps.PurchaseDatetime))
        ps.PurchaseDatetime = ps.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
      PurchaseResponce result = await DataExchange.SendPaymentResult(ps);
      PurchaseResponce purchaseResponce = result;
      result = (PurchaseResponce) null;
      return purchaseResponce;
    }

    private static Task<PurchaseResponce> SendPaymentResult(Purchase ps) => Task.Run<PurchaseResponce>((Func<PurchaseResponce>) (() =>
    {
      try
      {
        WebClient webClient = new WebClient()
        {
          Encoding = Encoding.UTF8
        };
        Uri address = new Uri(Global.WebApiUrl + "/purchase/");
        string str1 = JsonConvert.SerializeObject((object) ps);
        webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
        string str2 = webClient.UploadString(address, "POST", str1);
        Logger.Log.Info((object) "Отправка платежа");
        Logger.Log.Info((object) str1);
        return JsonConvert.DeserializeObject<PurchaseResponce>(str2);
      }
      catch (Exception ex)
      {
        return new PurchaseResponce()
        {
          Guid = ps.Guid,
          Status = 1,
          StatusMessage = "Error: " + ex.ToString()
        };
      }
    }));

    public static Task<TestConnectionResponse> CheckInternetConnectionAsync() => Task.Run<TestConnectionResponse>((Func<TestConnectionResponse>) (() =>
    {
      TestConnectionResponse connectionResponse;
      try
      {
        string str = new WebClient()
        {
          Encoding = Encoding.UTF8
        }.DownloadString(string.Format("{0}/status/?terminalCode={1}", (object) Global.WebApiUrl, (object) Global.TerminalId));
        Logger.Log.Info((object) str.ToString());
        connectionResponse = JsonConvert.DeserializeObject<TestConnectionResponse>(str);
      }
      catch (Exception ex)
      {
        Logger.Log.Error((object) ex.ToString());
        connectionResponse = new TestConnectionResponse()
        {
          IsDatabaseConnected = false,
          Status = 1,
          StatusMessage = "ConnectionError"
        };
      }
      return connectionResponse;
    }));

    public static Task<int> CheckDatabaseConnectionAsync() => Task.Run<int>((Func<int>) (() =>
    {
      int num;
      try
      {
        num = JsonConvert.DeserializeObject<TestConnectionResponse>(new WebClient()
        {
          Encoding = Encoding.UTF8
        }.DownloadString(Global.WebApiUrl + "/status/db/")).IsDatabaseConnected ? 0 : 1;
      }
      catch (Exception ex)
      {
        num = 1;
      }
      return num;
    }));

    public static Task<List<Purchase>> GetPaidServicesForIncas() => Task.Run<List<Purchase>>((Func<List<Purchase>>) (() =>
    {
      try
      {
        return JsonConvert.DeserializeObject<List<Purchase>>(new WebClient()
        {
          Encoding = Encoding.UTF8
        }.DownloadString(string.Format("{0}/purchase/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId)));
      }
      catch (Exception ex)
      {
        return new List<Purchase>();
      }
    }));

    public static async Task<bool> SendCheckIn(CheckIn checkIn)
    {
      bool flag = await Task.Run<bool>((Func<bool>) (() =>
      {
        try
        {
          using (WebClient webClient = new WebClient()
          {
            Encoding = Encoding.UTF8
          })
          {
            string data = JsonConvert.SerializeObject((object) checkIn);
            Logger.Log.Info((object) ("Отправляем Чекин: " + data));
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            ServerResponse serverResponse = JsonConvert.DeserializeObject<ServerResponse>(webClient.UploadString(Global.WebApiUrl + "/users/checkin", "PUT", data));
            if (serverResponse.Status == 0)
            {
              Logger.Log.Info((object) "Чекин отправлен");
              StorageService.Instance.Data.Replace(checkIn);
              return true;
            }
            Logger.Log.Error((object) ("Не удалось отправить Чекин: " + serverResponse.StatusMessage));
            return false;
          }
        }
        catch (Exception ex)
        {
          Logger.Log.Error((object) ("Ошибка отправки Чекина: " + ex.ToString()));
          return false;
        }
      }));
      return flag;
    }

    public static Task<string> SendVersion() => Task.Run<string>((Func<string>) (() =>
    {
      TerminalStatusResponse terminalStatusResponse = new TerminalStatusResponse();
      terminalStatusResponse.TerminalCode = Global.TerminalId.ToString();
      terminalStatusResponse.TerminalVersion = Global.Version;
      try
      {
        using (WebClient webClient = new WebClient()
        {
          Encoding = Encoding.UTF8
        })
        {
          webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
          webClient.UploadString(string.Format("{0}/status/version/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId), "PUT", JsonConvert.SerializeObject((object) terminalStatusResponse));
          Logger.Log.Info((object) ("Отправлена версия ПО: " + Global.Version + ";"));
          return "200";
        }
      }
      catch (Exception ex)
      {
        Logger.Log.Warn((object) ("Не удалось отправить версию ПО: " + Global.Version + "; " + ex.ToString() + "; " + JsonConvert.SerializeObject((object) terminalStatusResponse)));
        return "404";
      }
    }));

    public static Task<string> SendPrinterStatus(string printerStatus) => Task.Run<string>((Func<string>) (() =>
    {
      TerminalStatusResponse terminalStatusResponse = new TerminalStatusResponse();
      terminalStatusResponse.TerminalCode = Global.TerminalId.ToString();
      terminalStatusResponse.TerminalVersion = Global.Version;
      terminalStatusResponse.Printer = printerStatus;
      try
      {
        using (WebClient webClient = new WebClient()
        {
          Encoding = Encoding.UTF8
        })
        {
          webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
          webClient.UploadString(string.Format("{0}/status/printer/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId), "PUT", JsonConvert.SerializeObject((object) terminalStatusResponse));
          Logger.Log.Info((object) ("Отправлен статус принтера: " + printerStatus + ";"));
          return "200";
        }
      }
      catch (Exception ex)
      {
        Logger.Log.Warn((object) ("Не удалось отправить статус принтера: " + printerStatus + "; " + ex.ToString() + "; " + JsonConvert.SerializeObject((object) terminalStatusResponse)));
        return "404";
      }
    }));

    public static Task<string> SendNv200Status(string nv200Status) => Task.Run<string>((Func<string>) (() =>
    {
      TerminalStatusResponse terminalStatusResponse = new TerminalStatusResponse();
      terminalStatusResponse.TerminalCode = Global.TerminalId.ToString();
      terminalStatusResponse.TerminalVersion = Global.Version;
      terminalStatusResponse.Nv200 = nv200Status;
      try
      {
        using (WebClient webClient = new WebClient()
        {
          Encoding = Encoding.UTF8
        })
        {
          webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
          webClient.UploadString(string.Format("{0}/status/pay/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId), "PUT", JsonConvert.SerializeObject((object) terminalStatusResponse));
          Logger.Log.Info((object) ("Отправлен статус Купюрника: " + nv200Status + ";"));
          return "200";
        }
      }
      catch (Exception ex)
      {
        Logger.Log.Warn((object) ("Не удалось отправить статус Купюрника: " + nv200Status + "; " + ex.ToString() + "; " + JsonConvert.SerializeObject((object) terminalStatusResponse)));
        return "404";
      }
    }));

    public static Task<string> SendCardStatus(string CardStatus) => Task.Run<string>((Func<string>) (() =>
    {
      TerminalStatusResponse terminalStatusResponse = new TerminalStatusResponse();
      terminalStatusResponse.TerminalCode = Global.TerminalId.ToString();
      terminalStatusResponse.TerminalVersion = Global.Version;
      terminalStatusResponse.Card = CardStatus;
      try
      {
        using (WebClient webClient = new WebClient()
        {
          Encoding = Encoding.UTF8
        })
        {
          webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
          webClient.UploadString(string.Format("{0}/status/card/{1}", (object) Global.WebApiUrl, (object) Global.TerminalId), "PUT", JsonConvert.SerializeObject((object) terminalStatusResponse));
          Logger.Log.Info((object) ("Отправлен статус Пинки: " + CardStatus + ";"));
          return "200";
        }
      }
      catch (Exception ex)
      {
        Logger.Log.Warn((object) ("Не удалось отправить статус Пинки: " + CardStatus + "; " + ex.ToString() + "; " + JsonConvert.SerializeObject((object) terminalStatusResponse)));
        return "404";
      }
    }));
  }
}
