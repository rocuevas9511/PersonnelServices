using MongoDB.Bson.Serialization.Attributes;

namespace PersonnelServices.Model
{
    public class ModTest : ModelBase
    {
        public static string MODEL_NAME
        {
            get { return KEY_NAME; }
        }

        public static string MODEL_MESSAGE
        {
            get { return KEY_MESSAGE; }
        }

        [BsonElement(KEY_NAME)]
        public string Name { get; set; }

        [BsonElement(KEY_MESSAGE)]
        public string Message { get; set; }
    }
}
