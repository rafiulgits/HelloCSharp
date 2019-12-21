using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models
{
    public class Comment
    {
        public string Id {set; get;} = ObjectId.GenerateNewId().ToString();

        public string Body {set; get;}

        public DateTime CreatedOn {set; get;} = DateTime.Now;

        public User Owner {set; get;}
        
        public Comment(string body, User owner) {
            Body = body;
            Owner = owner;
        }
    }
}