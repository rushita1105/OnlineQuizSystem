using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
