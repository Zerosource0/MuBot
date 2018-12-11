using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuBotWebApi.Extensions;
using MuBotWebApi.Services;
using UserService = SpotifyWebAPI.Services.UserService;

namespace MuBotWebApi.Controllers.SpotifyControllers
{
    [Route("api/spotify/profile")]
    [ApiController]
    public class SpotifyProfileController : SpotifyControllerBase
    {
        private readonly IUserService _userService;

        public SpotifyProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProfile()
        {
            var user = _userService.GetUser(this.GetCurrentUserId());
            
            return Ok(await UserService.Instance.GetCurrentUserProfile(user.SpotifyToken.ToAuthenticationToken()));
        }
    }
}