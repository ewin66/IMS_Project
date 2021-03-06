﻿using System;
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
        IList<GetReportResult> GetReport(string fromDate, string toDate, int customerId, bool recipientPrinted, ref decimal? cumulativeAmount, ref decimal? cumulativeProfit);
        IList<GetOrderDetailsResult> GetOrderDetails(string fromDate, string toDate, int customerId, bool? recipientPrinted, ref decimal? cumulativeAmount);
        IList<GetArticlesResult> GetArticles(int? articleId, string name, string barcode, ref int? totalArticles, ref int? articlesWithStock, ref decimal? cumulativeAmount);
        DataTable GetArticlesTable(int? articleId, string name, string barcode, ref int? totalArticles, ref int? articlesWithStock, ref decimal? cumulativeAmount);
        DataTable GetProductsTable(int? productId, string ProductName, string Barcode, ref int? totalArticles, ref int? articlesWithStock, ref decimal? cumulativeAmount);
        int AddArticle(int pLU, string name, decimal price, string bar_code, decimal stock);
        int AddArticle(DataRow dataRow);
        int AddProduct(int productId, int categoryId, int suplierId, string productName, int quantityPerUnit, decimal unitPurchasePrice, decimal unitPrice, decimal unitsInStock, decimal reorderLevel, bool isDomestic, bool discontinued, string barCode1, string barCode2, string barCode3, string barCode4);
        int AddProduct(DataRow dataRow);
        ISingleResult<AddOrderResult> AddOrder(int customerId, int? employeeId, int orderStatusId, string orderNumber, string comment);
        ISingleResult<AddOrderResult> AddOrder(int orderStatusId, string orderNumber, string comment);
        int AddOrderDetails(int orderId, int productId, string productName, decimal quantity, decimal unitPrice, decimal unitPurchasePrice, decimal discount);
        int DeleteOrderProduct(int orderDetailsId);
        ISingleResult<UpdateOrderResult> UpdateOrder(int orderId, bool receiptPrinted);
        ISingleResult<GetTodayTurnoverResult> GetTodayTurnover();

        ISingleResult<AddCategoryResult> AddCategory(string categoryName);
        IList<GetCategoriesResult> GetCategories();
    }
}
