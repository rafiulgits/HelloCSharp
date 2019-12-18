using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _posts;

        public PostService(DatabaseSettings settings)
        {
            var client = new MongoClient(settings.DatabaseHost);
            var database = client.GetDatabase(settings.DatabaseName);
            _posts = database.GetCollection<Post>("post");
        }

        public List<Post> Get() =>
            _posts.Find(post => true).ToList();

        
        public Post Get(string id) => 
           _posts.Find(post => post.Id == id).FirstOrDefault();


        public Post Create(Post post) {
            _posts.InsertOne(post);
            return post;
        }
    }
}