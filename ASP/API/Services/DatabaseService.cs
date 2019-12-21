using API.Models;
using MongoDB.Driver;

namespace API.Services
{
    public class DatabaseService
    {
        public readonly UserService User;
        public readonly PostService Post;

        public DatabaseService(DatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.DatabaseHost);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            User = new UserService(database);
            Post = new PostService(database);
        }
    }
}