using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.DTO;
using MuBotWebApi.Extensions;
using MuBotWebApi.Helpers;
using MuBotWebApi.Services;
using SpotifyWebAPI.Services;
using UserService = MuBotWebApi.Services.UserService;

namespace MuBotWebApi.Controllers.SpotifyControllers
{
    [Authorize]
    [Route("api/spotify/auth")]
    [ApiController]
    public class SpotifyAuthController : SpotifyControllerBase
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IUserService _userService;

        public SpotifyAuthController(IOptions<AppSettings> appSettings, IUserService userService)
        {
            _appSettings = appSettings;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult HasToken()
        {
            var user = _userService.GetUser(this.GetCurrentUserId());
            if (user.SpotifyToken == null)
                return NoContent();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateToken([FromBody] CreateTokenRequest request)
        {
            AuthenticationService.ClientId = _appSettings.Value.SpotifyClientId;
            AuthenticationService.ClientSecret = _appSettings.Value.SpotifyClientSecret;
            AuthenticationService.RedirectUri = request.RedirectUri;

            var accessToken = await AuthenticationService.GetAccessToken(request.Code);

            var user = _userService.GetUser(this.GetCurrentUserId());
            var spotifyToken = accessToken.ToSpotifyToken();
            spotifyToken.User = user;
            user.SpotifyToken = spotifyToken;

            _userService.UpdateUser(user);

            return Ok();
        }
    }
}