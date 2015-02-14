using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDataModel.Models
{
    public interface IDataRepository
    {
        void Dispose();
        DbTransaction BeginTransaction();
        DbTransaction CommitTransaction();
        DbTransaction RollbackTransaction();

        Product GetProduct(int? productId, string productName, string barCode);
        IList<GetProductsResult> GetProducts(int? productId, string productName, string barCode, ref int? totalArticles, ref int? articlesWithStock, ref decimal? cumulativeAmount);
        IList<GetArticlesResult> GetArticles(int? articleId, string name, string barcode, ref int? totalArticles, ref int? articlesWithStock, ref decimal? cumulativeAmount);
        DataTable GetArticlesTable(int? articleId, string name, string barcode, ref int? totalArticles, ref int? articlesWithStock, ref decimal? cumulativeAmount);
        int AddArticle(int pLU, string name, decimal price, string bar_code, decimal stock);
        int AddArticle(DataRow dataRow);
    }
}
