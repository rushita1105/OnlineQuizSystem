using System;

namespace OnlineQuizSystem.Models
{
    public class ResultViewModel
    {
        public required string Username { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}