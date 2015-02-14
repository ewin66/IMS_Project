// Decompiled with JetBrains decompiler
// Type: Bujar.Fiscal.AppConfig
// Assembly: Bujar.Fiscal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F67EA74-8FA1-4626-BF9D-4F6BED24CDA7
// Assembly location: G:\GitHub\IMS_Project\Library\_Fiskalni\Bujar.Fiscal.dll

using IMS.Fiscal.My;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace IMS.Fiscal
{
  public class AppConfig
  {
    private static AssemblyName _Application = (AssemblyName) null;
    private static IPAddress _IP;

    public static AssemblyName Application
    {
      get
      {
        if (AppConfig._Application == null)
        {
          Assembly entryAssembly = Assembly.GetEntryAssembly();
          AppConfig._Application = entryAssembly == null ? new AssemblyName() : entryAssembly.GetName();
        }
        return AppConfig._Application;
      }
    }

    public static string ApplicationName
    {
      get
      {
        return AppConfig.Application.Name;
      }
    }

    public static string ApplicationPath
    {
      get
      {
        return MyProject.Application.Info.DirectoryPath + "\\";
      }
    }

    public static string AppLocal
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + AppConfig.ApplicationName + "\\Fiscal\\";
      }
    }

    public static IPAddress IP
    {
      get
      {
        if (AppConfig._IP == null)
        {
          IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
          AppConfig._IP = new IPAddress(0L);
          IPAddress[] addressList = hostEntry.AddressList;
          int index = 0;
          while (index < addressList.Length)
          {
            IPAddress ipAddress = addressList[index];
            if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
              AppConfig._IP = ipAddress;
            checked { ++index; }
          }
        }
        return AppConfig._IP;
      }
    }

    [DebuggerNonUserCode]
    public AppConfig()
    {
    }
  }
}
