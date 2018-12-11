using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO;
using MuBotWebApi.Database;
using MuBotWebApi.Helpers;

namespace MuBotWebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        void UpdateUser(User user);
        User GetUser(int id);
        User GetUser(string username);
        void CreateUser(CreateUserRequest request);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _log;
        private readonly AppSettings _appSettings;
        private readonly DatabaseContext _context;

        public UserService(ILogger<UserService> log, IOptions<AppSettings> appSettings, DatabaseContext context)
        {
            _log = log;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.UserId.Value == id)
                   ?? throw new ArgumentNullException(nameof(id), $"User with id {id} could not be found.");
        }

        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                   ?? throw new ArgumentNullException(nameof(username), $"User with id {username} could not be found.");
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), $"Value cannot be null.");

            try
            {
                _context.Update(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Log(LogLevel.Error, e, $"Updating user failed. User with Id {user.UserId} does not exist", user);
                throw;
            }
        }

        public void CreateUser(CreateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new ArgumentNullException(nameof(request.Username), "Value cannot be null.");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentNullException(nameof(request.Password), "Value cannot be null.");

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentNullException(nameof(request.Email), "Value cannot be null.");

            _context.Users.Add(new User()
            {
                Username = request.Username,
                PasswordHash = CreatePasswordHash(request.Password),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            });

            _context.SaveChanges();
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username), "Value cannot be null.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Value cannot be null.");

            User user = null;
            try
            {
                user = GetUser(username);
            }
            catch (Exception e)
            {
                _log.Log(LogLevel.Warning, e, $"User with username {username} does not exist", user);
                throw new UnauthorizedAccessException($"An account with this username does not exist");
            }

            if (!VerifyPassword(password, user.PasswordHash))
            {
                _log.Log(LogLevel.Warning, $"User with Id {user.UserId} entered a wrong password", user);
                throw new UnauthorizedAccessException($"The password you've entered is incorrect");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Hash);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            user.AuthToken = tokenHandler.WriteToken(token);

            user.PasswordHash = null;

            return user;
        }

        private string CreatePasswordHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        private bool VerifyPassword(string password, string savedPasswordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
    }
}

