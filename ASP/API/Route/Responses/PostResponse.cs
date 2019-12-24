using Model = API.Models;
using System;
using System.Collections.Generic;

namespace API.Route.Response
{
    public class PostResponse
    {
        public class Comment
        {
            public string Id {set; get;}
            public string Body {set; get;}
            public DateTime CreatedOn {set; get;}
            public UserResponse.Profile User {set; get;}

            public Comment(Model.Comment comment)
            {
                Id = comment.Id;
                Body = comment.Body;
                CreatedOn = comment.CreatedOn;
                User = new UserResponse.Profile(comment.Owner);
            }
        }

        public class Post
        {
            public string Id {set; get;}
            public string Title {set; get;}
            public string Body {set; get;}
            public DateTime CreatedOn {set; get;}
            public UserResponse.Profile User {set; get;}
            public List<Comment> Comments {set; get;}

            public Post(Model.Post post)
            {
                Id = post.Id;
                Title = post.Title;
                Body = post.Body;
                CreatedOn = post.CreatedOn;
                User = new UserResponse.Profile(post.Owner);

                SetupComments(post);
            }

            private void SetupComments(Model.Post post)
            {
                List<Comment> comments = new List<Comment>();
                foreach(var comment in post.Comments)
                {
                    comments.Add(new Comment(comment));
                }
                Comments = comments;
            }
        }
    }
}