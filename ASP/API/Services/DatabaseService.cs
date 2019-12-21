using API.Models;
using API.Models.Repository;
using MongoDB.Driver;

namespace API.Services
{
    public class DatabaseService
    {
        public readonly UserRepository User;
        public readonly PostRepository Post;

        public DatabaseService(DatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.DatabaseHost);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            User = new UserRepository(database);
            Post = new PostRepository(database);
        }
    }
}