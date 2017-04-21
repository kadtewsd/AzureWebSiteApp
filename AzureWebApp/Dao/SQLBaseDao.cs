using AzureWebApp.Dao;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace AzureWebApp.Dao
{
    public abstract class SQLBaseDao<T1, T2> : IBaseDao <T1, T2> 
    {
        private SqlConnection con = null;
        protected SqlCommand Command { get; set; }

        protected abstract T2 ResultValue
        {
            get; set;
        }

        public SQLBaseDao()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["webappDB"].ConnectionString;
            con = new System.Data.SqlClient.SqlConnection(connStr);
        }

        public T2 ExecuteSQL(T1 input)
        {
            SqlTransaction tran = null;
            T2 result = default(T2);
            try
            {
                con.Open();
                tran = con.BeginTransaction();

                this.Command = con.CreateCommand();
                Command.Connection = con;
                Command.Transaction = tran;
                result = this.Execute(input);
                tran.Commit();
            } catch (Exception e)
            {
                if (tran.Connection != null)
                {
                    tran.Rollback();
                }
                throw e;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                
            }
            return result;
        }

        protected abstract T2 Execute(T1 input);


        protected T2 Select()
        {
            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = this.Command;
            da.Fill(ds, "publishers");

            DataTable tbl = ds.Tables[0];

            foreach (DataRow row in tbl.Rows)
            {
                foreach (DataColumn col in tbl.Columns)
                {
                    foreach (PropertyInfo prop in this.ResultValue.GetType().GetProperties())
                    {
                        if (prop.Name.ToUpper() != col.ColumnName.ToUpper())
                        {
                            continue;
                        }
                        object value = row[col.ColumnName];

                        prop.SetValue(this.ResultValue, value);
                    }
                }
            }
            return this.ResultValue;
        }
    }
}