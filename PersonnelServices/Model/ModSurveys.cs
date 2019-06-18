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

        public static string MODEL_LANGUAGE
        {
            get { return KEY_LANGUAGE; }
        }

        [BsonElement(KEY_QUESTION)]
        public string Question { get; set; }

        [BsonElement(KEY_RESPONSE)]
        public string Response { get; set; }

        [BsonElement(KEY_LANGUAGE)]
        public string Language { get; set; }

    }
}
