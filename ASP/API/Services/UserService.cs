using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(DatabaseSettings settings)
        {
            var client = new MongoClient(settings.DatabaseHost);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.CollectionName);
        }

        public List<User> Get() => 
            _users.Find(user => true).ToList();

        public User Get(string id) => 
            _users.Find<User>(user => user.Id == id).FirstOrDefault();


        public User Create(User user) {
            _users.InsertOne(user);
            return user;
        }
    }
}