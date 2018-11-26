using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuBotWebApi.Services;
using UserService = SpotifyWebAPI.Services.UserService;

namespace MuBotWebApi.Controllers.SpotifyControllers
{
    [Route("api/spotify/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProfile()
        {
            var user = _userService.GetUser(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(await UserService.Instance.GetCurrentUserProfile(user.SpotifyToken));
        }
    }
}