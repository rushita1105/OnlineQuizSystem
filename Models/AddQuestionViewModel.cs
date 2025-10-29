using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class AddQuestionViewModel
    {
        [Required(ErrorMessage = "Quiz type is required")]
        public required string QuizType { get; set; }

        [Required(ErrorMessage = "Question text is required")]
        public required string QuestionText { get; set; }

        [Required(ErrorMessage = "Option 1 is required")]
        public required string Option1 { get; set; }

        [Required(ErrorMessage = "Option 2 is required")]
        public required string Option2 { get; set; }

        [Required(ErrorMessage = "Option 3 is required")]
        public required string Option3 { get; set; }

        [Required(ErrorMessage = "Option 4 is required")]
        public required string Option4 { get; set; }

        [Required(ErrorMessage = "Please select the correct answer")]
        [Range(1, 4, ErrorMessage = "Please select the correct answer (1-4)")]
        public int CorrectAnswer { get; set; }
    }
}