using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqDataModel.Models
{
    public sealed class DataRepository : IDataRepository, IDisposable
    {
        #region Private Members

        private MikaviDBDataContext _dataContext;

        #endregion
        
        #region Constructors

        public ICacheProvider Cache { get; set; }
        //public DataRepository() : this(new DefaultCacheProvider()) { }
        //public DataRepository() : this(new IbankDBDataContext()) { }
        public DataRepository(string connectionString) : this(new MikaviDBDataContext(connectionString)) { }
        public DataRepository(MikaviDBDataContext dataContext)
        {
            this._dataContext = dataContext;
            this.Cache = new DefaultCacheProvider();
            //this._dataContext = new IbankDBDataContext();
            //this.Cache = cacheProvider;
        }

        #endregion

        public void Dispose()
        {
            //Debug.WriteLine("Message Generator is being disposed");
            _dataContext.Dispose();
        }

        /// <summary>
        /// Start Transaction
        /// </summary>
        /// <returns></returns>
        public DbTransaction BeginTransaction()
        {
            if (_dataContext.Connection.State != ConnectionState.Open)
            {
                _dataContext.Connection.Open();
            }
            //_dataContext.Transaction = _dataContext.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            return _dataContext.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Start Transaction
        /// </summary>
        /// <returns></returns>
        public DbTransaction CommitTransaction()
        {
            if (_dataContext.Connection.State != ConnectionState.Open)
            {
                _dataContext.Connection.Open();
            }
            //_dataContext.Transaction = _dataContext.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            return _dataContext.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Start Transaction
        /// </summary>
        /// <returns></returns>
        public DbTransaction RollbackTransaction()
        {
            if (_dataContext.Connection.State != ConnectionState.Open)
            {
                _dataContext.Connection.Open();
            }
            //_dataContext.Transaction = _dataContext.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            return _dataContext.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public IList<GetArticlesResult> GetArticles(int? articleId, string name, string barcode, ref int? totalArticles, ref int? articlesWithStock, ref decimal? cumulativeAmount)
        {
            return _dataContext.GetArticles(articleId, name, barcode, ref totalArticles, ref articlesWithStock, ref cumulativeAmount).ToList();
        }
        public DataTable GetArticlesTable(int? articleId, string name, string barcode, ref int? totalArticles, ref int? articlesWithStock, ref decimal? cumulativeAmount)
        {
            var result = _dataContext.GetArticles(articleId, name, barcode, ref totalArticles, ref articlesWithStock, ref cumulativeAmount).ToList();
            return LinqQueryToDataTable(result);
        }
        public int AddArticle(int pLU, string name, decimal price, string bar_code, decimal stock)
        {
            return _dataContext.AddArticle(pLU, name, price, bar_code, stock);
        }
        public int AddArticle(DataRow dataRow)
        {
            return _dataContext.AddArticle(
                    int.Parse(dataRow["PLU"].ToString()),
                    dataRow["Name"].ToString(),
                    decimal.Parse(dataRow["Price"].ToString()),
                    dataRow["Bar_code"].ToString(),
                    decimal.Parse(dataRow["Stock"].ToString())
                );
        }
        
        public static DataTable LinqQueryToDataTable(IEnumerable<dynamic> v)
        {
            //We really want to know if there is any data at all
            var firstRecord = v.FirstOrDefault();
            if (firstRecord == null)
                return null;

            /*Okay, we have some data. Time to work.*/

            //So dear record, what do you have?
            PropertyInfo[] infos = firstRecord.GetType().GetProperties();

            //Our table should have the columns to support the properties
            DataTable table = new DataTable();

            //Add, add, add the columns
            foreach (var info in infos)
            {

                Type propType = info.PropertyType;

                if (propType.IsGenericType
                    && propType.GetGenericTypeDefinition() == typeof(Nullable<>)) //Nullable types should be handled too
                {
                    table.Columns.Add(info.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
            }

            //Hmm... we are done with the columns. Let's begin with rows now.
            DataRow row;

            foreach (var record in v)
            {
                row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row[i] = infos[i].GetValue(record, null) != null ? infos[i].GetValue(record, null) : DBNull.Value;
                }

                table.Rows.Add(row);
            }

            //Table is ready to serve.
            table.AcceptChanges();

            return table;
        }

        public DataTable LINQToDataTable<T>(IList<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}
