using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuizSystem.Controllers
{
    public class AdminController : Controller
    {
        // Load from storage
        public static List<Question> Questions = OnlineQuizSystem.Data.QuestionStorage.LoadQuestions();
        public static List<Result> Results = OnlineQuizSystem.Data.ResultStorage.LoadResults();

        public IActionResult Dashboard()
        {
            return View(Questions);
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View(new AddQuestionViewModel
            {
                QuizType = string.Empty,
                QuestionText = string.Empty,
                Option1 = string.Empty,
                Option2 = string.Empty,
                Option3 = string.Empty,
                Option4 = string.Empty
            });
        }

        [HttpPost]
        public IActionResult AddQuestion(AddQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    Id = Questions.Any() ? Questions.Max(q => q.Id) + 1 : 1,
                    Text = model.QuestionText,
                    QuizType = model.QuizType,
                    Answers = new List<Answer>()
                };

                // Add options
                var options = new[]
                {
                    new { Text = model.Option1, Id = 1 },
                    new { Text = model.Option2, Id = 2 },
                    new { Text = model.Option3, Id = 3 },
                    new { Text = model.Option4, Id = 4 }
                };

                int answerId = 1;
                foreach (var option in options)
                {
                    question.Answers.Add(new Answer
                    {
                        Id = answerId++,
                        Text = option.Text,
                        IsCorrect = option.Id == model.CorrectAnswer,
                        Question = question,
                        QuestionId = question.Id
                    });
                }

                Questions.Add(question);
                OnlineQuizSystem.Data.QuestionStorage.SaveQuestions(Questions);
                TempData["Message"] = "Question added successfully!";
                return RedirectToAction("Dashboard");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditQuestion(int id)
        {
            var question = Questions.FirstOrDefault(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            var correctAnswerIndex = question.Answers.FindIndex(a => a.IsCorrect) + 1;

            var viewModel = new EditQuestionViewModel
            {
                Id = question.Id,
                QuestionText = question.Text,
                Option1 = question.Answers[0].Text,
                Option2 = question.Answers[1].Text,
                Option3 = question.Answers[2].Text,
                Option4 = question.Answers[3].Text,
                CorrectAnswer = correctAnswerIndex
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditQuestion(EditQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = Questions.FirstOrDefault(q => q.Id == model.Id);
                if (question == null)
                {
                    return NotFound();
                }

                question.Text = model.QuestionText;

                var options = new[]
                {
                    new { Text = model.Option1, Id = 1 },
                    new { Text = model.Option2, Id = 2 },
                    new { Text = model.Option3, Id = 3 },
                    new { Text = model.Option4, Id = 4 }
                };

                for (int i = 0; i < options.Length; i++)
                {
                    question.Answers[i].Text = options[i].Text;
                    question.Answers[i].IsCorrect = options[i].Id == model.CorrectAnswer;
                }

                OnlineQuizSystem.Data.QuestionStorage.SaveQuestions(Questions);
                TempData["Message"] = "Question updated successfully!";
                return RedirectToAction("Dashboard");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteQuestion(int id)
        {
            var question = Questions.FirstOrDefault(q => q.Id == id);
            if (question != null)
            {
                Questions.Remove(question);
                OnlineQuizSystem.Data.QuestionStorage.SaveQuestions(Questions);
                TempData["Message"] = "Question deleted successfully!";
            }
            return RedirectToAction("Dashboard");
        }

        public IActionResult ViewResults()
        {
            // Map domain Result to ResultViewModel expected by the view
            var viewModels = Results.Select(r => new ResultViewModel
            {
                Username = r.Username,
                Score = r.Score,
                TotalQuestions = Questions.Count,
                Percentage = Questions.Count > 0 ? (r.Score * 100.0) / Questions.Count : 0,
                AttemptDate = r.AttemptDate
            }).ToList();

            return View(viewModels);
        }
    }
}
