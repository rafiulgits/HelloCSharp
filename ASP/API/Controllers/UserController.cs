using API.Models;
using API.Models.Extension;
using API.Options;
using API.Route.Requests;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controller
{
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly DatabaseService DB;

        public UserController(DatabaseService databaseService)
        {
           DB = databaseService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] AuthRequest.Login formData)
        {
            var user = DB.User.GetUserByEmail(formData.Email);
            if(user == null)
            {
                return BadRequest("no user found with this email");
            }
            if(user.CheckPassword(formData.Password) == false)
            {
                return BadRequest("incorrect password");
            }
            return user.GetToken();
        }

        [HttpGet("all")]
        public ActionResult<List<User>> Get() => DB.User.Get();


        [HttpGet("profile")]
        public ActionResult<User> Profile()
        {
            var user =  DB.User.Get(HttpContext.User.Identity.Name);
            if(user == null)
            {
                return null;
            }
            return user.WithoutPassword();
        }

        [AllowAnonymous]
        [HttpPost("register", Name="CreateUser")]
        public ActionResult<string> Create([FromBody]AuthRequest.Register formData)
        {
            var existingUser = DB.User.GetUserByEmail(formData.Email);
            if(existingUser != null)
            {
                return BadRequest("user with this email already registered");
            }
            var newUser = new User();
            newUser.Email = formData.Email;
            newUser.Name = formData.Name;
            newUser.SetPassword(formData.Password);
            newUser = DB.User.Create(newUser);
            return newUser.GetToken();
        }
    }
}