using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MuBotWebApi.Controllers.SpotifyControllers
{
    [Route("api/spotify/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        // extend with user information
        [HttpPost]
        public void RecieveToken([FromBody] string value)
        {

        }


    }
}