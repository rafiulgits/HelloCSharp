using API.Models;
using API.Options;
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
        public ActionResult<List<Post>> Get() => DB.Post.Get();
    

        [HttpGet("item/{id:length(24)}", Name = "GetPost")]
        public ActionResult<Post> Get(string id)
        {
            var post = DB.Post.Get(id);
            if(post == null){
                return NotFound();
            }
            return post;
        }

        [HttpPost("create")]
        public ActionResult<Post> Create([FromBody]PostRequest.Create formData)
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
            var post = new Post(formData.Title, formData.Body, user);
            return post;
        }
    }
}