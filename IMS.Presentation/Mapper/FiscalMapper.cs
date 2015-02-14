using IMS.Fiscal;
using LinqDataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viktor.IMS.Presentation.Mapper
{
    public class FiscalMapper
    {
        public static List<Article> PrepareFiscalReceipt(List<Product> orderDetails)
        {
            var fiscalReceipt = new List<Article>();
            foreach (var product in orderDetails)
            {
                var article = new Article();
                article.Name = product.ProductName;
                article.VAT = VATgroup.Г;//bez ddv
                article.Quantity = product.Quantity;
                article.Price = (decimal)product.UnitPrice;
                fiscalReceipt.Add(article);
            }
            return fiscalReceipt;
        }
    }
}
