// Точка входа веб-приложения: настройка MVC и создание базы данных при старте.

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

Database.Init(Path.Combine(app.Environment.ContentRootPath, "App_Data"));

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
