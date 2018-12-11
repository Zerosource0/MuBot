using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MuBotWebApi.Extensions
{
    public static class UserExtensions
    {
        public static int GetCurrentUserId(this ControllerBase controllerBase)
        {
            string id = ((ClaimsIdentity) controllerBase.User.Identity)?.FindFirst(ClaimTypes.Name)?.Value;
            return int.Parse(id);
        }
    }
}
