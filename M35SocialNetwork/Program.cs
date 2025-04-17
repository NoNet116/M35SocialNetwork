using App.Data;
using App.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ����������� �������� ���� ������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

// ��������� Identity � ������������� ������
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

//������������� �������������� HTTP-������� �� HTTPS.
app.UseHttpsRedirection();
//�������� ������� ������������� (routing middleware), ������� ����������, ���� ���������
app.UseRouting();

app.UseAuthentication();
/*���������� �������� ���� ������� � ��������.
 �������� � ���� � ���������� [Authorize], ����� ��������� ��� ��������� ������ � ������������/�������.

 �����: ����� ���� ������ ���������� app.UseAuthentication();, ���� ������������ ��������������. */
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
