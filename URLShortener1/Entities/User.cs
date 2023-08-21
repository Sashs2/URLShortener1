using System.ComponentModel.DataAnnotations;

namespace URLShortener1.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        
        [Required]
        public string Role { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public ICollection<Url>? Urls { get; set; }
    }
}
