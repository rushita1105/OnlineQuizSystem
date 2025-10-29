# 🧠 Online Quiz Management System (ASP.NET MVC)

## 📋 Overview
The **Online Quiz Management System** is a web-based application built using **ASP.NET MVC (C#)**.  
It allows an **Admin** to create, edit, and delete quiz questions and view user results, while **Users** can register, log in, and take quizzes added by the admin. This project demonstrates **role-based access**, **session management**, and **CRUD operations** in ASP.NET MVC.

---

## 🚀 Features

### 👤 User Panel
- Register and log in securely  
- Take quizzes with multiple-choice questions  
- Submit answers and view total score instantly  

### 🧑‍💼 Admin Panel
- Log in using fixed credentials  
- Add new quiz questions  
- Edit or delete existing questions  
- View results of all users who attempted the quiz  

---

## 🧱 Admin Login Details
---
- Username: admin
- Password: admin123

---

## 🛠️ Technologies Used
- ASP.NET MVC (C#)
- Razor Views
- HTML, CSS , JS
- Session Management
- In-memory Data (no database)

---

### 🧩 Project Structure

📁 **OnlineQuizSystem**
- 📂 **Controllers/**
  - 🧠 HomeController.cs  
  - 🧠 QuizController.cs  
  - 🧠 UserController.cs  
- 📂 **Models/**
  - 📄 Question.cs  
  - 📄 Quiz.cs  
  - 📄 User.cs  
- 📂 **Views/**
  - 🏠 Home/
    - Index.cshtml  
    - About.cshtml  
    - Contact.cshtml  
  - 📝 Quiz/
    - Start.cshtml  
    - Question.cshtml  
    - Result.cshtml  
  - ⚙️ Shared/
    - _Layout.cshtml  
    - _ValidationScriptsPartial.cshtml  
- 🌐 **wwwroot/**
  - css/site.css  
  - js/site.js  
  - images/logo.png  
- ⚙️ appsettings.json  
- 🧩 Program.cs  
- 🚀 Startup.cs  
- 📘 README.md  

---
### How to Run
### 1️⃣ Clone or Download
```bash
git clone https://github.com/rushita1105/OnlineQuizSystem.git
cd OnlineQuizSystem
```
### 2️⃣ Run the Project

Open the project in Visual Studio or VS Code.
Press Ctrl + F5 to start the application
Or run this command in the terminal:
```
dotnet run
```
### Conclusion
The Online Quiz Management System is a simple yet effective ASP.NET MVC project built to demonstrate core web development concepts.
It highlights practical implementation of:

- 🧩 MVC Architecture — Separation of Model, View, and Controller layers
- ⚙️ CRUD Operations — Create, Read, Update, and Delete quiz questions
- 🔐 Session Handling — Manage user sessions for authentication and quiz flow
- 👥 Role-Based Access Control — Different privileges for Admin and User




