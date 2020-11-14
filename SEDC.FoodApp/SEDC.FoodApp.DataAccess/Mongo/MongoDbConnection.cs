using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.DataAccess.Mongo
{
    public class MongoDbConnection : IMongoDbConnection
    {
        private readonly string _connectionString = string.Empty;
        private readonly string _databaseName = string.Empty;

        private readonly static object SyncConnectionString = new object();
        private readonly static object SyncDatabaseName = new object();

        public string ConnectionString 
        {
            get 
            {
                lock (SyncConnectionString) 
                {
                    return _connectionString;
                }
            }
        }

        public string DatabaseName 
        {
            get 
            {
                lock (SyncDatabaseName) 
                {
                    return _databaseName;
                }
            }
        }

        public MongoDbConnection(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public string GetConnectionString() => ConnectionString;
        public string GetDatabaseName() => DatabaseName;

        public Task<string> GetConnectionStringAsync() => Task.FromResult(ConnectionString);
        public Task<string> GetDatabaseNameAsync() => Task.FromResult(DatabaseName);
    }
}
