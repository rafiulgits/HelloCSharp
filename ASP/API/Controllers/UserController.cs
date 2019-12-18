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
        private readonly UserService _userService;
        private readonly JwtSettings _jwtSettings;

        public UserController(UserService userService, JwtSettings jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] AuthRequest.Login formData)
        {
            var user = _userService.Authenticate(formData.Email, formData.Password);
            if(user == null)
            {
                return BadRequest("authentication credentials failed");
            }
            return user.GetToken(_jwtSettings.Secret);
        }

        [HttpGet("all")]
        public ActionResult<List<User>> Get() =>
            _userService.Get();


        [HttpPost("create", Name="CreateUser")]
        public IActionResult Create([FromBody]User user)
        {
            if(string.IsNullOrEmpty(user.Id)){
                var id = System.Guid.NewGuid().ToString();
                user.Id = id.Replace("-", "").Substring(0, 24);
            }
            var newUser = _userService.Create(user);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = $"{baseUrl}/api/user/{user.Id}";
            return Created(locationUri, newUser);
        }
    }
}