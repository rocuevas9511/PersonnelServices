using MongoDB.Driver;
using PersonnelServices.DAL.Interface;
using PersonnelServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.DAL
{
    public class MongoSentiments : MongoApiBase, IApiSentiments
    {
        private readonly IMongoCollection<ModSentiments> _apiKeysCollection;
        public MongoSentiments(IMongoCollection<ModSentiments> apiKeysCollection)
        {
            _apiKeysCollection = apiKeysCollection;
        }

        public Task<bool> InsertSentiment(ModSentiments sentiment)
        {
            try
            {
                _apiKeysCollection.InsertOne(sentiment);
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }
    }
}
