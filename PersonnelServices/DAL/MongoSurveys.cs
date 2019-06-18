using MongoDB.Driver;
using PersonnelServices.DAL.Interface;
using PersonnelServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.DAL
{
    public class MongoSurveys : MongoApiBase, IApiSurveys
    {
        private readonly IMongoCollection<ModSurveys> _apiKeysCollection;

        public MongoSurveys(IMongoCollection<ModSurveys> apiKeysCollection)
        {
            _apiKeysCollection = apiKeysCollection;
        }

        public async Task<IEnumerable<ModSurveys>> GetSurveyAsync()
        {
            return await _apiKeysCollection.Find(a => true)
                                           .ToListAsync();
        }

        public Task<string> InsertSurvey(ModSurveys survey)
        {
            _apiKeysCollection.InsertOne(survey);

            return Task.FromResult(survey?.Id.ToString());
        }
    }
}
