using Microsoft.AspNetCore.Mvc.Filters;
using MuBotWebApi.Extensions;
using MuBotWebApi.Services;

namespace MuBotWebApi.Controllers.SpotifyControllers
{
    public class UpdateTokenFilter : IActionFilter
    {
        private readonly IUserService _userService;

        public UpdateTokenFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            if (!(context.Controller is SpotifyControllerBase controllerBase)) return;

            var user = _userService.GetUser(controllerBase.GetCurrentUserId());

            if (user.SpotifyToken == null) return;

            var token = user.SpotifyToken.ToAuthenticationToken();

            if (!token.HasExpired) return;

            await token.Refresh();
            user.SpotifyToken = token.ToSpotifyToken();
            _userService.UpdateUser(user);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
