using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using MuBotWebApi.Helpers;

namespace MuBotWebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        void UpdateUser(User user);
        User GetUser(string id);
    }

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Marc", LastName = "Jesse", Username = "Wehrmarc", Password = "pass" }
        };

        public void UpdateUser(User user)
        {
            _users = _users.Where(p => p.Id == user.Id).Select(u => user).ToList();
        }

        public User GetUser(string id)
        {
            return _users.FirstOrDefault(x => x.Username.Equals(id));
        }

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private readonly AppSettings _appSettings;

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username.ToLowerInvariant() == username.ToLowerInvariant() && x.Password == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Hash);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            user.AuthToken = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }
    }
}
