using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Data
{
    public static class ResultStorage
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "results.json");

        public static List<Result> LoadResults()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Result>();
            }

            var json = File.ReadAllText(FilePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<Result>>(json, options) ?? new List<Result>();
        }

        public static void SaveResults(List<Result> results)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(results, options);
            File.WriteAllText(FilePath, json);
        }
    }
}