using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.DataAccess.Mongo
{
    public abstract class MongoDbRepository<T> : IMongoDbRepository<T>
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        protected MongoDbRepository(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        protected MongoDbRepository(IMongoDbConnection mongoDbConnection)
        {
            _client = new MongoClient(mongoDbConnection.GetConnectionString());
            _database = _client.GetDatabase(mongoDbConnection.GetDatabaseName());
        }

        public IMongoQueryable<T> Collection { get { return GetCollectionAsQueryAble(); } }
        public IMongoCollection<T> MongoCollection { get { return GetCollection(); } }

        protected virtual string GetCollectionName()
        {
            return typeof(T).Name.ToLower() + "s";
        }

        protected IMongoCollection<T> GetCollection()
        {
            return _database.GetCollection<T>(GetCollectionName());
        }

        protected IMongoQueryable<T> GetCollectionAsQueryAble()
        {
            return _database.GetCollection<T>(GetCollectionName()).AsQueryable();
        }

        protected IMongoQueryable<TModel> GetCollectionAsQueryAble<TModel>()
        {
            return _database.GetCollection<TModel>(GetCollectionName()).AsQueryable();
        }

        public IMongoQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return GetCollectionAsQueryAble().Where(predicate);
        }

        public async Task InsertAsync(T record)
        {
            var collection = GetCollection();
            await collection.InsertOneAsync(record);
        }

        public void Insert(T record)
        {
            var collection = GetCollection();
            collection.InsertOne(record);
        }

        public Task InsertManyAsync(IEnumerable<T> records)
        {
            return GetCollection().InsertManyAsync(records);
        }

        public UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection();
            return collection.UpdateOne(filter, update);
        }

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection();
            return collection.UpdateOne(filter, update);
        }
        public async Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection();
            return await collection.UpdateOneAsync<T>(filter, update);
        }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection();
            return collection.UpdateOneAsync(filter, update);
        }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            var collection = GetCollection();
            return collection.UpdateOneAsync(filter, update, updateOptions);
        }

        public UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection();
            return collection.UpdateMany(filter, update);
        }

        public UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection();
            return collection.UpdateMany(filter, update);
        }

        public async Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection();
            return await collection.UpdateManyAsync(filter, update);
        }

        public async Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection();
            return await collection.UpdateManyAsync<T>(filter, update);

        }
        protected async Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter)
        {
            try
            {
                var collection = GetCollection();
                var cursor = await collection.FindAsync(filter);
                var result = await cursor.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting data from collection." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }

        protected IEnumerable<T> Find(FilterDefinition<T> filter, int size, int offset)
        {
            var collection = GetCollection();
            if (size <= 0)
            {
                size = 1000;
            }
            return collection.Find(filter).Skip(offset).Limit(size).ToEnumerable();
        }

        protected IEnumerable<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort, int size, int offset)
        {
            var collection = GetCollection();
            if (size <= 0)
            {
                size = 1000;
            }
            return collection.Find(filter).Sort(sort).Skip(offset).Limit(size).ToEnumerable();
        }

        protected List<U> Aggregate<U>(PipelineDefinition<T, U> pipeline)
        {
            var collection = GetCollection();
            return collection.Aggregate(pipeline).ToList();
        }

        protected long Count(FilterDefinition<T> filter)
        {
            var collection = GetCollection();
            return collection.Find(filter).CountDocuments();
        }

        protected int CountInt32(FilterDefinition<T> filter)
        {
            var collection = GetCollection();
            var longCount = collection.Find(filter).CountDocuments();
            return longCount > int.MaxValue ? int.MaxValue : Convert.ToInt32(longCount);
        }

        protected IEnumerable<T> Find(FilterDefinition<T> filter)
        {
            var collection = GetCollection();
            return collection.Find(filter).ToEnumerable();
        }

        protected DeleteResult DeleteOne(FilterDefinition<T> filter)
        {
            var collection = this.GetCollection();
            var deleteResult = collection.DeleteOne(filter);
            return deleteResult;
        }

        protected async Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter)
        {
            var collection = this.GetCollection();
            var deleteResult = await collection.DeleteOneAsync(filter);
            return deleteResult;
        }

        protected Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
        {
            return GetCollection().DeleteManyAsync(filter);
        }

        protected Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter) => GetCollection().DeleteManyAsync(filter);

    }
}
