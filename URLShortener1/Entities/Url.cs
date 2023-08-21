using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace URLShortener1.Entities
{
    public class Url
    {

        public int Id { get; set; }

        [Required] 
        public string LongUrl { get; set; }

        [Required]
        public string ShortUrl { get; set; }

      
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }



        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
      

        public User User { get; set; }
    }
}
