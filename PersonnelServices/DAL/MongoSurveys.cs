using MongoDB.Bson;
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

        public async Task<ModSurveys> GetSurvey(string lang)
        {
            IEnumerable<ModSurveys> result = await _apiKeysCollection.Aggregate<ModSurveys>(GetPipelineSurveys(lang)).ToListAsync();

            return result.ElementAt<ModSurveys>( new Random().Next(0, result.Count() -1) );
        }

        public Task<string> InsertSurvey(ModSurveys survey)
        {
            _apiKeysCollection.InsertOne(survey);

            return Task.FromResult(survey?.Id.ToString());
        }

        #region PIPELINE

        private BsonDocument[] GetPipelineSurveys(string lang)
        {
            return new[] {
                GetPipelineMatch(new BsonDocument
                {
                    { ModSurveys.MODEL_LANGUAGE, lang }
                })
            };
        }

        #endregion
    }
}
