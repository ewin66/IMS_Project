// Decompiled with JetBrains decompiler
// Type: Bujar.Fiscal.Article
// Assembly: Bujar.Fiscal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F67EA74-8FA1-4626-BF9D-4F6BED24CDA7
// Assembly location: G:\GitHub\IMS_Project\Library\_Fiskalni\Bujar.Fiscal.dll

using System;

namespace IMS.Fiscal
{
  public sealed class Article
  {
    private string _Name;
    private VATgroup _VAT;
    private Decimal _Quantity;
    private Decimal _Price;
    private Boolean _IsDomestic;

    public string Name
    {
      get
      {
        return this._Name;
      }
      set
      {
        this._Name = value;
      }
    }

    public VATgroup VAT
    {
      get
      {
        return this._VAT;
      }
      set
      {
        this._VAT = value;
      }
    }

    public Decimal Quantity
    {
      get
      {
          return this._Quantity;
      }
      set
      {
          this._Quantity = value;
      }
    }

    public Decimal Price
    {
        get
        {
            return this._Price;
        }
        set
        {
            this._Price = value;
        }
    }
    public Boolean IsDomestic
    {
        get
        {
            return this._IsDomestic;
        }
        set
        {
            this._IsDomestic = value;
        }
    }

    public Article()
    {
      this.Name = "";
      this.VAT = VATgroup.А;
      this.Quantity = Decimal.Zero;
      this.Price = Decimal.Zero;
      this.IsDomestic = true;
    }

    public Article(string name, VATgroup ddv, Decimal quantity, Decimal price, Boolean isDomestic)
      : this()
    {
      this.Name = name;
      this.VAT = ddv;
      this.Quantity = quantity;
      this.Price = price;
      this.IsDomestic = isDomestic;
    }
  }
}
