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
    public class MongoTest : MongoApiBase, IApiTest
    {
        private readonly IMongoCollection<ModTest> _apiKeysCollection;

        public MongoTest(IMongoCollection<ModTest> apiKeysCollection)
        {
            _apiKeysCollection = apiKeysCollection;
        }

        public async Task<IEnumerable<ModTest>> GetTestsAsync()
        {
            return await _apiKeysCollection.Find(a => true)
                                           .ToListAsync();
        }

        #region PIPELINES



        #endregion
    }
}
