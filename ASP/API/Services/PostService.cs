using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _posts;

        public PostService(IMongoDatabase database)
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

        public Comment AddComment(string postId, Comment comment)
        {
            Post post = _posts.Find(item => item.Id == postId).FirstOrDefault();
            if(post == null)
            {
                return null;
            }
            post.Comments.Append(comment);
            return comment;
        }
    }
}