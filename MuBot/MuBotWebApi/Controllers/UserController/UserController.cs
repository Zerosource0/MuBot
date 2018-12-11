using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using MuBotWebApi.Extensions;
using MuBotWebApi.Services;
using Newtonsoft.Json;

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

        [HttpGet("verify")]
        public ActionResult VerifyToken()
        {
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public ActionResult Ping()
        {
            return Ok("MuBot Backend is running");
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public ActionResult CreateUser([FromBody]CreateUserRequest request)
        {
            _userService.CreateUser(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate([FromBody]LoginRequest request)
        {
            User user = _userService.Authenticate(request.Username, request.Password);
            return Ok(user.AuthToken);
        }
    }
}