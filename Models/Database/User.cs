using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpotifyWebAPI.Services;

namespace Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UserId { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string AuthToken { get; set; }

        [ForeignKey("FK_TokenId")]
        public virtual SpotifyToken SpotifyToken { get; set; }
    }
}
