using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace API.Models.Repository
{
    public class PostRepository
    {
        private readonly IMongoCollection<Post> _posts;

        public PostRepository(IMongoDatabase database)
        {
            _posts = database.GetCollection<Post>("post");
        }

        public List<Post> Get() =>
            _posts.Find(post => true).ToList();

        
        public Post Get(string id) => 
           _posts.Find(post => post.Id == id).FirstOrDefault();


        public Post Create(Post post) 
        {
            _posts.InsertOne(post);
            return post;
        }

        public Post Update(string postId, string Title, string Body)
        {
            var filter = Builders<Post>.Filter.Eq(item => item.Id , postId);
            var operation = Builders<Post>.Update
                .Set(item => item.Title , Title)
                .Set(item => item.Body, Body);
            var result = _posts.UpdateOne(filter, operation);
            if(!result.IsAcknowledged)
            {
                return null;
            }
            return Get(postId);
        }

        public Comment AddComment(string postId, Comment comment)
        {
            var filter = Builders<Post>.Filter.Eq(item => item.Id, postId);
            var operation = Builders<Post>.Update.Push(item => item.Comments, comment);
            var result = _posts.FindOneAndUpdate(filter, operation);
            if(result == null)
            {
                return null;
            }
            return comment;
        }
    }
}