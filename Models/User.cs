using SpotifyWebAPI.Services;

namespace Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
        public AuthenticationToken SpotifyToken { get; set; }
    }
}
