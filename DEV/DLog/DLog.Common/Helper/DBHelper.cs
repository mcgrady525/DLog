using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DLog.Entity;

namespace DLog.Common.Helper
{
    /// <summary>
    /// DB操作帮助类
    /// </summary>
    public static class DBHelper
    {
        public static void BulkInsert<T>(SqlConnection conn, string tableName, IList<T> list)
        {
            using (var bulkCopy = new SqlBulkCopy(conn))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = tableName;

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T), new Attribute[] { new DatabaseTableColumnAttribute() })
                    //Dirty hack to make sure we only have system data types 
                    //i.e. filter out the relationships/collections
                    .Cast<PropertyDescriptor>()
                    .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                    .ToArray();

                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }

                bulkCopy.WriteToServer(table);
            }
        }


        /// <summary>
        ///Ticket.Common.DBHelper.NoLockInvokeDB(() =>
        ///{
        ///    using (var db = new TicketDB())
        ///    {
        ///        //...
        ///    }
        ///});
        /// </summary>
        /// <param name="action"></param>
        public static void NoLockInvokeDB(Action action)
        {
            InvokeDB(action, System.Transactions.IsolationLevel.ReadUncommitted);
        }

        /// <summary>
        ///Ticket.Common.DBHelper.InvokeDBWithLock(() =>
        ///{
        ///    using (var db = new TicketDB())
        ///    {
        ///        //...
        ///    }
        ///});
        /// </summary>
        /// <param name="action"></param>
        public static void InvokeDBWithLock(Action action)
        {
            InvokeDB(action, System.Transactions.IsolationLevel.ReadCommitted);
        }

        private static void InvokeDB(Action action, System.Transactions.IsolationLevel isolationLevel)
        {
            var transactionOptions = new System.Transactions.TransactionOptions();
            transactionOptions.IsolationLevel = isolationLevel;
            using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
            {
                try
                {
                    action();
                }
                finally
                {
                    transactionScope.Complete();
                }
            }
        }
    }
}
