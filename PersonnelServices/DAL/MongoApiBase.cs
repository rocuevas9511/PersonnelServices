using MongoDB.Bson;

namespace PersonnelServices.DAL
{
    public class MongoApiBase
    {
        protected readonly string MONGO_AGGREGATE_MATCH = "$match";
        protected readonly string MONGO_AGGREGATE_GROUP = "$group";
        protected readonly string MONGO_AGGREGATE_PROJECT = "$project";

        protected readonly string MONGO_FIND_OR = "$or";
        protected readonly string MONGO_FIND_REGEX = "$regex";
        protected readonly string MONGO_FIND_OPTIONS = "$options";


        protected virtual BsonDocument GetPipelineMatch(BsonDocument elementsMatch)
        {
            return GetPipeline(MONGO_AGGREGATE_MATCH, elementsMatch);
        }

        protected virtual BsonDocument GetPipelineGroup(BsonDocument elementsGroup)
        {
            return GetPipeline(MONGO_AGGREGATE_GROUP, elementsGroup);
        }

        protected virtual BsonDocument GetPipelineProject(BsonDocument elementsProject)
        {
            return GetPipeline(MONGO_AGGREGATE_PROJECT, elementsProject);
        }

        protected virtual BsonDocument GetPipeline(string function, BsonDocument elements)
        {
            var pipeline = new BsonDocument
            {
                {
                    function,
                    elements
                }
            };

            return pipeline;
        }

        protected virtual BsonDocument GetPipeline(string function, BsonArray elements)
        {
            var pipeline = new BsonDocument
            {
                {
                    function,
                    elements
                }
            };

            return pipeline;
        }
    }
}
