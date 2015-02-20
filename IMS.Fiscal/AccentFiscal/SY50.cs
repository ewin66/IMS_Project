// Decompiled with JetBrains decompiler
// Type: Bujar.Fiscal.AccentFiscal.SY50
// Assembly: Bujar.Fiscal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F67EA74-8FA1-4626-BF9D-4F6BED24CDA7
// Assembly location: G:\GitHub\IMS_Project\Library\_Fiskalni\Bujar.Fiscal.dll

using IMS.Fiscal;
using IMS.Fiscal.My.Resources;
using IMS.Fiscal.Properties;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IMS.Fiscal.AccentFiscal
{
  [ComVisible(true)]
  public sealed class SY50
  {
    public static int LastCommand = 0;
    private int _ComPortNumber;
    private string _ComPort;
    private List<Article> _Items;
    private string AppPath;
    private string INIPath;
    private string TextFile;

    public int ComPortNumber
    {
      get
      {
        return this._ComPortNumber;
      }
      set
      {
        this._ComPortNumber = value;
      }
    }
    public string ComPort
    {
        get
        {
            return this._ComPort;
        }
        set
        {
            this._ComPort = value;
        }
    }
    public List<Article> Stavki
    {
      get
      {
        return this._Items;
      }
      set
      {
        this._Items = new List<Article>();
        this._Items = value;
      }
    }

    private SY50()
    {
      this._Items = new List<Article>();
      if (!Directory.Exists(AppConfig.AppLocal))
        Directory.CreateDirectory(AppConfig.AppLocal);
      this.AppPath = AppConfig.AppLocal + "Fiscal32.exe";
      this.INIPath = AppConfig.AppLocal + "fiskal.ini";
      this.TextFile = AppConfig.AppLocal + "PF500.in";
      this._ComPortNumber = 1;
      this._Items = new List<Article>();
    }

    public SY50(string ComPort)
      : this()
    {
      this._ComPort = ComPort;
      this.CreateIniFile();
      if (File.Exists(this.AppPath))
        return;
      this.CreateExecutable();
    }

    public SY50(string ComPort, List<Article> stavki)
      : this(ComPort)
    {
      this.Stavki = stavki;
    }

    [ComVisible(true)]
    public void FiskalnaSmetka(SY50.PaidMode PaidType = SY50.PaidMode.VoGotovo)
    {
      if (this.Stavki.Count == 0)
        return;
      this.CreateFiskalnaSY50(PaidType);
      this.Run();
    }

    [ComVisible(true)]
    public void FiskalnaSmetka(List<Article> Stavki, SY50.PaidMode PaidType = SY50.PaidMode.VoGotovo)
    {
      this.Stavki = Stavki;
      this.FiskalnaSmetka(PaidType);
    }

    [ComVisible(true)]
    public void StornaSmetka(SY50.PaidMode PaidType = SY50.PaidMode.VoGotovo)
    {
      if (this.Stavki.Count == 0)
        return;
      this.CreateStornaSY50(PaidType);
      this.Run();
    }

    [ComVisible(true)]
    public void StornaSmetka(List<Article> Stavki, SY50.PaidMode PaidType = SY50.PaidMode.VoGotovo)
    {
      this.Stavki = Stavki;
      this.StornaSmetka(PaidType);
    }

    [ComVisible(true)]
    public void PodesuvajCas()
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        char ch;
        if (SY50.LastCommand == 1)
        {
          ch = '#';
          SY50.LastCommand = 2;
        }
        else
        {
          ch = '$';
          SY50.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + string.Format("={0}\t", (object) DateTime.Now.ToString("dd-MM-yy HH:MM:ss")));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void ZatvoriDen()
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        char ch;
        if (SY50.LastCommand == 1)
        {
          ch = '#';
          SY50.LastCommand = 2;
        }
        else
        {
          ch = '$';
          SY50.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "EZ\t");
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void Izvestaj_X()
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        char ch;
        if (SY50.LastCommand == 1)
        {
          ch = '#';
          SY50.LastCommand = 2;
        }
        else
        {
          ch = '$';
          SY50.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "EX\t");
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void SluzbenIzlez(Decimal Amount)
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        char ch;
        if (SY50.LastCommand == 1)
        {
          ch = '#';
          SY50.LastCommand = 2;
        }
        else
        {
          ch = '$';
          SY50.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + string.Format("F1\t{0}\t", (object) this.FormatNumber(Amount, 2)));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void SluzbenVlez(Decimal Amount)
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        char ch;
        if (SY50.LastCommand == 1)
        {
          ch = '#';
          SY50.LastCommand = 2;
        }
        else
        {
          ch = '$';
          SY50.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + string.Format("F0\t{0}\t", (object) this.FormatNumber(Amount, 2)));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void DetalenPeriodicenIzvestaj(DateTime OdDatum, DateTime DoDatum)
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        char ch;
        if (SY50.LastCommand == 1)
        {
          ch = '#';
          SY50.LastCommand = 2;
        }
        else
        {
          ch = '$';
          SY50.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + string.Format("^1\t{0}\t{1}\t", (object) OdDatum.ToString("dd-MM-yy"), (object) DoDatum.ToString("dd-MM-yy")));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void PeriodicenIzvestaj(DateTime OdDatum, DateTime DoDatum)
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        char ch;
        if (SY50.LastCommand == 1)
        {
          ch = '#';
          SY50.LastCommand = 2;
        }
        else
        {
          ch = '$';
          SY50.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + string.Format("^0\t{0}\t{1}\t", (object) OdDatum.ToString("dd-MM-yy"), (object) DoDatum.ToString("dd-MM-yy")));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void Diagnostika()
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        char ch;
        if (SY50.LastCommand == 1)
        {
          ch = '#';
          SY50.LastCommand = 2;
        }
        else
        {
          ch = '$';
          SY50.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "G\r\n");
        streamWriter.Close();
      }
      this.Run();
    }

    private void CreateStornaSY50(SY50.PaidMode PaidMode = SY50.PaidMode.VoGotovo)
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding);
      streamWriter.Write(" 01\t1\t\t1\t\r\n");
      int num1 = 0;
      foreach (var current in this.Stavki)
      {
          Strings.Chr((int)current.VAT);
          short num2 = (short)1;
          switch (current.VAT)
          {
              case VATgroup.А:
                  num2 = (short)1;
                  break;
              case VATgroup.Б:
                  num2 = (short)2;
                  break;
              case VATgroup.В:
                  num2 = (short)3;
                  break;
              case VATgroup.Г:
                  num2 = (short)4;
                  break;
          }
          if (num1 % 2 == 0)
              streamWriter.Write(string.Format("#1{0}\t{3}\t{1}\t{2}\t0\t\t\t", (object)current.Name, (object)this.FormatNumber(current.Price, 2), (object)this.FormatNumber(current.Quantity, 3), (object)num2) + "\r\n");
          else
              streamWriter.Write(string.Format(" 1{0}\t{3}\t{1}\t{2}\t0\t\t\t", (object)current.Name, (object)this.FormatNumber(current.Price, 2), (object)this.FormatNumber(current.Quantity, 3), (object)num2) + "\r\n");
          checked { ++num1; }
      }
      short num3 = (short)0;
      switch (PaidMode)
      {
          case SY50.PaidMode.VoGotovo:
              num3 = (short)0;
              break;
          case SY50.PaidMode.SoKarticka:
              num3 = (short)1;
              break;
          case SY50.PaidMode.SoKredit:
              num3 = (short)2;
              break;
      }
      streamWriter.Write(string.Format("&5{0}\t\t", (object)num3) + "\r\n");
      streamWriter.Write("%8");
      streamWriter.Flush();
      streamWriter.Close();
    }

    private void CreateFiskalnaSY50(SY50.PaidMode PaidMode = SY50.PaidMode.VoGotovo)
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding);
      streamWriter.Write(" 01\t1\t\t0\t\r\n");
      int num1 = 0;
      foreach (var current in this.Stavki)
      {
          Strings.Chr((int)current.VAT);
          short num2 = (short)1;
          switch (current.VAT)
          {
              case VATgroup.А:
                  num2 = (short)1;
                  break;
              case VATgroup.Б:
                  num2 = (short)2;
                  break;
              case VATgroup.В:
                  num2 = (short)3;
                  break;
              case VATgroup.Г:
                  num2 = (short)4;
                  break;
          }
          if (num1 % 2 == 0)
              streamWriter.Write(string.Format("#1{0}\t{3}\t{1}\t{2}\t{4}\t\t\t\r\n", (object)current.Name, (object)this.FormatNumber(current.Price, 2), (object)this.FormatNumber(current.Quantity, 3), (object)num2, current.IsDomestic ? 1 : 0));
          else
              streamWriter.Write(string.Format(" 1{0}\t{3}\t{1}\t{2}\t{4}\t\t\t\r\n", (object)current.Name, (object)this.FormatNumber(current.Price, 2), (object)this.FormatNumber(current.Quantity, 3), (object)num2, current.IsDomestic ? 1 : 0));
          checked { ++num1; }
      }

      short num3 = (short)0;
      switch (PaidMode)
      {
          case SY50.PaidMode.VoGotovo:
              num3 = (short)0;
              break;
          case SY50.PaidMode.SoKarticka:
              num3 = (short)1;
              break;
          case SY50.PaidMode.SoKredit:
              num3 = (short)2;
              break;
      }
      streamWriter.Write(string.Format("&5{0}\t\t", (object)num3) + "\r\n");
      streamWriter.Write("%8\r\n");
      streamWriter.Flush();
      streamWriter.Close();
    }

    private void CreateExecutable()
    {
      using (FileStream fileStream = new FileStream(this.AppPath, FileMode.CreateNew, FileAccess.Write))
        fileStream.Write(Resources.Fiscal32, 0, Resources.Fiscal32.Length);
    }

    private void CreateIniFile()
    {
      if (File.Exists(this.INIPath))
        File.Delete(this.INIPath);
      using (StreamWriter text = File.CreateText(this.INIPath))
      {
        text.WriteLine(";Ini fajlot treba da bide vo ist dir. so fiscal32.exe");
        text.WriteLine("");
        text.WriteLine("[Setup]");
        //text.WriteLine(string.Format("Port=COM{0}", (object) this.ComPortNumber));
        text.WriteLine(string.Format("Port={0}", (object)this.ComPort));
        text.WriteLine("Speed=5");
        text.WriteLine("Bit=8");
        text.WriteLine("Parity=0");
        text.WriteLine("Stop=1");
        text.WriteLine("Flow=0");
        text.Close();
      }
    }

    private void Run()
    {
      Interaction.Shell(string.Format("{0} {1}", (object) this.AppPath, (object) this.TextFile), AppWinStyle.Hide, false, -1);
    }

    public string FormatNumber(Decimal Number, int DecimalDigits)
    {
      if (Decimal.Compare(Number, Decimal.Zero) == 0)
      {
        string str = "0.";
        int num1 = DecimalDigits;
        int num2 = 1;
        while (num2 <= num1)
        {
          str += "0";
          checked { ++num2; }
        }
        return str;
      }
      long num = checked ((long) Math.Round(Math.Round(unchecked (Convert.ToDouble(Number) * Math.Pow(10.0, (double) DecimalDigits)))));
      return Strings.Left(Conversions.ToString(num), checked (Strings.Len(Conversions.ToString(num)) - DecimalDigits)) + "." + Strings.Right(Conversions.ToString(num), DecimalDigits);
    }

    public enum PaidMode
    {
      VoGotovo = 0,
      SoKarticka = 1,
      SoKredit = 2,
    }
  }
}
