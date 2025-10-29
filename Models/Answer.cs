using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public required string Text { get; set; }

        public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Question? Question { get; set; }
    }
}
