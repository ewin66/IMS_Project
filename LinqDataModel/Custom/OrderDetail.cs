using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LinqDataModel
{
    public partial class OrderDetail
    {
        //[Description("Podatoci za promenata dokolku e napravena vrz objektot: Sto e promeneto, Na koj del se odnesuva, Kakva promena")]

        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
