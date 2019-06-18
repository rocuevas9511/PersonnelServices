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
            ApiSurveys = new MongoSurveys(mongoClient.GetDatabase(DBNAME).GetCollection<ModSurveys>(API_SURVEYS));
            ApiEmotion = new MongoEmotion(mongoClient.GetDatabase(DBNAME).GetCollection<ModEmotion>(API_EMOTIONS));
        }

        private readonly string API_TEST = "test";
        public IApiTest ApiTest { get; private set; }

        private readonly string API_SURVEYS = "surveys";
        public IApiSurveys ApiSurveys { get; private set; }

        private readonly string API_EMOTIONS = "emotions";
        public IEmotionsDAL ApiEmotion { get; private set; }
    }
}
