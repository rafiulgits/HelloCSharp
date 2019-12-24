using API.Models;
using API.Models.Extension;
using API.Route.Response;
using API.Route.Requests;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace API.Controller
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DatabaseService DB;

        public PostController(DatabaseService databaseService)
        {
            DB = databaseService;
        }
        
        [HttpGet("all")]
        public ActionResult<List<PostResponse.Post>> Get()
        {
            var posts = DB.Post.Get();
            List<PostResponse.Post> responseData = new List<PostResponse.Post>();
            posts.ForEach(post => responseData.Add(new PostResponse.Post(post)));
            return responseData;
        }
    

        [HttpGet("item/{id:length(24)}", Name = "GetPost")]
        public ActionResult<PostResponse.Post> Get(string id)
        {
            var post = DB.Post.Get(id);
            if(post == null){
                return NotFound();
            }
            return new PostResponse.Post(post);
        }

        [HttpPost("create")]
        public ActionResult<PostResponse.Post> Create([FromBody]PostRequest.Create formData)
        {
            var user = DB.User.Get(HttpContext.User.Identity.Name);
            if(user == null)
            {
                return BadRequest("requested user not found");
            }
            if(!formData.IsValid())
            {
                return BadRequest("title and body both required");
            }
            user = user.WithoutPassword();
            var post = DB.Post.Create(new Post(formData.Title, formData.Body, user));
            return new PostResponse.Post(post);
        }

        [HttpPost("addcomment/{id}", Name="AddComment")]
        public ActionResult<PostResponse.Comment> AddComment([FromBody]PostRequest.AddComment formData, string id)
        {
            var user = DB.User.Get(HttpContext.User.Identity.Name);
            if(user == null)
            {
                return BadRequest("requested user not found");
            }
            if(!formData.IsValid())
            {
                return BadRequest("title and body both required");
            }
            user = user.WithoutPassword();
            Comment comment = new Comment(formData.Body, user);
            var result = DB.Post.AddComment(id, comment);
            if(result == null)
            {
                return NotFound("no post found with this id");
            }
            return new PostResponse.Comment(comment);
        }
        
        [HttpPut("update/{id}", Name="UpdatePost")]
        public ActionResult<PostResponse.Post> Update([FromForm]PostRequest.Update formData, string id)
        {
            var user = DB.User.Get(HttpContext.User.Identity.Name);
            if(user == null)
            {
                return BadRequest("requested user not found");
            }
            if(!formData.IsValid())
            {
                return BadRequest("given data is invalid");
            }
            var result = DB.Post.Update(id, formData.Title, formData.Body);
            if(result == null)
            {
                return NotFound("no post found with this id");
            }
            return new PostResponse.Post(result);
        }


        // [HttpPut("updatecomment/{id}", Name="UpdateComment")]
        // public ActionResult<Comment> UpdateComment([FromBody]PostRequest.UpdateComment formData, string id)
        // {
        //     var user = DB.User.Get(HttpContext.User.Identity.Name);
        //     if(user == null)
        //     {
        //         return BadRequest("requested user not found");
        //     }
        //     if(!formData.IsValid())
        //     {
        //         return BadRequest("given data is invalid");
        //     }
        //     user = user.WithoutPassword();
        //     var result = DB.Post.Update
        // }
    }
}