using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineQuizSystem.Controllers;
using OnlineQuizSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Initialize sample data
var sampleUsers = SampleData.GetUsers();
var sampleQuestions = SampleData.GetQuestions();

// Add sample data to static lists
sampleUsers.ForEach(user => AccountController.Users.Add(user));
sampleQuestions.ForEach(question => AdminController.Questions.Add(question));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();  // <-- Add this to enable session
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
