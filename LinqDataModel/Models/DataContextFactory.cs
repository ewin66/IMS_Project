using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using System.Data.Linq;

namespace LinqDataModel.Models
{
    /// <summary>
    /// This class provides several static methods for loading DataContext objects 
    /// in a variety of ways. You can load the data context as normal one new instance
    /// at a time, or you can choose to use one of the scoped factory methods that
    /// can scope the DataContext to a WebRequest or a Thread context (in a WinForm app
    /// for example).
    /// 
    /// Using scoped variants can be more efficient in some scenarios and allows passing
    /// a DataContext across multiple otherwise unrelated components so that the change
    /// context can be shared. 
    /// </summary>
    public class DataContextFactory
    {
        /// <summary>
        /// Creates a new Data Context for a specific DataContext type
        /// 
        /// Provided merely for compleness sake here - same as new YourDataContext()
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <returns></returns>
        public static TDataContext GetDataContext<TDataContext>()
                where TDataContext : DataContext, new()
        {
            return (TDataContext)Activator.CreateInstance<TDataContext>();
        }

        /// <summary>
        /// Creates a new Data Context for a specific DataContext type with a connection string
        /// 
        /// Provided merely for compleness sake here - same as new YourDataContext()
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static TDataContext GetDataContext<TDataContext>(string connectionString)
                where TDataContext : DataContext, new()
        {
            Type t = typeof(TDataContext);
            return (TDataContext)Activator.CreateInstance(t, connectionString);
        }



        


        /// <summary>
        /// Creates a Thread Scoped DataContext object that can be reused.
        /// The DataContext is stored in Thread local storage.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TDataContext GetThreadScopedDataContext<TDataContext>()
                                   where TDataContext : DataContext, new()
        {
            return (TDataContext)GetThreadScopedDataContextInternal(typeof(TDataContext), null, null);
        }


        /// <summary>
        /// Creates a Thread Scoped DataContext object that can be reused.
        /// The DataContext is stored in Thread local storage.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TDataContext GetThreadScopedDataContext<TDataContext>(string key)
                                   where TDataContext : DataContext, new()
        {
            return (TDataContext)GetThreadScopedDataContextInternal(typeof(TDataContext), key, null);
        }


        /// <summary>
        /// Creates a Thread Scoped DataContext object that can be reused.
        /// The DataContext is stored in Thread local storage.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        static object GetThreadScopedDataContextInternal(Type type, string key, string ConnectionString)
        {
            if (key == null)
                key = "__WRSCDC_" + Thread.CurrentContext.ContextID.ToString();

            LocalDataStoreSlot threadData = Thread.GetNamedDataSlot(key);
            object context = null;
            if (threadData != null)
                context = Thread.GetData(threadData);

            if (context == null)
            {
                if (ConnectionString == null)
                    context = Activator.CreateInstance(type);
                else
                    context = Activator.CreateInstance(type, ConnectionString);

                if (context != null)
                {
                    if (threadData == null)
                        threadData = Thread.AllocateNamedDataSlot(key);

                    Thread.SetData(threadData, context);
                }
            }

            return context;
        }

    }
}
