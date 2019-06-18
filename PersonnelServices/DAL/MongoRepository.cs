using MongoDB.Driver;
using PersonnelServices.DAL.Interface;
using PersonnelServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.DAL
{
    public class MongoRepository : IRepository
    {
        private readonly string DBNAME = "carebot";

        public MongoRepository(MongoClient mongoClient)
        {
            if (mongoClient == null)
            {
                throw new ArgumentNullException(nameof(mongoClient), "The Mongo Client must not be null");
            }

            ApiTest = new MongoTest(mongoClient.GetDatabase(DBNAME).GetCollection<ModTest>(API_TEST));
        }

        private readonly string API_TEST = "test";

        public IApiTest ApiTest { get; private set; }
    }
}
