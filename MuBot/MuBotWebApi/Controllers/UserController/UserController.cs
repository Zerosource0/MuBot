using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using MuBotWebApi.Services;

namespace MuBotWebApi.Controllers.UserController
{
    [Authorize]
    [Route("api/login")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("test")]
        [Produces("application/json")]
        public ActionResult Test()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userid = claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value;
            var otherId = User.FindFirst("sub")?.Value;
            return Ok($"You are authorized! id:{userid}, other:{otherId}");
        }

        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public ActionResult Ping()
        {
            return Ok("MuBot Backend is running");
        }



        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<User> Authenticate([FromBody]LoginParams loginParams)
        {
            var user = _userService.Authenticate(loginParams.Username, loginParams.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }

    public class LoginParams
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}