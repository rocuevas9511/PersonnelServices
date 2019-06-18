using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.Model
{
    public class ModSentiments : ModelBase
    {
        public static string MODEL_DATE
        {
            get { return KEY_DATE; }
        }

        public static string MODEL_SCORE
        {
            get { return KEY_SCORE; }
        }

        public static string MODEL_SENTIMENT
        {
            get { return KEY_SENTIMENT; }
        }

        [BsonElement(KEY_DATE)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

        [BsonElement(KEY_SCORE)]
        public string Score { get; set; }

        [BsonElement(KEY_SENTIMENT)]
        public string Sentiment { get; set; }
    }
}
