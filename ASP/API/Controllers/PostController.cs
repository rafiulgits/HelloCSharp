using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }
        
        [HttpGet("all")]
        public ActionResult<List<Post>> Get() =>
            _postService.Get();


        [HttpGet("{id:length(24)}", Name = "GetPost")]
        public ActionResult<Post> Get(string id)
        {
            var post = _postService.Get(id);
            if(post == null){
                return NotFound();
            }
            return post;
        }
    }
}