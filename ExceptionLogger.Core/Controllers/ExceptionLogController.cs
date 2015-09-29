using ExceptionLogger.Core.Abstracts;
using ExceptionLogger.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLogger.Core.Controllers
{
    class ExceptionLogController : IController<ExceptionLog>
    {
        private static IMongoClient client = null;
        private IMongoDatabase database = null;
        private IMongoCollection<ExceptionLog> collection = null;

        public ExceptionLogController()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("exceptionlogsdb");
            collection = database.GetCollection<ExceptionLog>("exceptionlogs");
        }

        //READ
        public IEnumerable<ExceptionLog> Get()
        {
            try
            {
                //collection = database.GetCollection<ExceptionLog>("exceptionlogs");
                return collection.Find(new BsonDocument()).ToListAsync().Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ExceptionLog Get(string id)
        {
            try
            {
                var filter = Builders<ExceptionLog>.Filter.Eq("Id", new ObjectId(id));
                return collection.Find(filter).FirstAsync().Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //UPDATE
        public bool Add(ExceptionLog entity)
        {
            try
            {
                this.collection.InsertOneAsync(entity);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var filter = Builders<ExceptionLog>.Filter.Eq("Id", new ObjectId(id));
                collection.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(ExceptionLog entity)
        {
            try
            {
                var filter = Builders<ExceptionLog>.Filter.Eq("Id", new ObjectId(entity.Id.ToString()));
                var update = Builders<ExceptionLog>.Update.Set("CID", "Updated");

                collection.UpdateOneAsync(filter, update);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
