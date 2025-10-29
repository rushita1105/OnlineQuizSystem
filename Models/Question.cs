using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public required string Text { get; set; }

        [Required]
        public required string QuizType { get; set; }

        public required List<Answer> Answers { get; set; } = new();
    }
}
