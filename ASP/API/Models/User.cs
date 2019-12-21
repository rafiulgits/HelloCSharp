using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace API.Models
{
    public class User 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {set; get;} = ObjectId.GenerateNewId().ToString();

        [BsonElement("name")]
        [BsonRequired]
        public string Name {set; get;}

        [BsonElement("email")]
        [BsonRequired]
        public string Email {set; get;}

        [BsonElement("password")]
        [BsonRequired]
        public string Password {set; get;}
    }
}