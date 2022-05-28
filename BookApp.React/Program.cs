using BookApp.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

#region connect
/// <summary>
/// BookApp 관련 의존성(종속성) 주입 관련 코드
/// </summary>
// pkg : Microsoft.EntityFrameworkCore.SqlServer

// new dbcontext add
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BookAppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// db container에 repository 등록
builder.Services.AddTransient<IBookRepository, BookRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
