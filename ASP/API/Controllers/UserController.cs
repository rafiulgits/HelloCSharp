using API.Models;
using API.Models.Extension;
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
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] AuthRequest.Login formData)
        {
            var user = _userService.GetUserByEmail(formData.Email);
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
        public ActionResult<List<User>> Get() =>
            _userService.Get();


        [HttpGet("profile")]
        public ActionResult<User> Profile()
        {
            var user =  _userService.Get(HttpContext.User.Identity.Name);
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
            var existingUser = _userService.GetUserByEmail(formData.Email);
            if(existingUser != null)
            {
                return BadRequest("user with this email already registered");
            }
            var newUser = new User();
            newUser.Email = formData.Email;
            newUser.UserName = formData.UserName;
            newUser.SetPassword(formData.Password);
            newUser = _userService.Create(newUser);
            return newUser.GetToken();
        }
    }
}