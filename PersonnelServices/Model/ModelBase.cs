using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonnelServices.Model
{
    public class ModelBase : KeyModelBase
    {
        public static string MODEL_ID
        {
            get { return KEY_ID; }
        }

        [BsonElement(KEY_ID)]
        public ObjectId Id { get; set; }

    }
}
