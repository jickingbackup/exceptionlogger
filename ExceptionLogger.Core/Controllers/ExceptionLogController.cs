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

        public ExceptionLogController(string connectionString)
        {
            client = new MongoClient(connectionString); //("mongodb://localhost:27017");
            database = client.GetDatabase("exceptionlogsdb");
            collection = database.GetCollection<ExceptionLog>("exceptionlogs");

            //seed
            if(collection.CountAsync(new BsonDocument()).Result== 0)
            {
                this.Add(new ExceptionLog() { Browser = "Firefox", Date = DateTime.Now, CID = "254533", Exception = "BENJIE GAY was detected on code.", IP = "neverhost", Is404Error = false, Referer = "http://google.porn", Server = "megatron", StackTrace = "im sooo lost.", URL = "http://google.porn/benjie" });
                this.Add(new ExceptionLog() { Browser = "Chrome", Date = DateTime.Now.AddDays(1), CID = "334569", Exception = "CHARLES GAY was detected on code.", IP = "neverhost", Is404Error = false, Referer = "http://google.porn", Server = "PHILOPHP", StackTrace = "im sooo lost.", URL = "http://google.porn/CHARLES" });
                this.Add(new ExceptionLog() { Browser = "IE", Date = DateTime.Now.AddDays(2), CID = "66666", Exception = "PHILO GAY was detected on code.", IP = "neverhost", Is404Error = false, Referer = "http://google.porn", Server = "OPTIMUS", StackTrace = "im sooo lost.", URL = "http://google.porn/PHILO" });

                this.Add(new ExceptionLog() { Browser = "Firefox", Date = DateTime.Now, CID = "254533", Exception = "Pubic hair was not foud.", IP = "neverhost", Is404Error = true, Referer = "http://google.porn", Server = "megatron", StackTrace = "im sooo lost.", URL = "http://google.porn/benjie" });
                this.Add(new ExceptionLog() { Browser = "Chrome", Date = DateTime.Now.AddDays(1), CID = "334569", Exception = "Condom was not foud.", IP = "neverhost", Is404Error = true, Referer = "http://google.porn", Server = "PHILOPHP", StackTrace = "im sooo lost.", URL = "http://google.porn/CHARLES" });
                this.Add(new ExceptionLog() { Browser = "IE", Date = DateTime.Now.AddDays(2), CID = "66666", Exception = "Heart was not foud.", IP = "neverhost", Is404Error = true, Referer = "http://google.porn", Server = "OPTIMUS", StackTrace = "im sooo lost.", URL = "http://google.porn/PHILO" });
            }
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
