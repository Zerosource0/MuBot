using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MuBotWebApi.Helpers;
using MuBotWebApi.Services;
using SpotifyWebAPI.Services;
using UserService = MuBotWebApi.Services.UserService;


namespace MuBotWebApi.Controllers.SpotifyControllers
{
    [Authorize]
    [Route("api/spotify/auth")]
    [ApiController]
    public class SpotifyAuthController : ControllerBase
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IUserService _userService;

        public SpotifyAuthController(IOptions<AppSettings> appSettings, UserService userService)
        {
            _appSettings = appSettings;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateToken([FromBody] string code, string redirectUri)
        {
            AuthenticationService.ClientId = _appSettings.Value.SpotifyClientId;
            AuthenticationService.ClientSecret = _appSettings.Value.SpotifyClientSecret;
            AuthenticationService.ClientId = _appSettings.Value.SpotifyClientId;
            AuthenticationService.RedirectUri = redirectUri;

            var accessToken = await AuthenticationService.GetAccessToken(code);

            var user = _userService.GetUser(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            user.SpotifyToken = accessToken;

            _userService.UpdateUser(user);

            return Ok();
        }
    }
}