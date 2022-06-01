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
/// BookApp ���� ������(���Ӽ�) ���� ���� �ڵ�
/// </summary>
// pkg : Microsoft.EntityFrameworkCore.SqlServer

// new dbcontext add
//builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BookAppDbContext>(options =>
//        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// db container�� repository ���
//builder.Services.AddTransient<IBookRepository, BookRepository>(); 

// ������Ʈ ���� ���� ������ �ƴ϶�, Shared�� ��ϵǾ� �ִ� ������ ����
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
