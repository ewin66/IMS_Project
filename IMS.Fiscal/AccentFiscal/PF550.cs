// Decompiled with JetBrains decompiler
// Type: Bujar.Fiscal.AccentFiscal.PF550
// Assembly: Bujar.Fiscal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F67EA74-8FA1-4626-BF9D-4F6BED24CDA7
// Assembly location: G:\GitHub\IMS_Project\Library\_Fiskalni\Bujar.Fiscal.dll

using Viktor.IMS.Fiscal;
using Viktor.IMS.Fiscal.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Viktor.IMS.Fiscal.Properties;

namespace Viktor.IMS.Fiscal.AccentFiscal
{
  [ComVisible(true)]
  public sealed class PF550
  {
    public static int LastCommand = 0;
    private int _ComPortNumber;
    private string _ComPort;
    private List<Article> _Items;
    private string AppPath;
    private string INIPath;
    private string TextFile;
    private Encoding cyrillic;

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

    private PF550()
    {
      this._Items = new List<Article>();
      if (!Directory.Exists(AppConfig.AppLocal))
        Directory.CreateDirectory(AppConfig.AppLocal);
      this.AppPath = AppConfig.AppLocal + "Fiscal32.exe";
      this.INIPath = AppConfig.AppLocal + "fiskal.ini";
      this.TextFile = AppConfig.AppLocal + "PF500.in";
      this._ComPortNumber = 1;
      this._Items = new List<Article>();
      this.cyrillic = Encoding.GetEncoding("windows-1251");
    }

    public PF550(string ComPort)
      : this()
    {
      this._ComPort = ComPort;
      this.CreateIniFile();
      if (File.Exists(this.AppPath))
        return;
      this.CreateExecutable();
    }

    public PF550(string ComPort, List<Article> stavki)
      : this(ComPort)
    {
      this.Stavki = stavki;
    }

    [ComVisible(true)]
    public void FiskalnaSmetka(PF550.PaidMode PaidType = PF550.PaidMode.VoGotovo)
    {
      if (this.Stavki.Count == 0)
        return;
      this.CreateFiskalnaPF550(PaidType);
      this.Run();
    }

    [ComVisible(true)]
    public void FiskalnaSmetka(List<Article> Stavki, PF550.PaidMode PaidType = PF550.PaidMode.VoGotovo)
    {
      this.Stavki = Stavki;
      this.FiskalnaSmetka(PaidType);
    }

    [ComVisible(true)]
    public void StornaSmetka(PF550.PaidMode PaidType = PF550.PaidMode.VoGotovo)
    {
      if (this.Stavki.Count == 0)
        return;
      this.CreateStornaPF550(PaidType);
      this.Run();
    }

    [ComVisible(true)]
    public void StornaSmetka(List<Article> Stavki, PF550.PaidMode PaidType = PF550.PaidMode.VoGotovo)
    {
      this.Stavki = Stavki;
      this.StornaSmetka(PaidType);
    }

