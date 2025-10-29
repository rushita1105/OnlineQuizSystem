using OnlineQuizSystem.Models;
using OnlineQuizSystem.Controllers;
using System.Collections.Generic;

namespace OnlineQuizSystem.Data
{
    public static class SampleData
    {
        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123",
                    IsAdmin = true
                }
            };
        }

        public static List<Question> GetQuestions()
        {
            var q1 = new Question
            {
                Id = 1,
                Text = "What is the capital of India?",
                 QuizType = "Geography",
                 Answers = new List<Answer>()
            };

            var q2 = new Question
            {
                Id = 2,
                Text = "Which language is used in ASP.NET?",
                 QuizType = "Programming",
                 Answers = new List<Answer>()
            };

            // Add answers after questions are created to properly set up the relationship
            q1.Answers.Add(new Answer { Id = 1, Text = "Delhi", IsCorrect = true, Question = q1 });
            q1.Answers.Add(new Answer { Id = 2, Text = "Mumbai", IsCorrect = false, Question = q1 });
            q1.Answers.Add(new Answer { Id = 3, Text = "Chennai", IsCorrect = false, Question = q1 });
            q1.Answers.Add(new Answer { Id = 4, Text = "Kolkata", IsCorrect = false, Question = q1 });

            q2.Answers.Add(new Answer { Id = 5, Text = "C#", IsCorrect = true, Question = q2 });
            q2.Answers.Add(new Answer { Id = 6, Text = "Python", IsCorrect = false, Question = q2 });
            q2.Answers.Add(new Answer { Id = 7, Text = "Java", IsCorrect = false, Question = q2 });
            q2.Answers.Add(new Answer { Id = 8, Text = "PHP", IsCorrect = false, Question = q2 });

            return new List<Question> { q1, q2 };
        }
    }
}
