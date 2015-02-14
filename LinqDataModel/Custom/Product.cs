using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LinqDataModel
{
    public partial class Product
    {
        //[Description("Podatoci za promenata dokolku e napravena vrz objektot: Sto e promeneto, Na koj del se odnesuva, Kakva promena")]

        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal? Price { get; set; }
    }
}
