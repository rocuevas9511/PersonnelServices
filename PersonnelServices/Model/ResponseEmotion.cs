using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.Model
{
    public class ResponseEmotion
    {
        public string faceId { get; set; }
        public Rectangle faceRectangle { get; set; }
        public FacesAttributes faceAttributes { get; set; }


    }
    public class Rectangle
    {
        public int top { get; set; }
        public int left { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class FacesAttributes
    {
        public Emotion emotion { get; set; }
    }

    public class Emotion
    {
        public float anger { get; set; }
        public float contempt { get; set; }
        public float disgust { get; set; }
        public float fear { get; set; }
        public float happiness { get; set; }
        public float neutral { get; set; }
        public float sadness { get; set; }
        public float surprise { get; set; }

    }
}