    [ComVisible(true)]
    public void PodesuvajCas()
    {
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        char ch;
        if (PF550.LastCommand == 1)
        {
          ch = '#';
          PF550.LastCommand = 2;
        }
        else
        {
          ch = '$';
          PF550.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "=" + DateTime.Now.ToString("dd-MM-yy HH:MM:ss"));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void PaperFeed(int Lines = 2)
    {
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        char ch;
        if (PF550.LastCommand == 1)
        {
          ch = '#';
          PF550.LastCommand = 2;
        }
        else
        {
          ch = '$';
          PF550.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "," + Lines.ToString());
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void ZatvoriDen()
    {
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        char ch;
        if (PF550.LastCommand == 1)
        {
          ch = '#';
          PF550.LastCommand = 2;
        }
        else
        {
          ch = '$';
          PF550.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "E");
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void Izvestaj_X()
    {
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        char ch;
        if (PF550.LastCommand == 1)
        {
          ch = '#';
          PF550.LastCommand = 2;
        }
        else
        {
          ch = '$';
          PF550.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "E2");
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void SluzbenVnes(Decimal Amount)
    {
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        char ch;
        if (PF550.LastCommand == 1)
        {
          ch = '#';
          PF550.LastCommand = 2;
        }
        else
        {
          ch = '$';
          PF550.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "F" + this.FormatNumber(Amount, 2));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void DetalenPeriodicenIzvestaj(DateTime OdDatum, DateTime DoDatum)
    {
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        char ch;
        if (PF550.LastCommand == 1)
        {
          ch = '#';
          PF550.LastCommand = 2;
        }
        else
        {
          ch = '$';
          PF550.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "^" + OdDatum.ToString("ddMMyy") + "," + DoDatum.ToString("ddMMyy"));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void PeriodicenIzvestaj(DateTime OdDatum, DateTime DoDatum)
    {
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        char ch;
        if (PF550.LastCommand == 1)
        {
          ch = '#';
          PF550.LastCommand = 2;
        }
        else
        {
          ch = '$';
          PF550.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "O" + OdDatum.ToString("ddMMyy") + "," + DoDatum.ToString("ddMMyy"));
        streamWriter.Close();
      }
      this.Run();
    }

    [ComVisible(true)]
    public void Diagnostika()
    {
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        char ch;
        if (PF550.LastCommand == 1)
        {
          ch = '#';
          PF550.LastCommand = 2;
        }
        else
        {
          ch = '$';
          PF550.LastCommand = 1;
        }
        streamWriter.Write(Conversions.ToString(ch) + "G");
        streamWriter.Close();
      }
      this.Run();
    }

    private void CreateStornaPF550(PF550.PaidMode PaidMode = PF550.PaidMode.VoGotovo)
    {
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, this.cyrillic))
      {
        streamWriter.Write(" U1,000,1\r\n");
        int num = 0;
        foreach (var current in this.Stavki)
        {
            char ch = Strings.Chr((int) current.VAT);
            if (num % 2 == 0)
                streamWriter.Write("#1" + current.Name + "\t" + Conversions.ToString(ch) + this.FormatNumber(current.Price, 2) + "*" + this.FormatNumber(current.Quantity, 3) + "\r\n");
            else
                streamWriter.Write("$1" + current.Name + "\t" + Conversions.ToString(ch) + this.FormatNumber(current.Price, 2) + "*" + this.FormatNumber(current.Quantity, 3) + "\r\n");
            checked { ++num; }  
        }
        streamWriter.Write(" 5\t\r\n");
        streamWriter.Write("%V");
        streamWriter.Close();
      }
    }

    private void CreateFiskalnaPF550(PF550.PaidMode PaidMode = PF550.PaidMode.VoGotovo)
    {
      Encoding encoding = Encoding.GetEncoding("windows-1251");
      if (File.Exists(this.TextFile))
        File.Delete(this.TextFile);
      using (StreamWriter streamWriter = new StreamWriter(this.TextFile, false, encoding))
      {
        streamWriter.Write(" 01,0000,1\r\n");
        int num1 = 0;
        Decimal num2 = new Decimal();
        foreach (var current in this.Stavki)
        {
            char ch = Strings.Chr((int)current.VAT);
            if (num1 % 2 == 0)
                streamWriter.Write("#1" + current.Name + "\t" + Conversions.ToString(ch) + this.FormatNumber(current.Price, 0) + "*" + this.FormatNumber(current.Quantity, 3) + "\r\n");
            else
                streamWriter.Write("$1" + current.Name + "\t" + Conversions.ToString(ch) + this.FormatNumber(current.Price, 0) + "*" + this.FormatNumber(current.Quantity, 3) + "\r\n");
            num2 = Decimal.Add(num2, Decimal.Multiply(current.Price, current.Quantity));
            checked { ++num1; }
        }
        char ch1 = 'P';
        switch (PaidMode)
        {
          case PF550.PaidMode.VoGotovo:
            ch1 = 'P';
            break;
          case PF550.PaidMode.SoKarticka:
            ch1 = 'D';
            break;
          case PF550.PaidMode.SoKredit:
            ch1 = 'N';
            break;
        }
        streamWriter.Write(" 5\t" + Conversions.ToString(ch1) + this.FormatNumber(num2, 2) + "\r\n");
        streamWriter.Write("%8");
        streamWriter.Close();
      }
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
      VoGotovo = 1,
      SoKarticka = 2,
      SoKredit = 3,
    }
  }
}
