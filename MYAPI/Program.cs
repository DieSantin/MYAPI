using Microsoft.EntityFrameworkCore;
using MYAPI.Data;
using MYAPI.ExApi;
using MYAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString(name: "DefaultDatabaseConnection");

DataResolver.Register(builder.Services, connectionString);
ExApiResolver.Register(builder.Services);
ServicesResolver.Register(builder.Services);
CommonResolver.Register(builder.Services);

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
