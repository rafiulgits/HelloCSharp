using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models 
{
    public class Post 
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {set; get;}

        [BsonElement("title")]
        public string Title {set; get;}

        [BsonElement("body")]
        public string Body {set; get;}

        [BsonElement("time")]
        public string Time {set; get;}

        [BsonElement("date")]
        public string Date {set; get;}

        [BsonElement("clap")]
        public long Clap {set; get;}

        [BsonElement("user")]
        public User Owner {set; get;}

        [BsonElement("comments")]
        public string[] Comments {set; get;}
    }
}