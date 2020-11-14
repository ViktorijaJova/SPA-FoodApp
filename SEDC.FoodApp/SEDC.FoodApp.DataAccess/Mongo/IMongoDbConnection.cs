using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.DataAccess.Mongo
{
    public interface IMongoDbConnection
    {
        string GetConnectionString();
        string GetDatabaseName();
        Task<string> GetConnectionStringAsync();
        Task<string> GetDatabaseNameAsync();
    }
}
