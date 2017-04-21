using IBatisNet.Common.Exceptions;
using IBatisNet.DataMapper;
using AzureWebApp.Models;
using AzureWebApp.Util;
using System;


namespace AzureWebApp.Dao
{
    public abstract class BaseSqlMapDao
    {

        protected static ISqlMapper mapper = Mapper.Instance();

        static BaseSqlMapDao()
        {
            AbstractConfigurationSettings settings = LoginManager.GetSettings();
            // 環境に応じて接続文字列を変更
            mapper.DataSource.ConnectionString = settings.DBConnectionString;


            //DomDaoManagerBuilder builder = new DomDaoManagerBuilder();
            // configure dao.config
            //builder.Configure();

            //******* 問題 ************//
            // 例によって、xml ファイルのロードはさまざまなパターンでエラーが出る。
            // 1. providers.config が存在しない。-> providers.config を用意する。
            // 2. dao.config の database 要素配下に provider 要素が存在しない -> 作成した provider.config を参照する。
            //      <database>
            //        <provider name="sqlServer4.0" />
            // 3. sqlMap のファイルの記述がおかしい。-> SQL の定義とかがおかしい。今回はパラメーターを #param# でなく #param としていた。
            // 4. あいまいな一致が見つかりましたエラー -> castle.dll のバグっぽい。新しめのを使う。
            // 5. dao.config の provider 要素の参照先が違うランタイム -> ランタイムをコンパイルしているのに直す。
            //     誤 :  <provider name="sqlServer2.0" />
            //     正 :  <provider name="sqlServer4.0" />
            // 上記は Providers.config に記載された 2 つのプロバイダー
            // しかし、これらをクリアしても動かない。まともに動く気配がないので、DataAccess は使わないのが吉。
        }


        //Insertの実行
        protected int ExecuteInsert(string statementName, object parameterObject)
        {
            int result = 0;
            try
            {
                mapper.BeginTransaction();
                result = (int) mapper.Insert(statementName, parameterObject);
                mapper.CommitTransaction();
            }
            catch (Exception e)
            {
                mapper.RollBackTransaction();
                throw new IBatisNetException("INSERT実行エラー '"
                  + statementName + "'  原因： " + e.Message, e);
            }
            return result;
        }

        protected int ExecuteUpdate(string statementName, object parameterObject)
        {
            int result = 0;
            try
            {
                mapper.BeginTransaction();
                result = (int)mapper.Update(statementName, parameterObject);
                mapper.CommitTransaction();
            }
            catch (Exception e)
            {
                mapper.RollBackTransaction();
                throw new IBatisNetException("Update T実行エラー '"
                  + statementName + "'  原因： " + e.Message, e);
            }
            return result;
        }

        protected T ExecuteQueryForObject<T>(string statementName, T parameterObject)
        {
            return this.ExecuteQueryForObject<T, T>(statementName, parameterObject);
        }

        //SelectObject(1件)の実行    
        protected T2 ExecuteQueryForObject<T1, T2> (string statementName, T1 parameterObject)
        {
            try
            {
                return (T2) mapper.QueryForObject(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("SELECT実行エラー '"
                  + statementName + "'   原因： " + e.Message, e);
            }
        }
    }
}