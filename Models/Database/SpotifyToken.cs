using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("SpotifyToken")]
    public class SpotifyToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TokenId { get; set; }

        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string TokenType { get; set; }
        [Required]
        public DateTime ExpiresOn { get; set; }
        [Required]
        public string RefreshToken { get; set; }

        [ForeignKey("FK_UserId")]
        public virtual User User { get; set; }

    }
}
