using BookApp.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region connect
/// <summary>
/// BookApp 관련 의존성(종속성) 주입 관련 코드
/// </summary>
// pkg : Microsoft.EntityFrameworkCore.SqlServer

// new dbcontext add
//builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BookAppDbContext>(options =>
//        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// db container에 repository 등록
//builder.Services.AddTransient<IBookRepository, BookRepository>(); 

// 프로젝트 별로 각각 셋팅이 아니라, Shared에 등록되어 있는 것으로 셋팅
builder.Services.AddDependencyInjectionContainerForBookApp(builder.Configuration.GetConnectionString("DefaultConnection"));

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
