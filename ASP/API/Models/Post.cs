using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models 
{
    public class Post 
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {set; get;} = ObjectId.GenerateNewId().ToString();

        [BsonElement("title")]
        [BsonRequired]
        public string Title {set; get;}

        [BsonElement("body")]
        [BsonRequired]
        public string Body {set; get;}

        [BsonElement("createdOn")]
        [BsonDateTimeOptions(Kind=DateTimeKind.Local)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedOn {set; get;} = DateTime.Now;

        [BsonElement("clap")]
        public long Clap {set; get;} = 0;

        [BsonElement("owner")]
        [BsonRequired]
        public User Owner {set; get;}

        [BsonElement("comments")]
        public Comment[] Comments {set; get;} = {};

        public Post(string title, string body, User user){
            Title = title;
            Body = body;
            Owner = user;
        }
    }
}