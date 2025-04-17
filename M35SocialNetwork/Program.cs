using App.Data;
using App.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Настраиваем контекст базы данных
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

// Добавляем Identity с конфигурацией пароля
builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 2;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Автоматически перенаправляет HTTP-запросы на HTTPS.
app.UseHttpsRedirection();
//Включает систему маршрутизации (routing middleware), которая определяет, куда отправить
app.UseRouting();

app.UseAuthentication();
/*Подключает проверку прав доступа к ресурсам.
 Работает в паре с атрибутами [Authorize], чтобы разрешать или запрещать доступ к контроллерам/методам.

 Важно: Перед этим обычно вызывается app.UseAuthentication();, если используется аутентификация. */
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
