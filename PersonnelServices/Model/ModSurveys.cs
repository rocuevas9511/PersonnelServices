using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.Model
{
    public class ModSurveys : ModelBase
    {
        public static string MODEL_QUESTION
        {
            get { return KEY_QUESTION; }
        }

        public static string MODEL_RESPONSE
        {
            get { return KEY_RESPONSE; }
        }

        [BsonElement(KEY_QUESTION)]
        public string Question { get; set; }

        [BsonElement(KEY_RESPONSE)]
        public string Response { get; set; }
    }
}
