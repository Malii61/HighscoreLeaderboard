using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Caching;
namespace InternProject
{
    class Leaderboard
    {
        public static MemoryCache mCache = MemoryCache.Default;

        // ADU -> AddDeleteUpdate
        public static void ADU(string sql, SqlCommand command)
        {
            if (Database.connection.State != ConnectionState.Open)
                Database.connection.Open();

            try
            {
                command.Connection = Database.connection;
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                Database.connection.Close();
            }
        }

        public static void ExecuteSql(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            Leaderboard.ADU(sql, cmd);
        }
        public static bool CheckUserNameUnique(string userName)
        {
            using (DataTable userDt = GetUserDt(string.Format("Select * FROM Users where user_name = '{0}'", userName)))
            {
                return (userDt.Rows.Count == 0);
            }
        }
        /*
        public static CacheItem GetDataTableFromCacheOrDatabase(string sql)
        {
            bool isEmpty = mCache.GetCount() == 0;

            if (isEmpty)
            {
                mCache.Add("Users", GetUserDt(sql), DateTimeOffset.Now.AddSeconds(300));
            }
            CacheItem cItemTable = mCache.GetCacheItem("Users");
            return cItemTable;
        }
        */


        public static DataTable GetUserDt(string sql)
        {
            DataTable tbl = new DataTable();
            SqlDataAdapter adtr = new SqlDataAdapter(sql, Database.connection);
            adtr.Fill(tbl);   
            return tbl;
        }
    }
}
