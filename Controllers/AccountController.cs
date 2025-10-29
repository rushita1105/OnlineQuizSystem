using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuizSystem.Controllers
{
    public class AccountController : Controller
    {
        // Simulating a database with static lists
    public static List<User> Users = OnlineQuizSystem.Data.UserStorage.LoadUsers();

        // Register Page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Prevent registration of admin account
                if (user.Username.Trim().ToLower() == "admin")
                {
                    ModelState.AddModelError("Username", "'admin' is reserved and cannot be registered.");
                    return View(user);
                }
                user.IsAdmin = false;
                Users.Add(user);
                OnlineQuizSystem.Data.UserStorage.SaveUsers(Users);
                TempData["Message"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Fixed admin credentials
            if (model.Username.Trim().ToLower() == "admin" && model.Password == "admin123")
            {
                HttpContext.Session.SetString("Username", "admin");
                HttpContext.Session.SetString("IsAdmin", "True");
                return RedirectToAction("Dashboard", "Admin");
            }
            var user = Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("IsAdmin", "False");
                return RedirectToAction("StartQuiz", "Quiz");
            }
            ViewBag.Message = "Invalid credentials!";
            return View(model);
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
