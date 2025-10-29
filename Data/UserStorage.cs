using OnlineQuizSystem.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace OnlineQuizSystem.Data
{
    public static class UserStorage
    {
        private static readonly string FilePath = "users.json";

        public static List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public static void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(FilePath, json);
        }
    }
}
