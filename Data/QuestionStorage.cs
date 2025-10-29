using OnlineQuizSystem.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineQuizSystem.Data
{
    public static class QuestionStorage
    {
        private static readonly string FilePath = "questions.json";

        public static List<Question> LoadQuestions()
        {
            if (!File.Exists(FilePath))
                return new List<Question>();

            var json = File.ReadAllText(FilePath);
            // Deserialize into lightweight DTOs to avoid navigation/property issues
            var doc = JsonDocument.Parse(json);
            var list = new List<Question>();
            foreach (var elem in doc.RootElement.EnumerateArray())
            {
                var q = new Question
                {
                    Id = elem.GetProperty("Id").GetInt32(),
                    Text = elem.GetProperty("Text").GetString() ?? string.Empty,
                    QuizType = elem.TryGetProperty("QuizType", out JsonElement quizType) ? quizType.GetString() ?? "General" : "General",
                    Answers = new List<Answer>()
                };

                if (elem.TryGetProperty("Answers", out var answersElem) && answersElem.ValueKind == JsonValueKind.Array)
                {
                    foreach (var aElem in answersElem.EnumerateArray())
                    {
                        var a = new Answer
                        {
                            Id = aElem.GetProperty("Id").GetInt32(),
                            Text = aElem.GetProperty("Text").GetString() ?? string.Empty,
                            IsCorrect = aElem.GetProperty("IsCorrect").GetBoolean(),
                            QuestionId = aElem.TryGetProperty("QuestionId", out var qid) ? qid.GetInt32() : q.Id,
                            Question = q
                        };
                        q.Answers.Add(a);
                    }
                }

                list.Add(q);
            }

            return list;
        }

        public static void SaveQuestions(List<Question> questions)
        {
            // Map to lightweight DTOs (avoid navigation props) before serializing
            var dto = new List<object>();
            foreach (var q in questions)
            {
                var answers = new List<object>();
                if (q.Answers != null)
                {
                    foreach (var a in q.Answers)
                    {
                        answers.Add(new
                        {
                            a.Id,
                            a.Text,
                            a.IsCorrect,
                            a.QuestionId
                        });
                    }
                }

                dto.Add(new { q.Id, q.Text, Answers = answers });
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(dto, options);
            File.WriteAllText(FilePath, json);
        }
    }
}
