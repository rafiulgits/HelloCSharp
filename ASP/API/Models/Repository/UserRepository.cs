using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace API.Models.Repository
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("user");
        }

        public List<User> Get() => 
            _users.Find(user => true).ToList();

        public User Get(string id) => 
            _users.Find<User>(user => user.Id == id).FirstOrDefault();


        public User Create(User user) {
            _users.InsertOne(user);
            return user;
        }

        public User GetUserByEmail(string email) =>
            _users.Find<User>(user => user.Email == email).FirstOrDefault();
    }
}