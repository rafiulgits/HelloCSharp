using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class User 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {set; get;}

        [BsonElement("name")]
        public string UserName {set; get;}

        [BsonElement("email")]
        public string Email {set; get;}

        [BsonElement("password")]
        public string Password {set; get;}

        public User()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}