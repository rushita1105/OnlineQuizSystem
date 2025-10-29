namespace OnlineQuizSystem.Models
{
    public class Result
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public int Score { get; set; }
        public DateTime AttemptDate { get; set; } = DateTime.Now;
    }
}
