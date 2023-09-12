using Microsoft.EntityFrameworkCore;
using MYAPI.Data.Context;
using MYAPI.Services.EXAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddHttpClient();
builder.Services.AddTransient<IEXAPIService, EXAPIService>();

var connectionString = builder.Configuration.GetConnectionString(name: "DefaultDatabaseConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var testConnectionString = builder.Configuration.GetConnectionString(name: "TestDatabaseConnection");
builder.Services.AddDbContext<TestMYAPIDbContext>(opt => opt.UseMySql(testConnectionString, ServerVersion.AutoDetect(testConnectionString)));

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

public partial class Program { }
