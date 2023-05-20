using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotnetAPI
{
    class DataContextDapper
    {
        // IConfiguration gets the connection string from appsettings.json
        private readonly IConfiguration _config;

        // the below is a CONSTRUCTOR using underscore marking a PRIVATE field (it's convention not a language feature)
        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }
        // Method that will load all our data
        public IEnumerable<T> LoadData<T> (string sql)
        {
            // Create new connection using ConnectionString from appsettings.json
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }

        // Method that loads single piece of data
        public T LoadDataSingle<T> (string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }
        // Method that tells us (bool) if the query was executed
        public bool ExecuteSql(string sql)
       {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql) > 0;
        }
        // tells us how many rows were affected
        public int ExecuteSqlWithRowCount(string sql)
       {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql);
        }

    }
}