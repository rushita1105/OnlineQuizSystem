using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuizSystem.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult StartQuiz(string? quizType = null)
        {
            if (string.IsNullOrEmpty(quizType))
            {
                var availableQuizTypes = AdminController.Questions
                    .Select(q => q.QuizType)
                    .Distinct()
                    .OrderBy(qt => qt)
                    .ToList();
                    var quizTypes = AdminController.Questions
                        .GroupBy(q => q.QuizType)
                        .Select(g => new
                        {
                            Type = g.Key,
                            Count = g.Count()
                        })
                        .OrderBy(qt => qt.Type)
                        .ToList();
                    ViewBag.QuizTypes = quizTypes;
                return View("SelectQuiz");
            }

            var questions = AdminController.Questions.Where(q => q.QuizType == quizType).ToList();
            ViewBag.QuizType = quizType;
            return View(questions);
        }

        [HttpPost]
        public IActionResult SubmitQuiz(Dictionary<int, string> answers)
        {
            int score = 0;
            foreach (var answer in answers)
            {
                var question = AdminController.Questions.FirstOrDefault(q => q.Id == answer.Key);
                if (question != null && question.Answers.Any(a => a.Text == answer.Value && a.IsCorrect))
                {
                    score++;
                }
            }

            string? username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var result = new Result 
            { 
                Id = AdminController.Results.Any() ? AdminController.Results.Max(r => r.Id) + 1 : 1,
                Username = username, 
                Score = score,
                AttemptDate = DateTime.Now
            };
            AdminController.Results.Add(result);
            OnlineQuizSystem.Data.ResultStorage.SaveResults(AdminController.Results);

            ViewBag.Score = score;
            ViewBag.Total = AdminController.Questions.Count;
            return View("Result");
        }
    }
}
