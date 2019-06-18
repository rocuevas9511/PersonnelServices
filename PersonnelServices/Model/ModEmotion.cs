﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.Model
{
    public class ModEmotion: ModelBase
    {
        public static string MODEL_DATE
        {
            get { return KEY_DATE; }
        }

        public static string MODEL_DETAILS
        {
            get { return KEY_DETAILS; }
        }
        public static string MODEL_EMOTION
        {
            get { return KEY_EMOTION; }
        }
        public static string MODEL_SCORE
        {
            get { return KEY_SCORE; }
        }

        [BsonElement(KEY_DATE)]
        public string Date { get; set; }

        [BsonElement(KEY_DETAILS)]
        public string Details { get; set; }

        [BsonElement(KEY_EMOTION)]
        public string Emotion { get; set; }

        [BsonElement(KEY_SCORE)]
        public string Score { get; set; }

    }
}
